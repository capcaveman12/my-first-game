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

    private bool _stopSpawn = false;

    [SerializeField]
    private GameObject[] _powerUps;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnRoutine()
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(5f);
            Vector3 PosToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject _NewEnemy = Instantiate(_enemyPrefab, PosToSpawn, Quaternion.identity);
            _NewEnemy.transform.parent = _enemyContainer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
    }

    IEnumerator SpawnPowerUp()
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(7.0f, 20.0f));
            Vector3 powerUpPos = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            Instantiate(_powerUps[UnityEngine.Random.Range(0, 3)], powerUpPos, Quaternion.identity);
        }
    }
}
