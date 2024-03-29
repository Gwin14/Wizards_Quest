using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileEnemy : MonoBehaviour
{

    public GameObject bulletexplosion;
    public Transform bulletExplosionSpawnPoint;
    public ParticleSystem hitParticles; // sistema de partÝculas para quando a bala acertar um objeto



    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // verifica se o projÚtil colidiu com um inimigo
        if (collision.gameObject.tag == "Player")
        {
            GameObject bullet = Instantiate(bulletexplosion, bulletExplosionSpawnPoint.position, Quaternion.identity);

            collision.gameObject.GetComponent<PlayerController>().Dano(5);

            Destroy(gameObject);

            hitParticles.transform.position = transform.position;
            hitParticles.Play();

        }
        if (collision.gameObject.tag == "Parede")
        {
            GameObject bullet = Instantiate(bulletexplosion, bulletExplosionSpawnPoint.position, Quaternion.identity);

            Destroy(gameObject);

            hitParticles.transform.position = transform.position;
            hitParticles.Play();

        }
    }
}
