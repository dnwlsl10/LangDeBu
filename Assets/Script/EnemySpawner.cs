using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnTarget;
    public Enemy enemyPrefab;
    private Enemy enemy;
    private float enemySpawnCount;
    public System.Action<Enemy> OnCreateEnemy;

    private void Start()
    {
        this.enemySpawnCount = 1;
    }

    private void CreateEnemy()
    {
        this.enemy = Instantiate<Enemy>(this.enemyPrefab);
        this.enemy.transform.position = spawnTarget.position;
        this.enemySpawnCount--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enemySpawnCount > 0 && other.gameObject.CompareTag("Player"))
        {
            CreateEnemy();

            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            OnCreateEnemy(enemy);
        }
    }
}
