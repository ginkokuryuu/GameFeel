using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEvent : MonoBehaviour
{
    public static MyEvent INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }

    public event Action OnEnemyKilled;
    public void EnemyHasBeenKilled()
    {
        OnEnemyKilled?.Invoke();
    }

    public event Action OnEnemySpawn;
    public void EnemySpawned()
    {
        OnEnemySpawn?.Invoke();
    }


}
