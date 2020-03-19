using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 10f;
    private float countdown = 5f;

    public Text waveCountdownText;

    public static int waveNumber = 0;
    private Enemy enemy;
    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }else if (countdown == 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        stats.Rounds++;

        Wave wave = waves[waveNumber%3];

        for (int i = 0; i< wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/ wave.rate);
        }
        waveNumber++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        EnemiesAlive++;
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        //Debug.Log(EnemiesAlive);
    }
}
