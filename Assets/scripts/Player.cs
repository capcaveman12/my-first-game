using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    // player speed//
    [SerializeField]
    float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _SpawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private GameObject _tripleshotPrefab;

    [SerializeField]
    private GameObject _shieldPrefab;

    private bool _isShieldActive = false;

    private bool _isSpeedActive = false;

    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private int _score;

    UIManager _uIManager;

    void Start()
    {
        // take current position set to 0 at start of the game//
        transform.position = new Vector3(0, 0, 0);
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _SpawnManager = GameObject.Find("spawnmanager").GetComponent < SpawnManager>();

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
    }
    
    void CalculateMovement()
    {
        if (_isSpeedActive == false)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }
        else if(_isSpeedActive == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * _speed * _speedMultiplier * Time.deltaTime);
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * _speed * _speedMultiplier * Time.deltaTime);
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

   void FireLaser() 
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleshotPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
            
        }
       

    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }


        _lives--;

        if(_lives < 1)
        {
            _SpawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
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
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ScorePlus(int points)
    {
        _score = _score + 10;
        _uIManager.NewScore(_score);
    }
}
