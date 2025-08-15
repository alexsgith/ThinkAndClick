using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    #region Properties

    [SerializeField] List<Sprite> cardSymbols;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardAreaTransform;
    [SerializeField] GameObject rowPrefab;
    [SerializeField] ScoreManager scoreScript;
    [SerializeField] float cardCheckDelay = 1f;
    //Non-Serialized
    [NonSerialized] public bool isCardFlipping = true;
    [NonSerialized] public CardPrefabScript firstCard = null , secondCard = null ;
    public Action CardMatchFound,WrongCardTurn, LevelOver,NewLevelStarted;
    int gameRow=3 , gameColumn=2;
    List<Sprite> symbolPairs;
    List<CardPrefabScript> cardPrefabList=new();

    #endregion
    
    #region Singleton
    public static CardManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion

    public bool CheckGridPossible(int row, int column)
    {
        int gridCount = row * column;
        if (gridCount%2 != 0)
        {
            PopupManager.Instance.ShowPopup("Grid size must be even.");
            return false;
        }
        if (gridCount / 2 > cardSymbols.Count)
        {
            PopupManager.Instance.ShowPopup("No enough symbols for the grid.");
            return false;
        }
        gameColumn = column; gameRow = row;
        return  true;
    }
    
    public void InstantiateCards()
    {
        ClearCards();
        RandomiseCards();
        ArrangeCards();
        StartCoroutine(WaitAndCloseAllCards());
    }

    private void ClearCards()
    {
        for (int i = cardAreaTransform.childCount-1; i >= 0; i--)
            Destroy(cardAreaTransform.GetChild(i).gameObject);
    }

    private void RandomiseCards()
    {
        symbolPairs = new List<Sprite>();
        for (int i = 0; i < gameRow * gameColumn / 2; i++)
        {
            symbolPairs.Add(cardSymbols[i]);
            symbolPairs.Add(cardSymbols[i]);
        }
        
        //shuffle the symbol pairs
        for (int i = 0; i < symbolPairs.Count; i++)
        {
            int randomIndex = Random.Range(i, symbolPairs.Count);
            (symbolPairs[i], symbolPairs[randomIndex]) = (symbolPairs[randomIndex], symbolPairs[i]);
        }
    }

    private void ArrangeCards()
    {
        int cardIndex = 0;
        for (int i = 0; i < gameRow; i++)
        {
            GameObject row = Instantiate(rowPrefab, cardAreaTransform);
            for (int j = 0; j < gameColumn; j++)
            {
                GameObject card = Instantiate(cardPrefab, row.transform);
                CardPrefabScript cardScript = card.GetComponent<CardPrefabScript>();
                cardPrefabList.Add(cardScript);
                cardScript.SetSymbol(symbolPairs[cardIndex++]);
            }
        }
    }
    public void CardFlipCalled(CardPrefabScript card)
    {
        SoundManager.Instance.PlayCardFlipSound();
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            CheckMatch();
        }
        else
        {
            Debug.LogWarning("Card Flipping in Progress"+card.symbolSprite.name+" "+isCardFlipping);
        }
    }

    public void StartNewGame()
    {
        scoreScript.ResetScore();
        scoreScript.ResetLevel();
        InstantiateCards();
    }

    public void StartNewLevel()
    {
        NewLevelStarted?.Invoke();
        InstantiateCards();
    }

    private void CheckMatch()
    {
        if (firstCard==null || secondCard==null)return;
        
        isCardFlipping = true;
        if (firstCard.symbolSprite == secondCard.symbolSprite)
        {
            CardMatchFound?.Invoke();
            SoundManager.Instance.CardMatchSound();
            StartCoroutine(WaitAndRemoveCard());
        }
        else
        {
            WrongCardTurn?.Invoke();
            SoundManager.Instance.CardWrongSound();
            StartCoroutine(WaitAndCloseTwoCard());
        }
    }

    public bool CheckCardOpen()
    {
        int count = 0;
        foreach (var cards in cardPrefabList)
        {
            if (cards.isOpened) count++;
            if (count >= 2) return true;
        }
        return false;
    }

    IEnumerator WaitAndCloseTwoCard()
    {
        yield return new WaitForSeconds(cardCheckDelay);
        firstCard.CloseCard();
        secondCard.CloseCard();
        SoundManager.Instance.PlayCardFlipSound();
        firstCard = secondCard = null;
    }
    IEnumerator WaitAndCloseAllCards()
    {
        yield return new WaitForSeconds(cardCheckDelay);
        SoundManager.Instance.PlayCardFlipSound();
        foreach (var card in cardPrefabList) card.CloseCard();
    }
    IEnumerator WaitAndRemoveCard()
    {
        yield return new WaitForSeconds(cardCheckDelay);
        firstCard.RemoveCard();
        cardPrefabList.Remove(firstCard);
        secondCard.RemoveCard();
        cardPrefabList.Remove(secondCard);
        isCardFlipping = false;
        firstCard = secondCard = null;
        
        if (cardPrefabList.Count ==0)LevelOver?.Invoke();
    }
}
