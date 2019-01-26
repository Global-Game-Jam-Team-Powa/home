using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
   public int Power = 10;
    Score GameScore;
    float StartPosY;
    private void Start()
    {
        GameScore = GameObject.FindObjectOfType<Score>();
        StartPosY = this.transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Object hit with Weapon");
        if (collision.gameObject.GetComponent<AIGhostController>() != null)  //vurduğu obje enemy ise 
        {
            Debug.Log("Enemy Object hit with weapon.");
            collision.gameObject.GetComponent<AIGhostController>().getHealth().LoseHealth(Power, collision.gameObject);  // enemy objesinin healthini düşür
            GameScore.AddToGameScore(collision.gameObject.GetComponent<AIGhostController>().Level); //oyun skoruna ekleme yap 
           }

    }

    public float GetStartY()
    {
        return StartPosY;
    }


}
