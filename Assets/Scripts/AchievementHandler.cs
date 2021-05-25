using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    int killCount = 0;
    GameObject popUpUI;

    // Start is called before the first frame update
    void Start()
    {
        popUpUI = transform.GetChild(0).gameObject;
        popUpUI.SetActive(false);

        MyEvent.INSTANCE.OnEnemyKilled += EnemyKilled;
    }

    private void OnDestroy()
    {
        MyEvent.INSTANCE.OnEnemyKilled -= EnemyKilled;
    }

    public void EnemyKilled()
    {
        killCount += 1;
        if(killCount == 5)
        {
            StartCoroutine(ShowAchievement());
        }
    }

    IEnumerator ShowAchievement()
    {
        popUpUI.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        popUpUI.SetActive(false);
    }
}
