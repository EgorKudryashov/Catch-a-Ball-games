using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainCamera;
    static private float musicVolume = 0.2f;

    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    private int difficultyMod;

    private int score;
    public TextMeshProUGUI scoreText;
    private int addLivesForScore = 100;
    private int livesBonus;

    public int lives;
    public TextMeshProUGUI livesText;

    public GameObject titleScreen;
    public Slider volumeSlider;
    public Button restartButton;
    public TextMeshProUGUI gameOverText;
    public GameObject helpPanel;

    public bool isPauseGame;
    public TextMeshProUGUI pauseText;

    public bool isGameActive;

    private void Start()
    {
        volumeSlider.value = musicVolume;
        ChangeVolume();
    }

    public void StartGame(int diffuculty)
    {
        isGameActive = true;
        isPauseGame = false;
        score = 0;
        lives = 3;
        livesBonus = addLivesForScore * diffuculty;
        difficultyMod = diffuculty;
        spawnRate /= diffuculty;

        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        PauseGame();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore (int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score >= livesBonus)
        {
            UpdateLives(1);
            livesBonus += addLivesForScore * difficultyMod;
        }
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeVolume()
    {
        musicVolume= volumeSlider.value;
        mainCamera.GetComponent<AudioSource>().volume = musicVolume;
    }

    public void PauseGame()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isPauseGame)
            {
                Time.timeScale = 0.0f;
                isPauseGame = true;
                pauseText.gameObject.SetActive(true);

            } else if (Input.GetKeyDown(KeyCode.Space) && isPauseGame)
            {
                Time.timeScale = 1;
                isPauseGame = false;
                pauseText.gameObject.SetActive(false);
            }
        }
    }

    public void ShowHelpPanel()
    {
        helpPanel.gameObject.SetActive(!helpPanel.activeSelf);
    }

}
