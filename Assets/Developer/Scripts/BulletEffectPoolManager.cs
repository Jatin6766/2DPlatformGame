using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectPoolManager : MonoBehaviour
{
    public static BulletEffectPoolManager Instance;
    
    [Header("Bullet Hit Effect")]
    [SerializeField] Animator bulletHitPrefab;
    [SerializeField] int initialPoolSize = 5;
    [SerializeField] AnimationClip bulletEffectClip;
    private Queue<Animator> bulletHitEffectsPool = new Queue<Animator>();


    float clipDuration;
    private void Awake()
    {
        Instance = this;

        InitializeBulletPool();
    }

    private void Start()
    {
        clipDuration = bulletEffectClip.length;
    }

    #region Bullet Effct
    private void InitializeBulletPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            spwanBulletEffect();
        }
    }

    Animator spwanBulletEffect()
    {
        Animator effect = Instantiate(bulletHitPrefab, transform);
        effect.transform.localScale = Vector2.one * 0.5f;
        effect.gameObject.SetActive(false);
        bulletHitEffectsPool.Enqueue(effect);
        return effect;
    }

    public Animator GetBulletEffectFromPool(Vector2 pos)
    {
        if (bulletHitEffectsPool.Count > 0)
        {
            Animator effect = bulletHitEffectsPool.Dequeue();
            effect.gameObject.SetActive(true);
            effect.transform.parent = null;
            effect.transform.localPosition = pos;
            effect.SetTrigger("BulletHit");
            StartCoroutine(ReturnBulletToPool(effect));
            return effect;
        }
        else
        {
            return spwanBulletEffect();
        }
    }

    IEnumerator ReturnBulletToPool(Animator effect)
    {
        yield return new WaitForSeconds(clipDuration);
        effect.gameObject.SetActive(false);
        effect.transform.parent = transform;
        bulletHitEffectsPool.Enqueue(effect);
    }
    #endregion

}
