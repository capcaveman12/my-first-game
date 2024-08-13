using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggresiveEnemy : Enemy
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    float _ramDistance = 5.0f;

    public override void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
    }

    public override void Update()
    {
        float _distFrmPlayer = Vector2.Distance(transform.position, _player.transform.position);

        if (_distFrmPlayer <= _ramDistance)
        {
            RamPlayer();
        }
        else
        {
            NormalMovement();
        }


        if (transform.position.x == 15.0f || transform.position.x == -15.0f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), Random.Range(6.0f, -6.0f), 0);
        }

        if (transform.position.y == 13.0f || transform.position.y == -13.0f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), Random.Range(6.0f, -6.0f), 0);
        }
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override void NormalMovement()
    {
        base.NormalMovement();
    }

    public override IEnumerator EnemyLaser()
    {
        return base.EnemyLaser();
    }

    public override void EnemyDeath()
    {

        base.EnemyDeath();

    }

    private void RamPlayer()
    {

        _speed = 2.0f;
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }
}
