using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;
    private int score;
    public bool isGameActive;

    void Start()
    {
        isGameActive = true;
        gameOverText.SetActive(false);
        StartCoroutine(spawnTarget());
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
            yield return new WaitForSeconds(1.0f);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.SetActive(true);
    }

}
