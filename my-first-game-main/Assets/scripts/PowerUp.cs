using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float _speed = 3.0f;

    [SerializeField]
    private int _powerUpId;

    [SerializeField]
    private AudioClip _powerUpSound;

    [SerializeField]
    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKey(KeyCode.C))
        {
            TowardPlayer();
        }

        _powerUpId = SpawnManager.powerId;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerUpSound, transform.position, 100.0f);
           
            if (player != null)
            {

                switch (_powerUpId)
                {
                    case 0:
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateShield();
                        break;
                    case 2:
                        player.SpeedUp();
                        break;
                }

            }
            else if(player == null)
            {
                Debug.Log("player is null");
            }

              Destroy(this.gameObject);
        }

        if(other.tag == "EnemyLaser")
        {
            Destroy(this.gameObject);
        }
    }

    public void TowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

}
