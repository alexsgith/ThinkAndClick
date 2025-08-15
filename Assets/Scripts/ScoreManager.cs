using System;
using UnityEngine;

//only current score is managed due to time restrict, future can impliment total score based system
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int level = 0;
    [SerializeField] private int matches = 0;
    [SerializeField] private int turns = 0;
    //private int totalScore = 0;
    private bool resetScoreOnLevel = true;
    public Action<int,int,int> ScoreUpdated; 
    
    void Start()
    {
        ResetScore();
        ResetLevel();
    }

    private void OnEnable()
    {
        CardManager.Instance.CardMatchFound += IncrementMatches;
        CardManager.Instance.WrongCardTurn += IncrementTurns;
        CardManager.Instance.NewLevelStarted += IncrementLevel;
    }
    private void OnDisable()
    {
        CardManager.Instance.CardMatchFound -= IncrementMatches;
        CardManager.Instance.WrongCardTurn -= IncrementTurns;
        CardManager.Instance.NewLevelStarted -= IncrementLevel;
    }

    public void ResetLevel()
    {
        level = 0;
        CalculateScore();
    }

    public void ResetScore()
    {
        matches = turns = 0;
        CalculateScore();
    }
    private void IncrementMatches()
    {
        matches++;
        CalculateScore();
    }
    private void IncrementTurns()
    {
        turns++;
        CalculateScore();
    }
    private void IncrementLevel()
    {
        level++;
        if(resetScoreOnLevel)ResetScore();
        CalculateScore();
    }
    //for future implimentaion if needed
    private void CalculateScore()
    {
        //main score logic for future enhancement
        ScoreUpdated?.Invoke(level, matches, turns);
    }
}
