using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingMissile : MonoBehaviour
{
    GameObject _enemy;

    [SerializeField]
    float _speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
        transform.up = _enemy.transform.position;
    }
}
