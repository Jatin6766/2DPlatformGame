using HefeGames;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    EnemyShoot enemyShoot;
    private void Start()
    {
        enemyShoot = GetComponentInParent<EnemyShoot>();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyKeywords.playerTag))
        {
            if (collision.GetComponent<PlayerController>())
            {
                collision.GetComponent<PlayerController>().TakeDamage();
            }
                enemyShoot.ShootAgain();
        }
        else if (collision.CompareTag(MyKeywords.groundTag))
        {
            enemyShoot.ShootAgain();
        }
    }
}
