using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    // Start is called before the first frame update

     Player _player;

    [SerializeField]
    private float _speed = 3.0f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.AddLife();
            Destroy(this.gameObject);
        }
        else if(other.tag == "EnemyLaser")
        {
            Destroy(this.gameObject);
        }
    }

    public void TowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

}
