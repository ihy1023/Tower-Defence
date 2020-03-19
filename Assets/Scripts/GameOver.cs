using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text rounds;
    void OnEnable()
    {
        Time.timeScale = 0f;
        rounds.text = stats.Rounds.ToString();
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        WaveSpawner.EnemiesAlive = 0;
        SceneManager.LoadScene("Tower Defense_Menu");
    }
    public void Nomal()
    {
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.waveNumber = 0;
        Enemy.hpp = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tower Defense_Nomal");
    }
    public void Winter()
    {
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.waveNumber = 0;
        Enemy.hpp = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("TowerDefense_Snow");
    }
    public void Forest()
    {
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.waveNumber = 0;
        Enemy.hpp = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tower Defense_Forest");
    }

}
