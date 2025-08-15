using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    #region Singleton
    public static PopupManager Instance { get; private set; }
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
    [SerializeField]Color defaultMsgColor;
    [SerializeField]TMP_Text messageText;
    [SerializeField]GameObject popupObject;
    
    public void ShowPopup(string message)
    {
        messageText.text = message;
        popupObject.SetActive(true);
    }
}
