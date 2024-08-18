using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    Player player;

    Animator _bombAnim;

    GameObject _barrier;

    [SerializeField]
    private AudioSource _bombAudio;

    [SerializeField]
    private AudioClip _bombExplosionClip;

    private void Start()
    {
        _barrier = GameObject.FindGameObjectWithTag("Barrier");
        _bombAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Movement();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Explosion();
            player.Damage();
            
        }
        else if(other.tag == "laser")
        {
            Explosion();
            Player.lasers.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void Explosion()
    {
        _bombAudio.clip = _bombExplosionClip;
        _bombAnim.SetTrigger("PlayerHit");
        _bombAudio.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.0f);
    }

    private void Movement()
    {
        float _speed = 2.0f;
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _barrier.transform.position, _speed * Time.deltaTime);
    }
}
