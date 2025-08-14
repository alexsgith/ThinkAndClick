using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] List<Sprite> cardSymbols;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardAreaTransform;
    [SerializeField] GameObject rowPrefab;
    [SerializeField] int gameRow,gameColumn=2;

    List<Sprite> symbolPairs;

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
                cardScript.SetSymbol(symbolPairs[cardIndex++]);
            }
        }
    }
}
