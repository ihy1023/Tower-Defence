using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class liveUi : MonoBehaviour
{
    public Text liveText;

    // Update is called once per frame
    void Update()
    {
        liveText.text = "Life : " + stats.Lives.ToString();

    }
}