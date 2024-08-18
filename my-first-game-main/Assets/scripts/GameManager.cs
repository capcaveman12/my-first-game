using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static bool isGameOver = false;


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

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

}
