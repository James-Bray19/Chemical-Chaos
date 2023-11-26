using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public Text text;
    float timeLeft = 4f;
    public AudioManager audio;

    void Awake()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        int temp = (int)timeLeft;
        timeLeft -= Time.unscaledDeltaTime;
        if (temp != (int)timeLeft) { audio.Select(); text.transform.localScale = text.transform.localScale * 1.4f; }
        text.text = ((int)timeLeft).ToString();
        if (text.text == "0") { text.text = "GO!"; }
        if (timeLeft < 0)
        {
            Time.timeScale = 1;
            audio.PlayTheme();
            Destroy(this.gameObject);
        }
    }
}
