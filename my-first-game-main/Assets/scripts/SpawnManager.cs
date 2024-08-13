using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    static public bool _stopSpawn = false;

    [SerializeField]
    private GameObject[] _powerUps;

    [SerializeField]
    private GameObject _ammoPlus;

    [SerializeField]
    private GameObject _extraLife;

    [SerializeField]
    private GameObject _laserSaberPowerUp;

    [SerializeField]
     public int enemiesLeft = 10;

    [SerializeField]
     public int level = 1;

    [SerializeField]
    public int enemiesSpawned;

    UIManager _uiMan;

    [SerializeField]
    GameObject _ammoThiefCollectible;

    [SerializeField]
    GameObject _enemyCommander;

    [SerializeField]
    GameObject[] _enemies;

    [SerializeField]
    GameObject _theBoss;

    private void Start()
    {
        
        _uiMan = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    private void Update()
    {

      if(enemiesSpawned == 10 && enemiesLeft == 0)
        {
            StopCoroutine(SpawnRoutineOne());
            StartCoroutine(SpawnRoutineTwo());
        }
      else if(enemiesSpawned == 20 && enemiesLeft == 0)
        {
            StopCoroutine(SpawnRoutineTwo());
            StartCoroutine(SpawnRoutineThree());
        }
      else if(enemiesSpawned == 40 && enemiesLeft == 0)
        {
            StopCoroutine(SpawnRoutineThree());
            StartCoroutine(SpawnRoutineBoss());
        }
    }

    IEnumerator SpawnRoutineOne()
    {


        while (_stopSpawn == false && enemiesSpawned < 10)
        {
                yield return new WaitForSeconds(5.0f);
                Vector3 PosToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject _NewEnemy = Instantiate(_enemies[Random.Range(0, 2)], PosToSpawn, Quaternion.identity);
                _NewEnemy.transform.parent = _enemyContainer.transform;
            enemiesSpawned++;
            
        }
    }

    IEnumerator SpawnRoutineTwo()
    {
        level = 2;
        enemiesLeft = 10;
        _uiMan.EnemyUpdate();
        _uiMan.WaveUpdate();
        _uiMan.StartCoroutine("WaveDisplay");
        while (_stopSpawn == false && enemiesSpawned >= 10 && enemiesSpawned < 20)
        {

            
            yield return new WaitForSeconds(5.0f);
            Vector3 PosToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject _NewEnemy = Instantiate(_enemies[Random.Range(0, 3)], PosToSpawn, Quaternion.identity);
            _NewEnemy.transform.parent = _enemyContainer.transform;
            enemiesSpawned++;
        }
    }

    IEnumerator SpawnRoutineThree()
    {
        level = 3;
        enemiesLeft = 20;
        _uiMan.EnemyUpdate();
        _uiMan.WaveUpdate();
        _uiMan.StartCoroutine("WaveDisplay");
        while(_stopSpawn == false && enemiesSpawned >= 20 && enemiesSpawned < 40)
        {

            
            yield return new WaitForSeconds(5.0f);
            Vector3 PosToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject _NewEnemy = Instantiate(_enemies[Random.Range(0, 6)], PosToSpawn, Quaternion.identity);
            _NewEnemy.transform.parent = _enemyContainer.transform;
            enemiesSpawned++;
        }
    }

    IEnumerator SpawnRoutineBoss()
    {
        level = 4;
        enemiesLeft = 10;
        _uiMan.EnemyUpdate();
        _uiMan.WaveUpdate();
        _uiMan.StartCoroutine("WaveDisplay");
        while (_stopSpawn == false && enemiesSpawned >= 40 && enemiesSpawned < 51)
        {
            yield return new WaitForSeconds(5.0f);
            Vector3 PosToSpawn = new Vector3(Random.Range(8f, 8f), 7, 0);
            GameObject _NewEnemy = Instantiate(_enemies[Random.Range(0, 6)], PosToSpawn, Quaternion.identity);
            _NewEnemy.transform.parent = _enemyContainer.transform;
            enemiesSpawned++;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
        StopAllCoroutines();
        
    }

    IEnumerator SpawnPowerUp()
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(7.0f, 10.0f));
            Vector3 powerUpPos = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            Instantiate(_powerUps[Random.Range(0, 3)], powerUpPos, Quaternion.identity);
        }
    }

    IEnumerator SpawnAmmo()
    {
        while(_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
            Vector3 ammoPos = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            Instantiate(_ammoPlus, ammoPos, Quaternion.identity);
        }
    }

    public IEnumerator SpawnLife()
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(15.0f, 20.0f));
            Vector3 lifePos = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            Instantiate(_extraLife, lifePos, Quaternion.identity);
        }
    }

    public IEnumerator SpawnLaserSaberPowerUp()
    {
        while(_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(30.0f, 60.0f));
            Vector3 saberPos = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            Instantiate(_laserSaberPowerUp, saberPos, Quaternion.identity);
        }
    }

    public IEnumerator SpawnAmmoThief()
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
            Vector3 spawnPOS = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            Instantiate(_ammoThiefCollectible, spawnPOS, Quaternion.identity);
        }
    }

    public void EnemyKilled()
    {
        enemiesLeft--;
        _uiMan.EnemyUpdate();
    }
}
