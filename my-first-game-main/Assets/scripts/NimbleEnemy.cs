using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbleEnemy : Enemy
{
    float _dangerDist = 5.0f;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        NormalMovement();
        Dodge();

        float _position = transform.position.y;

        if (_position <= -3.8f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 6.0f, 0);
        }

        if (_position == 13.0f || _position == -13.0f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), Random.Range(6.0f, -6.0f), 0);
        }
    }

    public override void NormalMovement()
    {
        base.NormalMovement();
    }

    public override IEnumerator EnemyLaser()
    {
        return base.EnemyLaser();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override void DestroyCollectible()
    {
        base.DestroyCollectible();
    }

    public override void EnemyDeath()
    {
        base.EnemyDeath();
    }

    private void Dodge()
    {
        //Vector3 laserPos = new Vector3(0, 0, 0);//

        if (Player.lasers.Count != 0)
        {
            foreach (GameObject laser in Player.lasers)
            {
                if (laser != null)
                {
                    Vector3 laserPos = laser.transform.position;


                    if (Vector3.Distance(transform.position, laserPos) < _dangerDist)
                    {


                        if (laserPos.x > transform.position.x)
                        {
                            transform.Translate(Vector3.left * 3.0f * Time.deltaTime);
                        }

                        if (laserPos.x < transform.position.x)
                        {
                            transform.Translate(Vector3.right * 3.0f * Time.deltaTime);
                        }

                        float _position = transform.position.y;

                        if (_position <= -3.8f)
                        {
                            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 6.0f, 0);
                        }

                        if (_position == 13.0f || _position == -13.0f)
                        {
                            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), Random.Range(6.0f, -6.0f), 0);
                        }
                    }
                }
            }
        }
        
        
    }
}
