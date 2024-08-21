using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 8.0f;

    public static Vector3 direction;

    private void Start()
    {
        direction = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            Destroy(gameObject);
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void ReverseDirection()
    {
        direction = Vector3.up;
    }

}
