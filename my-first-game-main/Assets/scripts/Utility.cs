using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public delegate void EnemyCount();
    public static event EnemyCount enemyCount;

    public delegate void NewWave();
    public static event NewWave newWave;

    public static void OnEnemyDeath()
    {
         if(enemyCount != null)
        {
            enemyCount();
        }
    }

    public static void WaveCount()
    {
        if (newWave != null)
            newWave();
    }
}
