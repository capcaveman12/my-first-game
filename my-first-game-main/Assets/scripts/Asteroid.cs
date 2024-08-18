
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _rotateSpeed = 3.0f;

    private Animator _asteroidAnim;

    private SpawnManager _spawnManager;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    private AudioSource _audioSource;

    Utility _ut;

    
    [SerializeField]
    private void Start()
    {
        transform.position = new Vector3(0, 3.35f, 0);

        _asteroidAnim = GetComponent<Animator>();

        _spawnManager = GameObject.Find("spawnmanager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The asteroid spawn manager is null");
        }

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("The asteroid audio source is null");
        }
        else if(_audioSource != null)
        {
            _audioSource.clip = _explosionSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            _asteroidAnim.SetTrigger("OnAsteroidDestroy");

            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.gameObject,3.0f);
            _audioSource.Play();

            _spawnManager.StartCoroutine("SpawnRoutineOne");
            _spawnManager.StartCoroutine("SpawnPowerUp");
            _spawnManager.StartCoroutine("SpawnAmmo");
            _spawnManager.StartCoroutine("SpawnLife");
            _spawnManager.StartCoroutine("SpawnLaserSaberPowerUp");
            _spawnManager.StartCoroutine("SpawnAmmoThief");
            
            Player.lasers.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }


}
