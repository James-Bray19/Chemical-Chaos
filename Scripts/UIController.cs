using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Text timerText;
	private float timer;
	public Text potionText;
	public static int potionCount;
    public Text heatText;
    public static int heatLevel;

    void Update()
	{
		timer += Time.deltaTime;
		int minutes = Mathf.FloorToInt(timer / 60F);
		int seconds = Mathf.FloorToInt(timer % 60F);
		int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
		timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");

		potionText.text = potionCount.ToString();
        heatText.text = heatLevel.ToString() + "% (x" + (0.2f * PotionSelector.lostPotionCount + 1).ToString() + ")";
    }
}
