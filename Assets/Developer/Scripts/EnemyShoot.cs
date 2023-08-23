using HefeGames;
using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] EnemyBullet bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform firePoint;

    SpriteRenderer spriteRendere;

    bool isReadyToShoot;
    Transform player;
    EnemyAnimator enemyAnimator;


    // For Bullets
    Rigidbody2D bulletRb2d;
    SpriteRenderer bulletSpriteRendere;

    private void Awake()
    {
        spriteRendere = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<EnemyAnimator>();

        bulletRb2d = bullet.GetComponent<Rigidbody2D>();
        bulletSpriteRendere = bullet.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag(MyKeywords.playerTag))
        { 
            player = GameObject.FindGameObjectWithTag(MyKeywords.playerTag).transform;
        }
    }

    void OnEnable()
    {
        StartCoroutine(StartShoot());
    }

    void OnDisable()
    { 
        StopCoroutine(StartShoot());
    }

    IEnumerator StartShoot()
    {
        yield return new WaitForSeconds(2);
        while(gameObject.activeInHierarchy)
        {
            if (player != null)
            {
                yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
                isReadyToShoot = false;
                StartCoroutine(Shoot());
                yield return new WaitUntil(() => isReadyToShoot);
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator Shoot()
    {
        enemyAnimator.SetTrigger("Attack");
        bool face = LookAtPlayerSide();
        float _speed = face ? -bulletSpeed : bulletSpeed;
        bulletSpriteRendere.flipX = face;

        yield return new WaitForSeconds(0.1f);

        bullet.gameObject.SetActive(true);
        bullet.transform.position = firePoint.position;
        bulletRb2d.velocity = new Vector2(_speed, 0);
    }

    bool LookAtPlayerSide()
    {
        if (player)
        { 
            bool isFlip = player.position.x - transform.position.x > 0 ? false : true;
            spriteRendere.flipX = isFlip;
            return isFlip;
        }
        return false;
    }

    public void ShootAgain()
    {
        bullet.gameObject.SetActive(false);
        isReadyToShoot = true;
    }


}
