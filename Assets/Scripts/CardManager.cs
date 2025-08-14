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
    [SerializeField] float cardCheckDelay = 1f;
    //Non-Serialized
    [NonSerialized] public bool isCardFlipping = false;
    [NonSerialized] public CardPrefabScript firstCard = null , secondCard = null ;
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
    
    private void Start()
    {
        InstantiateCards();
    }

    public bool CheckGridPossible(int row, int column)
    {
        int gridCount = row * column;
        if (gridCount%2 != 0)
        {
            Debug.Log("Grid size must be even.");
            return false;
        }
        if (gridCount / 2 > cardSymbols.Count)
        {
            Debug.Log("No enough symbols for the grid.");
            return false;
        }
        gameColumn = column; gameRow = row;
        return  true;
    }
    
    public void InstantiateCards()
    {
        RandomiseCards();
        ArrangeCards();
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
        if (firstCard == null)
        {
            firstCard = card;
            Debug.Log("First Card Flipped: " + firstCard.symbolSprite.name);
        }
        else if (secondCard == null)
        {
            secondCard = card;
            CheckMatch();
        }
        else
        {
            firstCard = card;
            secondCard = null;
        }
    }

    private void CheckMatch()
    {
        if (firstCard==null || secondCard==null)return;
        
        isCardFlipping = true;
        if (firstCard.symbolSprite == secondCard.symbolSprite)
        {
            Debug.Log("Card Match Found!");
            StartCoroutine(WaitAndRemoveCard());
        }
        else
        {
            Debug.Log("Card No Match!");
            StartCoroutine(WaitAndCloseCard());
        }
    }

    IEnumerator WaitAndCloseCard()
    {
        yield return new WaitForSeconds(cardCheckDelay);
        firstCard.CloseCard();
        secondCard.CloseCard();
        firstCard = secondCard = null;
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
    }
}
