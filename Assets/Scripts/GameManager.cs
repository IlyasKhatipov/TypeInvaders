using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints;
    public int rewardForOneKilledEnemy = 1;
    public float spawnInterval = 2f;
    public GameObject LoseCanvas;
    public TextMeshProUGUI scoresTextField;
    public TextMeshProUGUI bestScoresField;
    public TextMeshProUGUI bestScoresAmount;
    private float timeSinceLastSpawn = 0f;
    private int currentScores = 0;


    void Start() 
    {
        LoseCanvas.SetActive(false);
        bestScoresField.gameObject.SetActive(false);
        bestScoresAmount.gameObject.SetActive(false);
        scoresTextField.text = "Scores: " + currentScores.ToString();

    }
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime; 
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f; 
        }
    }

    public void UpdateScores() 
    {
        currentScores += rewardForOneKilledEnemy;
        scoresTextField.text = "Scores: " + currentScores.ToString();
    }

    public void SaveBestScores() 
    {
        int best = PlayerPrefs.GetInt("BestScores", 0);
        if (currentScores > best) 
        {
            PlayerPrefs.SetInt("BestScores", currentScores);
            PlayerPrefs.Save();
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void StopGame() 
    {
        Time.timeScale = 0f;
        LoseCanvas.SetActive(true);
        int best = PlayerPrefs.GetInt("BestScores", 0);
        if (currentScores > best)
        {
            bestScoresField.gameObject.SetActive(true);
            bestScoresAmount.gameObject.SetActive(true);
            bestScoresAmount.text = currentScores.ToString();
        }
        SaveBestScores();
    }
}
