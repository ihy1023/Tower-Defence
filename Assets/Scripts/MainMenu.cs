using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject MAPui;
    public GameObject plays;
    public GameObject exits;
    public GameObject backs;
    public GameObject Nomals;
    public GameObject Winters;
    public GameObject Forests;


    public void play()
    {
        ui.SetActive(false);
        MAPui.SetActive(true);
        plays.SetActive(false);
        exits.SetActive(false);
        Nomals.SetActive(true);
        Winters.SetActive(true);
        Forests.SetActive(true);
        backs.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void back()
    {
        MAPui.SetActive(false);
        ui.SetActive(true);
        plays.SetActive(true);
        exits.SetActive(true);
        Nomals.SetActive(false);
        Winters.SetActive(false);
        Forests.SetActive(false);
        backs.SetActive(false);
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
