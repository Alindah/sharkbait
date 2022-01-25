using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Main UI")]
    public int score = 0;
    public int lives = 3;
    public Text scoreText;
    public GameObject healthContainer;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject losePanel;

    [Header("Difficulty")]
    public BirdSpawner birdSpawner;
    public SharkSpawner sharkSpawner;
    public float timeUntilRampUp = 60.0f;
    public float sharkSpeedIncrement = 0.1f;
    public float maxSharkSpeed = 1.5f;
    public int levelsUntilAdditionalSharks = 3;
    public int maxSpawn = 3;

    [Header("Misc")]
    public float wilsonSpawnChance = 5.0f;
    
    private Image[] lifeImages;
    private int maxHealth;
    private string scoreTextString;
    private bool gameIsPaused = false;
    private bool wilsonIsActive = false;
    private int level = 1;

    void Start()
    {
        // Reset variables
        Time.timeScale = 1;

        maxHealth = healthContainer.transform.childCount;
        lifeImages = healthContainer.transform.GetComponentsInChildren<Image>();
        scoreTextString = scoreText.text;
        UpdateUIScore();
        InvokeRepeating("RampUpDifficulty", timeUntilRampUp, timeUntilRampUp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu();
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool GetPauseStatus()
    {
        return gameIsPaused;
    }

    public void SetWilsonActive(bool isActive)
    {
        wilsonIsActive = isActive;
    }

    public bool WilsonIsActive()
    {
        return wilsonIsActive;
    }

    public void UpdateScore(int points)
    {
        score += points * level;
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
        Text loseScreenText = losePanel.GetComponentInChildren<Text>();
        string loseMessage = loseScreenText.text;
        
        PauseGame();
        loseScreenText.text = String.Format(loseMessage, score);
        losePanel.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    private void TogglePauseMenu()
    {
        if (losePanel.activeSelf)
            return;

        pausePanel.SetActive(!pausePanel.activeSelf);

        Time.timeScale = gameIsPaused ? 1 : 0;
        gameIsPaused = !gameIsPaused;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void RampUpDifficulty()
    {
        // Increase speed of each shark if it has not reached max speed yet;
        foreach (SharkController sharkController in sharkSpawner.transform.GetComponentsInChildren<SharkController>())
            sharkController.speed = sharkController.speed * level < maxSharkSpeed ? sharkController.speed * level + sharkSpeedIncrement : maxSharkSpeed;
        
        // Increase spawn limit for sharks 
        if (level % levelsUntilAdditionalSharks == 0 && sharkSpawner.spawnLimit < maxSpawn)
            sharkSpawner.spawnLimit++;

        level++;
    }
}
