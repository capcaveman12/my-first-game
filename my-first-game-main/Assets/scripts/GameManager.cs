using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static bool isGameOver = false;

    public static bool playerWon = false;


    private void Start()
    {
        Player.playerDeath += GameOver;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(1);
            SpawnManager._stopSpawn = false;

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.R) && playerWon == true)
        {
            SceneManager.LoadScene(1);
            SpawnManager._stopSpawn = false;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

}
