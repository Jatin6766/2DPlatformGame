using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet movement")]
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;

    public delegate void ShootBullet(bool characterface);
    public static ShootBullet OnShootBullet;

    private void OnEnable()
    {
        OnShootBullet += Shoot;
    }

    private void OnDisable()
    {
        OnShootBullet -= Shoot;
    }

    void Shoot(bool _isFacingRight)
    {
        StartCoroutine(moveObjectWithDelay(_isFacingRight));
    }

    IEnumerator moveObjectWithDelay(bool _isFacingRight)
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.PlaySound(SoundManager.Instance.playerShoot);
        Bullet bullet = BulletPoolManager.Instance.GetBulletFromPool();
        bullet.transform.position = firePoint.position;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<SpriteRenderer>().flipX = !_isFacingRight;
        float _speed = _isFacingRight ? bulletSpeed : -bulletSpeed;
        bulletRb.velocity = new Vector2(_speed, 0);
    }

   
}
