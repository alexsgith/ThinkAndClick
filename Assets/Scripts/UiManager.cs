using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject levelUpScreen;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] TMP_Text levelTexts;
    [SerializeField] TMP_Text matchTexts;
    [SerializeField] TMP_Text turnTexts;
    [SerializeField] TMP_Text levelOverText;
    [SerializeField] Button nextLevelBtn;
    [SerializeField] Button GoHomeBtn;
    [SerializeField] Button StartGameBtn;
    [SerializeField] Button ExitGameBtn;
    
    GameState currentGameState;
    private void Start()
    {
        SetGameState(GameState.Menu);
    }

    private void OnEnable()
    {
        CardManager.Instance.LevelOver += LevelOverCalled;
    }
    private void OnDisable()
    {
        CardManager.Instance.LevelOver -= LevelOverCalled;
    }

    private void LevelOverCalled()
    {
        SetGameState(GameState.LevelUp);
        levelOverText.text = $"In Level {levelTexts.text}  you had {matchTexts.text} Matches in {turnTexts.text} Turns";
    }

    private void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                menuScreen.SetActive(true);
                gameScreen.SetActive(false);
                levelUpScreen.SetActive(false);
                break;
            case GameState.GameStart:
                gameScreen.SetActive(true);
                menuScreen.SetActive(false);
                levelUpScreen.SetActive(false);
                break;
            case GameState.LevelUp:
                levelUpScreen.SetActive(true);
                menuScreen.SetActive(false);
                gameScreen.SetActive(false);
                break;
            default:
                Debug.Log("State Not Defined: " + state);
                break;
        }
    }
}
public enum GameState
{
    Menu,
    GameStart,
    LevelUp
}
