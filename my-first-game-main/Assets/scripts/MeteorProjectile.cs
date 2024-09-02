using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorProjectile : MonoBehaviour
{
    [SerializeField]
    float _speed = 5.0f;

    Player _player;

    Animator _meteorAnim;

    [SerializeField]
    AudioSource _explosionSource;

    [SerializeField]
    AudioClip _explosionClip;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _explosionSource.clip = _explosionClip;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        float _position = transform.position.y;

        if (_position <= -3.8f)
        {
            Destroy(this.gameObject);
        }

        if (_position == 13.0f || _position == -13.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.Damage();
            MeteorDestruction();
        }
        else if (other.tag == "laser")
        {
            MeteorDestruction();
        }
        else if (other.tag == "LaserSaber")
        {
            MeteorDestruction();
        }
        
    }

    private void MeteorDestruction()
    {
        Destroy(this.gameObject);
        _explosionSource.Play();

    }
}
