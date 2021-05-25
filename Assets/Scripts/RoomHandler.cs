using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomHandler : MonoBehaviour
{
    int enemyCount = 0;
    GameObject popUpUI;

    private void Awake()
    {
        MyEvent.INSTANCE.OnEnemyKilled += EnemyKilled;
        MyEvent.INSTANCE.OnEnemySpawn += EnemySpawned;
    }

    private void Start()
    {
        popUpUI = transform.GetChild(0).gameObject;
        popUpUI.SetActive(false);
    }

    private void OnDestroy()
    {
        MyEvent.INSTANCE.OnEnemyKilled -= EnemyKilled;
        MyEvent.INSTANCE.OnEnemySpawn -= EnemySpawned;
    }

    public void EnemySpawned()
    {
        enemyCount += 1;
    }

    public void EnemyKilled()
    {
        enemyCount -= 1;
        if(enemyCount <= 0)
        {
            RoomCleared();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RoomCleared()
    {
        popUpUI.SetActive(true);
    }
}
