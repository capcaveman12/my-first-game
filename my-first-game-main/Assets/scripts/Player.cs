using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    // player speed//
    [SerializeField]
    float _speed = 3.5f;

    [SerializeField]
    private float _thrusterSpeed;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private int _ammoCount = 15;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _SpawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private GameObject _tripleshotPrefab;

    private bool _isShieldActive = false;

    [SerializeField]
    private bool _secondShieldActive = false;

    [SerializeField]
    private bool _thirdShieldActive = false;

    private bool _isSpeedActive = false;

    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private int _score;

    UIManager _uIManager;

    private Animator _playerAnim;

    [SerializeField]
    private AudioClip _laserSound;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioSource _explosionSoundSource;

    [SerializeField]
    private AudioClip _explosionSoundClip;

    [SerializeField]
    private bool _isThrustersActive = false;

    [SerializeField]
    public static bool _ThrustersOverheat = false;

    public static float scrollBar;

    Animator _camAnim;

    [SerializeField]
    private bool _laserSaberActive = false;

    PowerUp _powerup;

    AmmoPlus _ammoPlus;

    ExtraLife _extraLife;

    LaserSaberPowerUp _laserSaber;

    [SerializeField]
    GameObject _homingMissile;

    [SerializeField]
    bool _homingMissileLoaded = false;

    public static List<GameObject> lasers = new List<GameObject>();

    void Start()
    {
        _camAnim = GameObject.Find("Main Camera").GetComponent<Animator>();
        transform.position = new Vector3(0, 0, 0);

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _SpawnManager = GameObject.Find("spawnmanager").GetComponent < SpawnManager>();

        _playerAnim = GetComponent<Animator>();

        if (_SpawnManager == null)
        {
            Debug.LogError("The spawnmanager is null");
        }

        if(_uIManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
            
        }

        _audioSource.clip = _laserSound;

        _explosionSoundSource.clip = _explosionSoundClip;

        IsPlayerHurt();

        if(_laserSaberActive == true)
        {
            transform.GetChild(6).gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FireHomingMissile();
        }

        if(_ThrustersOverheat == true)
        {
            StartCoroutine(BackToNormal());
        }
    }

    public static void ThrustersOverheat()
    {
        _ThrustersOverheat = true;
    }

    private void FireHomingMissile()
    {
        if(_homingMissileLoaded == true)
        {
            Instantiate(_homingMissile, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
            _homingMissileLoaded = false;
        }
    }

    IEnumerator BackToNormal()
    {
        if (_ThrustersOverheat == true)
        {
            
            yield return new WaitForSeconds(3.0f);
            _ThrustersOverheat = false;
            
            
        }
    }
    
    void CalculateMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Thruster();
            StartCoroutine(ThrustersEngaged());
            StopCoroutine(ThrustersDisengage());
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            NoThruster();
            StartCoroutine(ThrustersDisengage());
            StopCoroutine(ThrustersEngaged());
        }

        if(_isThrustersActive == true && _ThrustersOverheat == false)
        {
            _thrusterSpeed = 3;
            
        }
        else if(_isThrustersActive == false && _ThrustersOverheat == false)
        {
            _thrusterSpeed = 1;
            StopCoroutine(BackToNormal());
            
        }
        else if(_isThrustersActive == false && _ThrustersOverheat == true)
        {
            _thrusterSpeed = 0;
            StartCoroutine(BackToNormal());
            
        }
        else if(_isThrustersActive == true && _ThrustersOverheat == true)
        {
            _thrusterSpeed = 0;
            StartCoroutine(BackToNormal());
            
        }

        if (_isSpeedActive == false)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * _speed * _thrusterSpeed * Time.deltaTime);
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * _speed * _thrusterSpeed * Time.deltaTime);
        }
        else if(_isSpeedActive == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * _speed * _speedMultiplier * _thrusterSpeed * Time.deltaTime);
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * _speed * _speedMultiplier * _thrusterSpeed * Time.deltaTime);
        }


        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    private void Thruster()
    {
        _isThrustersActive = true;
    }

    private void NoThruster()
    {
        _isThrustersActive = false;
    }

    IEnumerator ThrustersEngaged()
    {
        while (scrollBar < 1.0 && _isThrustersActive == true)
        {
            yield return new WaitForSeconds(0.5f);
            scrollBar += 0.01f * Time.deltaTime;
        }
    }

    IEnumerator ThrustersDisengage()
    {
        while (scrollBar > 0.0f && _isThrustersActive == false)
        {
            yield return new WaitForSeconds(0.5f);
            scrollBar -= 5.0f * Time.deltaTime;
        }
    }

   void FireLaser() 
    {
        if (_ammoCount != 0)
        {
            _canFire = Time.time + _fireRate;


            if (_isTripleShotActive == true)
            {
                GameObject tripleLaser = Instantiate(_tripleshotPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
                lasers.Add(tripleLaser);
            }
            else
            {
                GameObject singleLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
                _ammoCount -= 1;
                lasers.Add(singleLaser);

                _uIManager.AmmoCount(_ammoCount);
            }
            
            _audioSource.Play();
        }
    }

    public void ActivateLaserSaber()
    {
        _laserSaberActive = true;
        StartCoroutine(DeactivateLaserSaber());
    }

    IEnumerator DeactivateLaserSaber()
    {
       yield return new WaitForSeconds(5.0f);
        _laserSaberActive = false;
        transform.GetChild(6).gameObject.SetActive(false);
    }

    public void Damage()
    {

        _camAnim.SetTrigger("PlayerHit");
        _camAnim.SetTrigger("CoolDown");

        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            transform.GetChild(1).gameObject.SetActive(false);
            ActivateSecondShield();
            return;
           
        }

        if(_secondShieldActive == true)
        {
            _secondShieldActive = false;
            transform.GetChild(4).gameObject.SetActive(false);
            ActivateThirdShield();
            return;
            
        }

        if(_thirdShieldActive == true)
        {
            _thirdShieldActive = false;
            transform.GetChild(5).gameObject.SetActive(false);
            return;
            
        }

        


        _lives--;

        _uIManager.UpdateLives(_lives);

        if (_lives < 1)
        {

            _SpawnManager.OnPlayerDeath();

            _playerAnim.SetTrigger("PlayerDies");
            _explosionSoundSource.Play();

            DestroyPlayer();

        }

        _camAnim.SetTrigger("PlayerHit");



    }

    private void DestroyPlayer()
    {
        Destroy(this.GetComponent<Collider2D>());
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(2).gameObject);
        Destroy(transform.GetChild(3).gameObject);
        Destroy(this.gameObject, 2.8f);
        Destroy(GameObject.FindGameObjectWithTag("enemy").gameObject);
    }

    private void HurtThreeLives()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    private void HurtTwoLives()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    private void HurtOneLife()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedUp()
    {
        _isSpeedActive = true;
        StartCoroutine(SlowDownRoutine());
    }

    IEnumerator SlowDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isSpeedActive = false;
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        transform.GetChild(1).gameObject.SetActive(true);

        if(_secondShieldActive == true || _thirdShieldActive == true)
        {
            _secondShieldActive = false;
            transform.GetChild(4).gameObject.SetActive(false);
            _thirdShieldActive = false;
            transform.GetChild(5).gameObject.SetActive(false);
            // change to an array//
        }
    }

    public void ScorePlus(int points)
    {
        _score = _score + 10;
        _uIManager.NewScore(_score);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "EnemyLaser")
        {
            Damage();
            Destroy(other.gameObject);
        }
        else if(other.tag == "EnemyMissile")
        {
            Damage();
            Destroy(other.gameObject);
        }
    }

    private void ActivateSecondShield()
    {
        _secondShieldActive = true;
        transform.GetChild(4).gameObject.SetActive(true);
    }

    private void ActivateThirdShield()
    {
        _thirdShieldActive = true;
        transform.GetChild(5).gameObject.SetActive(true);

    }

    public void AddAmmo()
    {
        _ammoCount += 15;
        _uIManager.AmmoCount(_ammoCount);
    }

    public void AddLife()
    {
        if(_lives < 3)
        {
            _lives += 1;
            _uIManager.UpdateLives(_lives);
        }
    }

    private void IsPlayerHurt()
    {

        switch (_lives)
        {
            case 3:
                HurtThreeLives();
                break;
            case 2:
                HurtTwoLives();
                break;
            case 1:
                HurtOneLife();
                break;
        }

    }

    public void AmmoThief()
    {
        _ammoCount = 0;
    }

}
