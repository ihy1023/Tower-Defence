using System.Collections;
using UnityEngine;

public class stats : MonoBehaviour
{
    public static int money;
    public int startmoney = 200;

    public static int Lives;
    public int startLives= 20;

    public static int Rounds;
    
    void Start()
    {
        money = startmoney;
        Lives = startLives;

        Rounds = 0;
    }

}
