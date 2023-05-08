using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Camera mainCamera;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public float fireRate = 0.5f; // tempo mínimo entre os tiros
    private float lastFireTime = 0f;


    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        Vector2 aimDirection = (mouseWorldPosition - transform.position).normalized;

        if (Input.GetMouseButton(0) && Time.time - lastFireTime >= fireRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = aimDirection * bulletSpeed;

            lastFireTime = Time.time; // atualiza o tempo do último tiro

        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
