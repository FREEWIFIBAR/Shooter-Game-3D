using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}