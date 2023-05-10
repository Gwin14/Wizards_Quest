using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{

    public GameObject bulletexplosion;
    public Transform bulletExplosionSpawnPoint;
    public ParticleSystem hitParticles; // sistema de part�culas para quando a bala acertar um objeto



    void Start()
    {

    }
    void Update()
    {

    }


    //void OnBecameInvisible()
    //{
    //    // destr�i o objeto quando ele sai da tela
    //    Destroy(gameObject);
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        // verifica se o proj�til colidiu com um inimigo
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject bullet = Instantiate(bulletexplosion, bulletExplosionSpawnPoint.position, Quaternion.identity);

            collision.gameObject.GetComponent<EnemyController>().Dano(5);

            Destroy(gameObject);

            hitParticles.transform.position = transform.position;
            hitParticles.Play();

        }
    }
}
