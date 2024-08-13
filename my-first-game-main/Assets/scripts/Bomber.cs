﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy
{
    [SerializeField]
    GameObject _bomb;

    GameObject _player;

    public override void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
        
    }

    public override void Update()
    {
        base.Update();

        float _distance = Vector3.Distance(_player.transform.position, transform.position);
       
    }

    public override void NormalMovement()
    {
        base.NormalMovement();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if(other.tag == "Barrier")
        {
            DropBomb();
        }
    }

    public override IEnumerator EnemyLaser()
    {
        return base.EnemyLaser();
    }

    public override void DestroyCollectible()
    {
        base.DestroyCollectible();
    }

    public override void EnemyDeath()
    {
        base.EnemyDeath();
    }

    public void DropBomb()
    {
        


       
        
            
            Instantiate(_bomb, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        
        
    }
}