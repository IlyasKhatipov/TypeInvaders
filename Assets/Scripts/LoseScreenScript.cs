using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoseScreenScript : MonoBehaviour
{

    public void OnRestartButtonPressed() 
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
        Time.timeScale = 1.0f;
    }

    public void OnMenuButtonPressed() 
    {
        SceneManager.LoadScene("Menu");
    }
}
