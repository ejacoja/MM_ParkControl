using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreAndLivesUI : MonoBehaviour
{
    public static ScoreAndLivesUI Instance { get; private set; }

    public Text ScoreText = null;
    public int currentScore;

    public int PointsPerPowerup = 10;

    public List<Image> Lives = new List<Image>();
    private int currentLives;

    public UnityEvent GameOver;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void LoseOneLife()
    {
        Lives[currentLives].enabled = false;
        currentLives--;

        if (currentLives < 0)
        {
            //GAME OVER
            GameOver?.Invoke();
        }
    }

    public void AddScore()
    {
        currentScore += PointsPerPowerup;
        ScoreText.text = currentScore.ToString();
    }

    private void Awake()
    {
        Instance = this;
        foreach (Image img in Lives)
        {
            img.enabled = true;
        }
        currentLives = Lives.Count - 1;
        currentScore = 0;
        ScoreText.text = currentScore.ToString();
    }
}
