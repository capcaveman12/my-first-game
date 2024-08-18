using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public  float _speed = 1.0f;

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

    GameObject _collectible;


    public virtual void Start()
    {
        _speed = 1.0f;
        _collectible = GameObject.FindGameObjectWithTag("Collectible");
        _enemyDeathAnim = GetComponent<Animator>();

        StartCoroutine(EnemyLaser());

        _spawn = GameObject.Find("spawnmanager").GetComponent<SpawnManager>();
        _audioSource.clip = _explosionSound;


    }

    // Update is called once per frame
    public virtual void Update()
    {
        float _enemyX = transform.position.x;
        float _enemyY = transform.position.y;
        float _collectibleX = transform.position.x;
        float _collectibleY = transform.position.y;


        if (_collectibleX == _enemyX && _collectibleY < _enemyY)
        {
            DestroyCollectible();
        }

        NormalMovement();

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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (other.tag == "Player")
        {
            

            if (_player != null)
            {
                _player.Damage();
            }
             else if(_player == null)
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

        if(other.tag == "LaserSaber")
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

    public virtual IEnumerator EnemyLaser()
    {
        while (true)
        {
            _laserAudioSource.clip = _enemyLaserSound;

            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));

            Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);

            _laserAudioSource.Play();
        }

    }

    public virtual void EnemyDeath()
    {

        _audioSource.Play();

        _enemyDeathAnim.SetTrigger("OnEnemyDeath");

        _speed = 0;

        StopAllCoroutines();

        Destroy(GetComponent<Collider2D>());

        Destroy(this.gameObject, 2.8f);

        _spawn.EnemyKilled();
    }

    public virtual void NormalMovement()
    {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    public virtual void DestroyCollectible()
    {
        _laserAudioSource.clip = _enemyLaserSound;

            Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);

            _laserAudioSource.Play();
    }

}
