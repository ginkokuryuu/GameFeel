using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int healthPoint = 4;

    private void Start()
    {
        MyEvent.INSTANCE.EnemySpawned();
    }

    public void Attacked(int damage)
    {
        healthPoint -= damage;
        if(healthPoint <= 0)
            Death();
    }

    void Death()
    {
        MyEvent.INSTANCE.EnemyHasBeenKilled();
        Destroy(this.gameObject);
    }
}
