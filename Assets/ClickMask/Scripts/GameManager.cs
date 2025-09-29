using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject titleScreen;
    private int score;
    private int highScore = 0;
    public bool isGameActive;
    public float minSpawnRate = 0.1f;
    public float spawnRate = 1f;
    public float spawnRateTick = 10f;
    public float spawnRateMultiplierPerTick = 0.9f;


    private void Awake()
    {
        LoadHighScore();
    }
    void Start()
    {
        isGameActive = false;
        scoreText.text = "Best Score: " + highScore;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    IEnumerator spawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }
    IEnumerator IncreaseDifficultyLoop()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRateTick);

            // make spawns faster, but clamp to a minimum
            spawnRate = Mathf.Max(minSpawnRate, spawnRate * spawnRateMultiplierPerTick);
            // Optional: Debug.Log($"New spawnRate: {spawnRate}");
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
        highScoreText.text = "Best Score: " + highScore;
        restartButton.SetActive(true);
        gameOverText.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        scoreText.text = "Score: 0";
        score = 0;
        isGameActive = true;
        titleScreen.SetActive(false);
        spawnRate /= difficulty;
        StartCoroutine(spawnTarget());
        StartCoroutine(IncreaseDifficultyLoop());
        UpdateScore(0);
    }


    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }
}

[System.Serializable]
public class SaveData
{
    public int highScore;
}

