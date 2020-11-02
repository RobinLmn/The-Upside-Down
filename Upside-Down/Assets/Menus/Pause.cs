using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    bool paused = false;
    public GameObject pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused && Time.timeScale > 0)
            {
                Time.timeScale = 0;
                paused = true;
                pauseMenu.SetActive(true);
            }
            else if(paused)
            {
                Time.timeScale = 1;
                paused = false;
                pauseMenu.SetActive(false);
            }
        }

        if (Input.GetButtonDown("Submit") && paused)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
            SceneManager.LoadScene(0);
        }
    }
}
