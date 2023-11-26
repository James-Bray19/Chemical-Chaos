using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public SceneSelector scene;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { scene.Pause(); pauseMenu.SetActive(true); }
    }
}
