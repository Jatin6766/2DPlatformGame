using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;
    [Header("Bullet Spwan Details")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] int initialPoolSize = 15;
    private Queue<Bullet> bulletPool = new Queue<Bullet>();

    private void Awake()
    {
        Instance = this;

        InitializeBulletPool();
    }

    #region Bullet
    private void InitializeBulletPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            spwanBullet();
        }
    }

    Bullet spwanBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform);
        bullet.transform.localScale = Vector2.one * 0.5f;
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
        return bullet;
    }

    public Bullet GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            Bullet bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.transform.parent = null;
            return bullet;
        }
        else
        {
            return spwanBullet();
        }
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.parent = transform;
        bulletPool.Enqueue(bullet);
    }
    #endregion

}
