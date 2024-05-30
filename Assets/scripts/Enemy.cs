using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 1.0f;
    void Start()
    {
        // enemy needs to move down and respawn at random x axis//
        transform.position = new Vector3(Random.Range(-9.3f, 9.3f), Random.Range(0, 6.0f));
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
