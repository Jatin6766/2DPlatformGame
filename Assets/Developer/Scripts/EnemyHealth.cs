using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance;

    [SerializeField] Transform progreeBar;

    private int _currentHealth;
    [SerializeField] int maxHealth = 3;

    EnemyAnimator enemyAnimator;

    int currentHealth {
        get { return _currentHealth; }
        set { _currentHealth = value;

            if (_currentHealth == maxHealth)
                return;

            if (_currentHealth <= 0)
            {
                SetIndicator(false);
            }
            else if (_currentHealth > 0)
            {
                progreeBar.GetChild(_currentHealth).gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        OnResetValuse();
    }

    void OnResetValuse()
    { 
        currentHealth = maxHealth;
        SetIndicator(true);
    }

    void SetIndicator(bool val)
    {
        foreach (Transform item in progreeBar)
        {
            item.gameObject.SetActive(val);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemyAnimator.SetTrigger("Die");
            //play dead sound
            StartCoroutine(OnDie());
        }
        else
        {
            enemyAnimator.SetTrigger("Hit");
        }
    }

    IEnumerator OnDie()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        EnemyPoolManager.Instance.ReturnEnemyToPool(gameObject);
    }
}
