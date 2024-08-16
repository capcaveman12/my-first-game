using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackerMissile : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    float _speed = 3.0f;


    [SerializeField]
    AudioSource _missileAudio;

    [SerializeField]
    AudioClip _missileAudioExplosion;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        transform.up = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            transform.up = _player.transform.position;

            _missileAudio.clip = _missileAudioExplosion;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            _missileAudio.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            Player.lasers.Remove(other.gameObject);
        }
        else if(other.tag == "Player")
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
            _missileAudio.Play();
            player.Damage();
        }
        else if(other.tag == "LaserSaber")
        {
            _missileAudio.Play();
        }
    }
}
