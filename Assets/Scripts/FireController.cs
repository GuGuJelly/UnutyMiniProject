using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] ObjectPool bulletPool;
    [SerializeField] Transform muzzlePoint;

    [Range(1, 10)]
    [SerializeField] float bulletSpeed;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        // 대여해서 파이어
        PooledObject instance = bulletPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        PlayerBullet bullet = instance.GetComponent<PlayerBullet>();
        bullet.SetSpeed(bulletSpeed);
    }
}
