using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefabScript : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite cardBackSprite;
    public Sprite symbolSprite;
    public float flipSpeed = 10;
    public bool isOpened = true;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnCardClicked);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        if(CardManager.Instance.isCardFlipping || isOpened ||CardManager.Instance.CheckCardOpen())return;
        Debug.Log(CardManager.Instance.isCardFlipping+" "+ isOpened);
        CardManager.Instance.CardFlipCalled(this);
        StartCoroutine(FlipCard());
    }

    public void SetSymbol(Sprite sprite)
    {
        symbolSprite = sprite;
        cardImage.sprite = symbolSprite;
    }

    public void CloseCard()
    {
        if (!isOpened)return;
        StartCoroutine(FlipCard());
    }

    IEnumerator FlipCard()
    {
        CardManager.Instance.isCardFlipping = true;
        isOpened = !isOpened;
        float totalRotation = 0f;
        while (totalRotation <= 90f)
        {
            float rotationStep = flipSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationStep);
            totalRotation += rotationStep;
            yield return null;
        }
        cardImage.sprite = isOpened ? symbolSprite : cardBackSprite;
        while (totalRotation >= 0f)
        {
            float rotationStep = flipSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, -rotationStep);
            totalRotation -= rotationStep;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        CardManager.Instance.isCardFlipping = false;
    }

    public void RemoveCard()
    {
        GetComponent<Image>().color = Color.clear;
    }
}
