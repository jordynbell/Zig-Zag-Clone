using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        highScoreText.text = "Best: " + GetHighScore().ToString();
    }

    [System.Obsolete]
    public void StartGame()
    {
        gameStarted = true;
        FindObjectOfType<Road>().StartBuilding();
    }

    [System.Obsolete]
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) 
        {
            StartGame();  
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score.ToString();
        }
    }

    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("HighScore");
        return i;
    }

}
