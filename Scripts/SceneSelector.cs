using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public static int score;
    public void playGame() { SceneManager.LoadScene("SampleScene"); }
    public void playMenu() { SceneManager.LoadScene("Menu"); }
    public void playSettings() { SceneManager.LoadScene("Settings"); }
    public void playTutorial() { SceneManager.LoadScene("Tutorial"); }
    public void playHighscores() { SceneManager.LoadScene("ScoreBoard"); }
    public void exitGame() { Application.Quit(); }
    public void Pause() { Time.timeScale = 0; AudioListener.pause = true; }
    public void Unpause() { Time.timeScale = 1; AudioListener.pause = false; }
}