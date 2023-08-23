using HefeGames;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyKeywords.cameraTag))
        {
            BulletPoolManager.Instance.ReturnBulletToPool(this);
        }
        else if (collision.CompareTag(MyKeywords.groundTag))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.enemyHitBody);
            BulletEffectPoolManager.Instance.GetBulletEffectFromPool(transform.position);
            BulletPoolManager.Instance.ReturnBulletToPool(this);
        }
        else if (collision.CompareTag(MyKeywords.enemyHeadTag))
        {
            if (collision.GetComponentInParent<EnemyHealth>())
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.EnemyHitHead);
                HUDController.Instance.Score += 2;
                BulletEffectPoolManager.Instance.GetBulletEffectFromPool(transform.position);
                collision.GetComponentInParent<EnemyHealth>().TakeDamage(3);
                BulletPoolManager.Instance.ReturnBulletToPool(this);
            }
        }
        else if (collision.CompareTag(MyKeywords.enemyBodyTag))
        {
            if (collision.GetComponentInParent<EnemyHealth>())
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.enemyHitBody);
                HUDController.Instance.Score += 1;
                BulletEffectPoolManager.Instance.GetBulletEffectFromPool(transform.position);
                collision.GetComponentInParent<EnemyHealth>().TakeDamage(1);
                BulletPoolManager.Instance.ReturnBulletToPool(this);
            }
        }
    }
}
