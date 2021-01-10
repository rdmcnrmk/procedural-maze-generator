using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject targetDebug;

    [SerializeField]
    private GameObject bulletPrefab = null;

    private float bulletForce = 20f;

    private Vector3 firePoint;
    private Vector3 fireDir;

    private float firePointDistFromOrigin = 1;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        targetDebug.transform.position = worldPosition;

        fireDir = (worldPosition - transform.position).normalized;
        firePoint = transform.position + fireDir * firePointDistFromOrigin;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(fireDir * bulletForce, ForceMode2D.Impulse);
    }
}
