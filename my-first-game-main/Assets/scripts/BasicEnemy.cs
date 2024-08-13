using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    

    public override void Start()
    {

        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override IEnumerator EnemyLaser()
    {
        return base.EnemyLaser();
    }

    public override void EnemyDeath()
    {
        base.EnemyDeath();
    }

    public override void NormalMovement()
    {
        base.NormalMovement();
    }


}
