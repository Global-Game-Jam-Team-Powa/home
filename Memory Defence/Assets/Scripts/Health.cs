using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthScore = 100;
    readonly int Step = 5;
    Score ScoreObject;
    AIGhostController Ghost;

    private void Start()
    {
        Ghost = GameObject.FindObjectOfType<AIGhostController>();
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


    public void LoseHealth(int Strenght, GameObject EnemyObject)
    {
        Debug.Log("Ghost " + Strenght * Step + " can kaybetti.");
        if (HealthScore - Strenght * Step <= 0)
        {
            
            HealthScore = 100;
            Ghost.GetOut();
        }
        else
        {
            HealthScore -= (int)Strenght * Step;
        }
    }
}
