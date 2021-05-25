using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text scoreText = null;
    int currentScore = 0;

    public void AddScore()
    {
        currentScore += 1;
        scoreText.text = "Score: " + currentScore;
    }

    private void Start()
    {
        MyEvent.INSTANCE.OnEnemyKilled += AddScore;
    }

    private void OnDestroy()
    {
        MyEvent.INSTANCE.OnEnemyKilled -= AddScore;
    }
}
