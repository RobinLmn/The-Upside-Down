using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool cursor;
    public int mainGameNo;
    public int creditsNo;
    void Start()
    {
        if (!cursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(mainGameNo);
    }
    public void Shut()
    {
        Application.Quit();
    }
    public void URL(string url)
    {
        Application.OpenURL(url);
    }
    public void Cred()
    {
        SceneManager.LoadScene(creditsNo);
    }

   private void Update()
   {
       if (Input.GetButtonDown("Fire1") && !cursor)
           Play();
   }
}
