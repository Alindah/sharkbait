using System;
using System.Collections;
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
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject losePanel;
    public Transform instructionsContainer;
    public GameObject[] instructionPages;

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

    private GameStats gameStats;
    private Image[] lifeImages;
    private int maxHealth;
    private string scoreTextString;
    private bool gameIsPaused = false;
    private bool gameIsRunning = false;
    private bool wilsonIsActive = false;
    private int level = 1;
    private float gameOverDelayTime = 0.01f;
    private const string PAGE_NUMBER_NAME = "PageNumber";
    private const string PANEL_HEADER_NAME = "Header";
    private const string PANEL_STATS_NAME = "Stats";
    private const string PANEL_WILSON = "Wilson";
    private const string GAME_OVER_FUNC = "GameOverDelay";

    void Start()
    {
        PauseGame();
        gameStats = gameObject.GetComponent<GameStats>();
        instructionPages = new GameObject[instructionsContainer.childCount];
        maxHealth = healthContainer.transform.childCount;
        lifeImages = healthContainer.transform.GetComponentsInChildren<Image>();
        scoreTextString = scoreText.text;
        GetInstructionPages();
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
            StartCoroutine(GAME_OVER_FUNC);
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

    public void StartGame()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        gameIsRunning = true;
    }

    private void GetInstructionPages()
    {
        for (int i = 0; i < instructionsContainer.childCount; i++)
        {
            instructionPages[i] = instructionsContainer.GetChild(i).gameObject;
            SetInstructionPageNumber(instructionPages[i], i);
        }
    }

    public void ShowInstructions()
    {
        SwitchPanels(startPanel, instructionPages[0]);
    }

    public void SwitchPanels(GameObject previousPanel, GameObject nextPanel)
    {
        previousPanel.SetActive(false);
        nextPanel.SetActive(true);
    }

    private void SetInstructionPageNumber(GameObject instructionPage, int index)
    {
        Text pageNumText = instructionPage.transform.Find(PAGE_NUMBER_NAME).gameObject.GetComponent<Text>();
        string message = pageNumText.text;
        pageNumText.text = String.Format(message, index + 1, instructionsContainer.childCount);
    }

    // Need delay because scripts execute in wrong order (even after adjustment in Edit > Project Settings > Script Execution Order)
    // Without it, stats will not be updated properly before appearing on the screen
    private IEnumerator GameOverDelay()
    {
        // Note: We do not pause the game until after delay because WaitForSeconds() is affected by Time.timeScale
        gameIsRunning = false;
        yield return new WaitForSeconds(gameOverDelayTime);
        ShowGameOver();
    }

    private void ShowGameOver()
    {
        Text loseScreenText = losePanel.transform.Find(PANEL_HEADER_NAME).GetComponent<Text>();
        Text statsText = losePanel.transform.Find(PANEL_STATS_NAME).GetComponent<Text>();
        string loseMessage = loseScreenText.text;
        string statsMessage = statsText.text;

        PauseGame();
        loseScreenText.text = String.Format(loseMessage, score);
        statsText.text = String.Format(statsMessage,
            gameStats.seagullsKilled, gameStats.swallowsKilled, gameStats.sharksKilled, gameStats.numOfSelfBonks, gameStats.pickupsCollected);
        losePanel.SetActive(true);

        // Show Wilson icon if he was picked up
        if (gameStats.wilsonRescued)
            losePanel.transform.Find(PANEL_WILSON).gameObject.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    private void TogglePauseMenu()
    {
        if (!gameIsRunning)
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
