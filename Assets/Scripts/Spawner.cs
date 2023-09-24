using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float Radius = 5;
    public float Count = 1;
    public float Speed = 7;
    public float MaxLife = 20;

    void Start()
    {
    }

    [ContextMenu("SPAWN")]
    public void Spawn() {
        for (int i = 0; i < Count ; i++) {
            var rndPos = Random.onUnitSphere * Radius;
            rndPos.y = 0;
            var newEnemy = Instantiate(EnemyPrefab);
            newEnemy.transform.position = transform.position + rndPos;

            newEnemy.GetComponent<AIPath>().maxSpeed = Speed;
            newEnemy.GetComponent<Damagable>().MaxLive = MaxLife;
        }
    }
}
