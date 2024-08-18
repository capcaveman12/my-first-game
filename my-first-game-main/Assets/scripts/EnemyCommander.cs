using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommander : MonoBehaviour
{
    private Animator _enemyDeathAnim;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _enemyLaserSound;

    [SerializeField]
    private AudioSource _laserAudioSource;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    SpawnManager _spawn;

    [SerializeField]
    GameObject _trackerMissile;

    Vector3[] _movements = { Vector3.down, Vector3.left, Vector3.right };

    float _speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        _enemyDeathAnim = GetComponent<Animator>();

        _spawn = GameObject.Find("spawnmanager").GetComponent<SpawnManager>();

        StartCoroutine(EnemyLaser());
        Invoke("FireMissile", 2.0f);


    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Movement());
        float _position = transform.position.y;

        if (_position <= -3.8f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 6.0f, 0);
        }

        if (_position == 13.0f || _position == -13.0f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), Random.Range(6.0f, -6.0f), 0);
        }
    }

    IEnumerator Movement()
    {
        MoveDown();
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        MoveRight();
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        MoveLeft();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (other.tag == "Player")
        {


            if (_player != null)
            {
                _player.Damage();
            }
            else if (_player == null)
            {
                Debug.LogError("Player is null");
            }

            EnemyDeath();
        }



        if (other.tag == "laser")
        {
            if (_player != null)
            {
                _player.ScorePlus(10);
            }
            else if (_player == null)
            {
                Debug.LogError("Player is null");
            }

            Player.lasers.Remove(other.gameObject);
            Destroy(other.gameObject);

            EnemyDeath();
        }

        if (other.tag == "LaserSaber")
        {
            if (_player != null)
            {
                _player.ScorePlus(10);
            }
            else if (_player == null)
            {
                Debug.LogError("Player is null");
            }

            EnemyDeath();
        }

    }

    private void EnemyDeath()
    {

        _audioSource.Play();

        _enemyDeathAnim.SetTrigger("OnEnemyDeath");

        _speed = 0;

        StopAllCoroutines();

        Destroy(GetComponent<Collider2D>());

        Destroy(this.gameObject, 2.8f);

        _spawn.EnemyKilled();
    }

    IEnumerator EnemyLaser()
    {
        while (true)
        {
            _laserAudioSource.clip = _enemyLaserSound;

            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));

            Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);

            _laserAudioSource.Play();
        }

    }

    private void FireMissile()
    {
       
        Instantiate(_trackerMissile, transform.position + new Vector3(2, 2, 0), Quaternion.identity);
    }
}
