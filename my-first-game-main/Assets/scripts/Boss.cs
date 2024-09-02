using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    bool _isShieldActive = true;

    public int health;

    GameObject _placement;

    [SerializeField]
    GameObject _laser;

    [SerializeField]
    private AudioSource _bossAudioSrce;

    [SerializeField]
    private AudioClip _laserShotClip;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    bool _bossIsDead = false;

    Animator _bossAnim;

    [SerializeField]
    GameObject _meteor;

    [SerializeField]
    GameObject _drone;

    public static int numberDrones;

    // Start is called before the first frame update
    void Start()
    {
        _placement = GameObject.FindGameObjectWithTag("Placement");
        transform.position = new Vector3(0, 8.37f, 0);
        StartCoroutine(LaserAttack());
        _bossAudioSrce.clip = _laserShotClip;
        _bossAnim = GetComponent<Animator>();
        health = UIManager.bossHealthInt;
        StartCoroutine(MeteorRain());
        SpawnDrones();
    }

    // Update is called once per frame
    void Update()
    {
        Shield();
        Movement();
        if(health == 0)
        {
            OnBossDeath();
            _bossIsDead = true;
            UIManager.isBossDead = true;
        }

        health = UIManager.bossHealthInt;

        if(numberDrones > 0)
        {
            _isShieldActive = true;
        }
        else if(numberDrones == 0)
        {
            _isShieldActive = false;
            StartCoroutine(SpawnDroneAgain());
        }
        else if(numberDrones == 4)
        {
            StopCoroutine(SpawnDroneAgain());
        }
    }

    void SpawnDrones()
    {
        
        Instantiate(_drone, new Vector3(8.83f, 3.06f, 0), Quaternion.identity);
        Instantiate(_drone, new Vector3(-8.83f, 3.06f, 0), Quaternion.identity);
        Instantiate(_drone, new Vector3(4.36f, 3.06f, 0), Quaternion.identity);
        Instantiate(_drone, new Vector3(-4.36f, 3.06f, 0), Quaternion.identity);
        numberDrones = 4;
    }

    IEnumerator SpawnDroneAgain()
    {
        while (numberDrones <= 4)
        {
            yield return new WaitForSeconds(10.0f);
            Instantiate(_drone, new Vector3(8.83f, 3.06f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
            Instantiate(_drone, new Vector3(-8.83f, 3.06f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
            Instantiate(_drone, new Vector3(4.36f, 3.06f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
            Instantiate(_drone, new Vector3(-4.36f, 3.06f, 0), Quaternion.identity);
            numberDrones = 4;
            StopCoroutine(SpawnDroneAgain());
        }
    }

    public void Shield()
    {
        if (_bossIsDead == false)
        {
            if (_isShieldActive == true)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                _isShieldActive = true;
            }

            if (_isShieldActive == false)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                _isShieldActive = false;
            }
        }
    }

    private void Damage()
    {
        UIManager.bossHealthInt -= 10;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            if(_isShieldActive == true)
            {
                Player.lasers.Remove(other.gameObject);
                Destroy(other.gameObject);
                return;
            }
            else
            {
                Damage();
                Player.lasers.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator LaserAttack()
    {
        while (_bossIsDead == false)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            GameObject _attackProjectile = Instantiate(_laser, transform.position + new Vector3(0, -3, 0), Quaternion.identity);

            if(_attackProjectile.tag == "EnemyLaser")
            {
                _bossAudioSrce.Play();
            }
        }
    }

    IEnumerator MeteorRain()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        Instantiate(_meteor, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Instantiate(_meteor, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Instantiate(_meteor, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Instantiate(_meteor, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
    }

    private void Movement()
    {
        float _speed = 1.0f;
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _placement.transform.position, _speed * Time.deltaTime);
    }

    private void OnBossDeath()
    {
        _bossAudioSrce.clip = _explosionSound;
        _bossAudioSrce.Play();
        _bossAnim.SetTrigger("OnEnemyDeath");
        Destroy(this.gameObject, 3.0f);
        _bossIsDead = true;
        SpawnManager._stopSpawn = true;
        GameManager.playerWon = true;
        StopAllCoroutines();
    }
}
