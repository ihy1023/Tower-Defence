using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    float speed1;

    float hp = 100;
    public float starthp = 100;

    public int money=50;
    public Image HPBAR;
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;
    public static float hpp=0;
    private float num=0;

    void Start()
    {
        target = Waypoints.points[0];
        hp = starthp;
        speed1 = speed;
        hpup();

    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        HPBAR.fillAmount = hp / starthp;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed1 = speed*(1f - pct);
    }

    void Die()
    {
        stats.money += money;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        //Debug.Log(WaveSpawner.EnemiesAlive);
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed1 * Time.deltaTime, Space.World);
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            endPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void endPath()
    {
        stats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        //Debug.Log(WaveSpawner.EnemiesAlive);
    }

    public void hpup()
    {
        hpp = 0;
        hpp = WaveSpawner.waveNumber * 50;
        hp =hp + hpp;
        starthp = hp;
    }
}
