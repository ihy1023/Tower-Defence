using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUi;

    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return;

        if (stats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUi.SetActive(true);
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        WaveSpawner.EnemiesAlive = 0;
        SceneManager.LoadScene("Tower Defense_Menu");
    }
}
