using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthScore = 100;
    readonly int Step = 5;
    Score ScoreObject;

    private void Start()
    {
        ScoreObject = GameObject.FindObjectOfType<Score>();
    }

    public void GainHealth(int Gain)
    {
        if (HealthScore + Gain > 100)
        {
            HealthScore = 100;
        }
        else
        {
            HealthScore += Gain;
        }
    }

    public void LoseHealth(EnemyLevel Strenght)
    {
        if (HealthScore - (int)Strenght * Step <= 0)
        {
            HealthScore = 0;
            GameObject.FindObjectOfType<GameController>().GameOver();
        }
        else
        {
            HealthScore -= (int)Strenght * Step;
        }
    }

    public void LoseHealth(int Strenght, GameObject EnemyObject)
    {
        if (HealthScore - Strenght * Step <= 0)
        {
            HealthScore = 0;
            Destroy(EnemyObject);
            ScoreObject.EnemyBonus(EnemyObject);
        }
        else
        {
            HealthScore -= (int)Strenght * Step;
        }
    }
}
