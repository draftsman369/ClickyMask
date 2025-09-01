using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject titleScreen;
    private int score;
    public bool isGameActive;
    public float spawnRate = 1;

    void Start()
    {
        isGameActive = false;
    
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

    public void GameOver()
    {
        isGameActive = false;
        restartButton.SetActive(true);
        gameOverText.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        score = 0;
        isGameActive = true;
        titleScreen.SetActive(false);
        spawnRate /= difficulty;
        StartCoroutine(spawnTarget());
        UpdateScore(0);
    }

}
