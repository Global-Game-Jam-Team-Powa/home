﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    public List<Sprite> LifeSprites;
    public int lifeCount = 3; //4 tane life barı olacak
    public GameObject player;

    GameController GM;
    private void Start()
    {
        GM = GameObject.FindObjectOfType<GameController>();
    }
    public void LoseLife()
    {
        gameObject.GetComponent<Image>().sprite = LifeSprites[--lifeCount];
        if (lifeCount == 0)
        {
            player.transform.GetComponent<Animator>().SetBool("IsDeath", true);
            GM.GameOver();
        }
    }

}
