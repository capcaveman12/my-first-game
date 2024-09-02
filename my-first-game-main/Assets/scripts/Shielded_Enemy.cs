using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielded_Enemy : Enemy
{

    bool _isShieldActive = true;
    public override void Start()
    {
        
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override IEnumerator EnemyLaser()
    {
        return base.EnemyLaser();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
     if(other.tag == "laser")
        {
            if(_isShieldActive == true)
            {
                _isShieldActive = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else if(_isShieldActive == false)
            {
                EnemyDeath();
            }

            Destroy(other.gameObject);
            Player.lasers.Remove(other.gameObject);
         }

     if(other.tag == "Player")
     {
            if(_isShieldActive == true)
            {
                _isShieldActive = false;
                transform.GetChild(0).gameObject.SetActive(false);
                player.Damage();
                
            }
            else if(_isShieldActive == false)
            {
                EnemyDeath();
                player.Damage();
            }
     }

     if (other.tag == "LaserSaber")
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (_isShieldActive == false)
            {
                EnemyDeath();
            }
        }
    }

    public override void EnemyDeath()
    {
        base.EnemyDeath();
    }
}
