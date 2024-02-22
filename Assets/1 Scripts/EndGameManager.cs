using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameType
{
    Moves,
    Time
}

[System.Serializable]
public class EndGameRequirements
{
    public GameType gameType;
    public int counterValue;
}

public class EndGameManager : MonoBehaviour
{
    public GameObject movesLabel, timeLabel;
    public GameObject retryPanel, winPanel;
    public Text counter;
    public Text winScoreT, winStarT, loseScoreT, loseStarT;
    public EndGameRequirements requirements;
    public int currentCounterValue;

    Board board;
    ScoreManager scoreManager;
    float timerSeconds;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        scoreManager = FindObjectOfType<ScoreManager>();
        SetGameType();
        SetupGame();
    }

    void SetGameType()
    {
        if (board.world != null)
        {
            if (board.level < board.world.levels.Length)
            {
                if (board.world.levels[board.level] != null)
                {
                    requirements = board.world.levels[board.level].endGameRequirements;
                }
            }                
        }
    }

    void SetupGame()
    {
        currentCounterValue = requirements.counterValue;
        if(requirements.gameType == GameType.Moves)
        {
            movesLabel.SetActive(true);
            timeLabel.SetActive(false);
        }
        else
        {
            timerSeconds = 1;
            timeLabel.SetActive(true);
            movesLabel.SetActive(false);
        }
        counter.text = "" + currentCounterValue;
    }

    public void DecreaseCounterValue()
    {
        if (board.currentState != GameState.pause)
        {
            currentCounterValue--;
            counter.text = "" + currentCounterValue;

            if (currentCounterValue == 0)
            {
                LoseGame();
            }
        }
    }

    public void WinGame()
    {
        winScoreT.text = scoreManager.score + "";
        int stars = 0;
        if (scoreManager.score >= 3000)
        {
            stars = 3;
        }
        else if (scoreManager.score >= 2000)
        {
            stars = 2;
        }
        else if (scoreManager.score >= 1000)
        {
            stars = 1;
        }
        winStarT.text = stars + " / 3";
        winPanel.SetActive(true);
        board.currentState = GameState.win;
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    public void LoseGame()
    {
        loseScoreT.text = scoreManager.score + "";
        int stars = 0;
        if (scoreManager.score >= 3000)
        {
            stars = 3;
        }
        else if (scoreManager.score >= 2000)
        {
            stars = 2;
        }
        else if (scoreManager.score >= 1000)
        {
            stars = 1;
        }
        loseStarT.text = stars + " / 3";
        retryPanel.SetActive(true);
        board.currentState = GameState.lose;
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    void Update()
    {
        if(requirements.gameType == GameType.Time && currentCounterValue > 0)
        {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }
}
