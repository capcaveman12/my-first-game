using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSaberPowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        //Player.collectPwUp += TowardPlayer;//
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKey(KeyCode.C))
        {
            TowardPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (other.tag == "Player")
        {
            _player.ActivateLaserSaber();
            Destroy(this.gameObject);
        }
        else if (other.tag == "EnemyLaser")
        {
            Destroy(this.gameObject);
        }
    }

    public void TowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }
}
