using UnityEngine;
using UnityEngine.UI;

public class SetDifficutly : MonoBehaviour
{

    Button difficultyButton;
    private GameManager gameManager;
    public int difficulty;

    private void Start()
    {
        difficultyButton = this.GetComponent<Button>();
        gameManager = GameObject.Find("GAME_MANAGER").GetComponent<GameManager>();
        difficultyButton.onClick.AddListener(SetDifficultyLevel);
    }

    private void SetDifficultyLevel()
    {
        gameManager.StartGame(difficulty);
        Debug.Log(difficultyButton.name);
    }
}
