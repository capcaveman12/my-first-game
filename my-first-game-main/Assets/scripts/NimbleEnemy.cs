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
                }
            }
        }
        
        
    }
}
