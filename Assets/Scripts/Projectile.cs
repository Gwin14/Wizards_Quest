using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{

    public GameObject bulletexplosion;
    public Transform bulletExplosionSpawnPoint;
    public ParticleSystem hitParticles; // sistema de partículas para quando a bala acertar um objeto
    public float dano;



    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // verifica se o projétil colidiu com um inimigo
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject bullet = Instantiate(bulletexplosion, bulletExplosionSpawnPoint.position, Quaternion.identity);

            collision.gameObject.GetComponent<EnemyController>().Dano(dano);

            Destroy(gameObject);

            hitParticles.transform.position = transform.position;
            hitParticles.Play();

        }
    }
}
