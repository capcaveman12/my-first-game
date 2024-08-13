using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoThief : MonoBehaviour
{

    Player _player;

    float _speed = 3.0f;

    UIManager _ui;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player.AmmoThief();

            _ui.AmmoCount(0);

            Destroy(this.gameObject);
        }
    }
}
