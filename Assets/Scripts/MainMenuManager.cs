using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoresField;

    void Start() 
    {
        int best = PlayerPrefs.GetInt("BestScores", 0);
        bestScoresField.text = "Best score: " + best.ToString();
    } 
    public void OnPlayButtonPressed() 
    {
        SceneManager.LoadScene("Level");
        Time.timeScale = 1.0f;
    }

    public void OnExitButtonPressed() 
    {
        Application.Quit();
    }
}
