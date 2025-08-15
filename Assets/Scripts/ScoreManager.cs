using UnityEngine;

//only current score is managed due to time restrict, future can impliment total score based system
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int level = 0;
    [SerializeField] private int matches = 0;
    [SerializeField] private int turns = 0;
    private int totalScore = 0;
    private bool resetScoreOnLevel = true;
    
    void Start()
    {
        ResetScore();
        ResetLevel();
    }

    private void OnEnable()
    {
        CardManager.Instance.CardMatchFound += IncrementMatches;
        CardManager.Instance.WrongCardTurn += IncrementTurns;
        CardManager.Instance.LevelOver += IncrementLevel;
    }
    private void OnDisable()
    {
        CardManager.Instance.CardMatchFound -= IncrementMatches;
        CardManager.Instance.WrongCardTurn -= IncrementTurns;
        CardManager.Instance.LevelOver -= IncrementLevel;
    }

    private void ResetLevel()
    {
        level = 0;
    }

    private void ResetScore()
    {
        matches = turns = totalScore = 0;
    }
    private void IncrementMatches()
    {
        matches++;
    }
    private void IncrementTurns()
    {
        turns++;
    }
    private void IncrementLevel()
    {
        level++;
        if(resetScoreOnLevel)ResetScore();
    }
    //for future implimentaion if needed
    private void CalculateScore()
    {
        totalScore = matches - turns + level;
        Debug.Log($"Level: {level}, Matches: {matches}, Turns: {turns}, Total Score: {totalScore}");
    }
}
