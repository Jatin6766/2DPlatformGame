using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;
    [Header("Enemy Spwan Details")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int initialenemyPoolSize = 15;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    [SerializeField] Transform[] spwanPoints;

    private void Awake()
    {
        Instance = this;

        InitializeEnemyPool();
    }
    

    private void Start()
    {
        spwanEnemyPrefab(3);
    }

    private void InitializeEnemyPool()
    {
        for (int i = 0; i < initialenemyPoolSize; i++)
        {
            GameObject _enemy = Instantiate(enemyPrefab, transform);
            _enemy.SetActive(false);
            enemyPool.Enqueue(_enemy);
        }
    }

    public void spwanEnemyPrefab(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Transform _enemy = GetEnemyFromPool().transform;
            _enemy.position = spwanPoints[Random.Range(0, spwanPoints.Length)].position;
        }
    }

    public GameObject GetEnemyFromPool()
    {
        if (enemyPool.Count > 0)
        {
            GameObject _enemy = enemyPool.Dequeue();
            _enemy.SetActive(true);
            _enemy.transform.parent = null;
            return _enemy;
        }
        return null;
    }

    public void ReturnEnemyToPool(GameObject _enemy)
    {
        _enemy.transform.parent = transform;
        _enemy.SetActive(false);
        enemyPool.Enqueue(_enemy);
        spwanEnemyPrefab(1);
    }
}
