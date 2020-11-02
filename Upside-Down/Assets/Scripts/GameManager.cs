﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void endGame() {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game over");
    }
}
