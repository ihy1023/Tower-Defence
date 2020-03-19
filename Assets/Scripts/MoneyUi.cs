using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MoneyUi : MonoBehaviour
{
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$"+stats.money.ToString();

    }
}
