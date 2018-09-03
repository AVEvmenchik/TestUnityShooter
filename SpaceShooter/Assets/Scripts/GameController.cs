using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private int score;
    private bool restart;
    private bool gameOver;

    private void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore ();

        StartCoroutine(SpawnWaves());


    }



    private void Update()
    {

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
               // Application.LoadLevel (Application.loadedLevel);
                SceneManager.LoadScene("SpaceShooter");

            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                SpawnAsteroid(spawnPosition);
                //RpcSpawnAsteroid(spawnPosition);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }



    [ClientRpc]
    void RpcSpawnAsteroid(Vector3 spawnPosition)
    {
        if (isClient)
            SpawnAsteroid(spawnPosition);
    }


    void SpawnAsteroid(Vector3 spawnPosition)
    {
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazard, spawnPosition, spawnRotation);
    }


    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

}
