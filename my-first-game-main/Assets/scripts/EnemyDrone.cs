using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    [SerializeField]
    GameObject _laser;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
        Instantiate(_laser, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            Boss.numberDrones--;
        }
        else if(other.tag == "LaserSaber")
        {
            Destroy(this.gameObject);
            Boss.numberDrones -= 1;
        }
        
    }
}
