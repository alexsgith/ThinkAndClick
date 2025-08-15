using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject levelUpScreen;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private ScoreManager scoreScript;
    [SerializeField] TMP_Text levelTexts;
    [SerializeField] TMP_Text matchTexts;
    [SerializeField] TMP_Text turnTexts;
    [SerializeField] TMP_Text levelOverText;
    [SerializeField] TMP_InputField rowInput;
    [SerializeField] TMP_InputField columnInput;
    [SerializeField] Button nextLevelBtn;
    [SerializeField] Button StartGameBtn;
    [SerializeField] Button ExitGameBtn;
    [SerializeField] List<Button> GoHomeBtn;
    
    GameState currentGameState;
    private void Start()
    {
        SetGameState(GameState.Menu);
    }

    private void OnEnable()
    {
        CardManager.Instance.LevelOver += LevelOverCalled;
        scoreScript.ScoreUpdated += ScoreUpdated;
        nextLevelBtn.onClick.AddListener(OnClickNextLevel);
        StartGameBtn.onClick.AddListener(OnClickStartGame);
        ExitGameBtn.onClick.AddListener(OnClickExitGame);
        rowInput.onValueChanged.AddListener(GridRowValueChanged);
        columnInput.onValueChanged.AddListener(GridColumnValueChanged);
        foreach (var btn in GoHomeBtn)
            btn.onClick.AddListener(OnClickGoHome);
    }

    private void OnDisable()
    {
        CardManager.Instance.LevelOver -= LevelOverCalled;
        scoreScript.ScoreUpdated -= ScoreUpdated;
        nextLevelBtn.onClick.RemoveListener(OnClickNextLevel);
        StartGameBtn.onClick.RemoveListener(OnClickStartGame);
        ExitGameBtn.onClick.RemoveListener(OnClickExitGame);
        rowInput.onValueChanged.RemoveListener(GridRowValueChanged);
        columnInput.onValueChanged.RemoveListener(GridColumnValueChanged);
        foreach (var btn in GoHomeBtn)
            btn.onClick.RemoveListener(OnClickGoHome);
    }

    private void OnClickGoHome()
    {
        SetGameState(GameState.Menu);
    }

    private void ScoreUpdated(int level, int matches, int turns)
    {
        levelTexts.text = level.ToString();
        matchTexts.text = matches.ToString();
        turnTexts.text = turns.ToString();
    }
   

    private void GridRowValueChanged(string value)
    {
        if (value.Trim() == string.Empty || Int32.Parse(value.Trim()) <= 1)
        {
            rowInput.text = "2";
            PopupManager.Instance.ShowPopup("value should be greater than 1");
        }
    }
    private void GridColumnValueChanged(string value)
    {
        if (value.Trim() == string.Empty || Int32.Parse(value.Trim()) <= 1)
        {
            columnInput.text = "2";
            PopupManager.Instance.ShowPopup("value should be greater than 1");
        }
    }

    private void OnClickNextLevel()
    {
        CardManager.Instance.StartNewLevel();
        SetGameState(GameState.GameStart);
    }

    private void OnClickExitGame()
    {
        Application.Quit();
    }

    private void OnClickStartGame()
    {
        int row = Int32.Parse(rowInput.text);
        int column = Int32.Parse(columnInput.text);
        if(!CardManager.Instance.CheckGridPossible(row, column))return;
        SetGameState(GameState.GameStart);
        CardManager.Instance.StartNewGame();
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
