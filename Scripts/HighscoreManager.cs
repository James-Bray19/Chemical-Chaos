using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public List<int> highScores;
    public static int score;
    public List<Text> highScoreTexts;
    public float time;

    void Start()
    {
        highScores = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("Highscore" + i.ToString()));
        }
        highScores.Sort(); highScores.Reverse();

        for (int i = 0; i < 5; i++)
        {
            if (score > highScores[i])
            {
                highScores.Insert(i, score);
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Highscore" + i.ToString(), highScores[i]);
            Debug.Log(highScores[i].ToString() + PlayerPrefs.GetInt("Highscore" + i.ToString()).ToString());
            highScoreTexts[i].text = highScores[i].ToString() + " potions";
        }
        score = 0;
    }

    public static void UpdateHighscore(int updateScore) { score = updateScore; }
}
