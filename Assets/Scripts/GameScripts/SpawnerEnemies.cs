using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameMode
{
    Easy,
    Normal,
    Hard
}
public class SpawnerEnemies : MonoBehaviour
{
    public GameObject[] ghostPrefabs; 
    public float spawnTime = 1.0f;
    private Coroutine spawnCoroutine;

    public TextMeshProUGUI killsText; 
    public TextMeshProUGUI timerText; 
    private int killsCount = 0; 

    public float gameTime = 120f; 
    private float remainingTime;
    private int maxGhostsToCatch = 30; 
    private int totalGhostsSpawned = 0;

    public GameMode currentGameMode;

    private void Start()
    {
        killsText.text = "Kills: 0";
        timerText.text = "Time: " + gameTime.ToString("F0"); 
        remainingTime = gameTime;
        StartSpawning();
        ResetSpawner();
    }
    public void ResetSpawner()
    {
        killsCount = 0;
        totalGhostsSpawned = 0;
        remainingTime = gameTime;

        killsText.text = "Kills: 0";
        timerText.text = "Time: " + gameTime.ToString("F0");

        StopSpawning(); 
        StartSpawning(); 
    }
    private void Update()
    {
        UpdateTimer();
        CheckWinCondition();
    }
    private void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Max(Mathf.Floor(remainingTime), 0).ToString("F0");

        if (remainingTime <= 0 && killsCount < maxGhostsToCatch)
        {
            EndGame(false); 
        }
    }
    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            Debug.Log("Iniciando generación de enemigos.");
            spawnCoroutine = StartCoroutine(SpawnGhosts());
        }
    }
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            Debug.Log("Deteniendo generación de enemigos.");
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
    private IEnumerator SpawnGhosts()
    {
        int enemiesToSpawn = 0;

        switch (currentGameMode)
        {
            case GameMode.Easy:
                enemiesToSpawn = 30; // Solo enemigos grandes(puede variar)
                break;
            case GameMode.Normal:
                enemiesToSpawn = 100; // 50 grandes y 50 pequeños(puede variar)
                break;
            case GameMode.Hard:
                enemiesToSpawn = 200; // 100 grandes y 100 pequeños(puede variar)
                break;
        }

        while (totalGhostsSpawned < enemiesToSpawn)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            GameObject ghostPrefab = ghostPrefabs[Random.Range(0, ghostPrefabs.Length)];

            if (currentGameMode == GameMode.Easy)
            {
                ghostPrefab = ghostPrefabs[0]; 
            }
            else if (currentGameMode == GameMode.Normal)
            {
                ghostPrefab = (Random.Range(0, 2) == 0) ? ghostPrefabs[0] : ghostPrefabs[1];
            }
            else if (currentGameMode == GameMode.Hard)
            {
                ghostPrefab = (Random.Range(0, 2) == 0) ? ghostPrefabs[0] : ghostPrefabs[1];
            }

            GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
            EnemiesControl enemyControl = ghost.GetComponent<EnemiesControl>();
            if (enemyControl != null)
            {
                enemyControl.spawner = this;

                // según el modo
                if (currentGameMode == GameMode.Hard && ghostPrefab == ghostPrefabs[0])
                {
                    enemyControl.clicksToKill = 3; 
                }
                ghost.tag = (ghostPrefab == ghostPrefabs[0]) ? "Grande" : "Pequeño";
                enemyControl.SetGhostProperties();
            }

            totalGhostsSpawned++;
            yield return new WaitForSeconds(spawnTime);
        }
    }
    private Vector2 GetRandomSpawnPosition()
    {
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 2;
        float cameraWidth = cameraHeight * cam.aspect;

        Vector2 spawnPosition;
        if (Random.value > 0.5f)
        {
            spawnPosition = new Vector2(Random.Range(-cameraWidth - 1f, -cameraWidth), Random.Range(-cameraHeight / 2, cameraHeight / 2)); 
        }
        else
        {
            spawnPosition = new Vector2(Random.Range(cameraWidth, cameraWidth + 1f), Random.Range(-cameraHeight / 2, cameraHeight / 2)); 
        }

        return spawnPosition;
    }
    public void IncreaseKillCount()
    {
        killsCount++;
        killsText.text = "Kills: " + killsCount.ToString();
    }

    private void EndGame(bool win)
    {
        StopSpawning();

        if (win)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
        }
        else
        {
            MusicManager.Instance.StopAllMusic();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            SfxManager.Instance.PlayGameOverSound();
        }
    }
    private void CheckWinCondition()
    {
        if (killsCount >= maxGhostsToCatch)
        {
            EndGame(true); 
        }
    }
    public void SpawnEnemy(Vector3 spawnPosition)
    {
        spawnPosition.z = 1;

        int randomIndex = Random.Range(0, ghostPrefabs.Length);
        GameObject enemyToSpawn = ghostPrefabs[randomIndex];

        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }
}