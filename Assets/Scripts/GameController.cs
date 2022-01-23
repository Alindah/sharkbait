using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;
    public Text scoreText;
    public GameObject healthContainer;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private Image[] lifeImages;
    private int maxHealth;
    private string scoreTextString;
    private bool gameIsPaused = false;

    void Start()
    {
        maxHealth = healthContainer.transform.childCount;
        lifeImages = healthContainer.transform.GetComponentsInChildren<Image>();
        scoreTextString = scoreText.text;
        UpdateUIScore();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool GetPauseStatus()
    {
        return gameIsPaused;
    }

    public void UpdateScore(int points)
    {
        score += points;
        UpdateUIScore();
    }

    public void UpdateHealth(int lifeDifference)
    {
        lives += lifeDifference;
        UpdateUIHealth(lifeDifference);

        if (lives <= 0)
            ShowGameOver();
    }

    private void UpdateUIScore()
    {
        scoreText.text = String.Format(scoreTextString, score);
    }

    private void UpdateUIHealth(int lifeDifference)
    {
        if (lifeDifference < 0)
            lifeImages[lives].sprite = emptyHeart;
        else
            lifeImages[lives - 1].sprite = fullHeart;
    }

    private void ShowGameOver()
    {
        Debug.Log("GAME OVER, MAN, GAME OVER!");
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    private void TogglePause()
    {
        Time.timeScale = gameIsPaused ? 1 : 0;
        gameIsPaused = !gameIsPaused;
    }
}
