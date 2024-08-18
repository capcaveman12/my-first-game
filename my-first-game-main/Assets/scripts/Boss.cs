using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    bool _isShieldActive = false;

    public int health;

    GameObject _placement;

    [SerializeField]
    GameObject[] projectiles;

    [SerializeField]
    private AudioSource _bossAudioSrce;

    [SerializeField]
    private AudioClip _laserShotClip;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    bool _bossIsDead = false;

    Animator _bossAnim;

    // Start is called before the first frame update
    void Start()
    {
        _placement = GameObject.FindGameObjectWithTag("Placement");
        transform.position = new Vector3(0, 8.37f, 0);
        StartCoroutine(Attack());
        StartCoroutine(Shield());
        _bossAudioSrce.clip = _laserShotClip;
        _bossAnim = GetComponent<Animator>();
        health = UIManager.bossHealthInt;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(health == 0)
        {
            OnBossDeath();
            _bossIsDead = true;
            UIManager.isBossDead = true;
        }

        health = UIManager.bossHealthInt;
    }

    IEnumerator Shield()
    {
        while (_bossIsDead == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 20.0f));
            transform.GetChild(0).gameObject.SetActive(true);
            _isShieldActive = true;
            yield return new WaitForSeconds(Random.Range(5.0f, 20.0f));
            transform.GetChild(0).gameObject.SetActive(false);
            _isShieldActive = false;
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

    IEnumerator Attack()
    {
        while (_bossIsDead == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
            GameObject _attackProjectile = Instantiate(projectiles[Random.Range(0, 2)], new Vector3(0, 0, 0), Quaternion.identity);

            if(_attackProjectile.tag == "EnemyLaser")
            {
                _bossAudioSrce.Play();
            }
        }
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
    }
}
