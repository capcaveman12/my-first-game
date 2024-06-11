using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float _speed = 3.0f;

    [SerializeField]
    private int _powerUpId;
    void Start()
    {
        
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
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                if(_powerUpId == 0)
                {
                    player.ActivateTripleShot();
                }
                else if(_powerUpId == 1)
                {
                    player.ActivateShield();
                }
                else if(_powerUpId == 2)
                {
                    player.SpeedUp();
                }
            }
            else
            {
                Debug.Log("player is null");
            }
            Destroy(this.gameObject);
        }
    }
}
