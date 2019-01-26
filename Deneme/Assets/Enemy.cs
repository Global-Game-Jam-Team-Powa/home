using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Health Health;
   public EnemyLevel Level = EnemyLevel.Level_1;


     void Start()
    {
        Health = GameObject.FindObjectOfType<Health>();
    }


     void OnCollisionEnter( Collision collision)
    {
        if (collision.transform.tag == "Player" )
        {
            Health.LoseHealth(this.Level);          
        }
    }






}
