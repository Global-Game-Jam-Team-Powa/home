using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{


    public Text ScoreText;
    int GameScore=0;
    int Step = 5;
    int RescueScore = 50;


    int GetScore()
    {
        return GameScore;
    }

   public void AddToGameScore(int Addition)
    {
        GameScore += Addition;
        UpdateScore();

    }
    public void AddToGameScore(EnemyLevel EL)
    {
        GameScore += (int)EL*Step;
        UpdateScore();

    }
    public void SubFromGameScore(int Subs)
    {
        GameScore -= Subs;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = GameScore.ToString();
    }

  public  void EnemyBonus(GameObject Enemy)
    {
        GameScore += (int)Enemy.GetComponent<Enemy>().Level * Step * Step;
        UpdateScore();
    }

 public   void RescueBonus()
    {
        GameScore += RescueScore;
        UpdateScore();
    }

}
