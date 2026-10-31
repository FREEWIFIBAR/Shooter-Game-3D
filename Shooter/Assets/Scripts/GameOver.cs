using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text actualScore, bestScore;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        int score = PlayerPrefs.GetInt("Last Score");
        float best = PlayerPrefs.GetInt("High Score");

        actualScore.text = string.Format("Last Score: {0}", score);
        bestScore.text = string.Format("Best Score: {0}", best);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
}