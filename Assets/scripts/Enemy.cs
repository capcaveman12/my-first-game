using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 1.0f;

    private Player _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float _position = transform.position.y;
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (_position <= -3.8f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 6.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (other.tag == "Player")
        {
            

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if (other.tag == "laser")
        {
            if (_player != null)
            {
                _player.ScorePlus(10);
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
