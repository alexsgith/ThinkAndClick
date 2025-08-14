using UnityEngine;
using UnityEngine.UI;

public class CardPrefabScript : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite symbolSprite;
    [SerializeField] private Sprite cardBackSprite;
    public bool isOpened = false;
    
    public void SetSymbol(Sprite sprite)
    {
        cardImage.sprite = sprite;
    }

    public void OpenCard()
    {
        cardImage.sprite = symbolSprite;
        isOpened = true;
    }

    public void CloseCard()
    {
        cardImage.sprite = cardBackSprite;
        isOpened = false;
    }
}
