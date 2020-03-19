using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    public GameObject ui;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            audio.Pause();
            Time.timeScale = 0f;

        }
        else
        {
            audio.Play();
            Time.timeScale = 1f;
        }
    }
    public void Retry_Nomal()
    {
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.waveNumber = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tower Defense_Nomal");
    }

    public void Menu()
    {
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.waveNumber = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tower Defense_Menu");
    }
    public void Retry_Winter()
    {
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.waveNumber = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("TowerDefense_Snow");
    }
    public void Retry_Forest()
    {
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawner.waveNumber = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tower Defense_Forest");
    }
}
