using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{

    public List<Sprite> LifeSprites;
    public int lifeCount = 3; //4 tane life barı olacak


    public void LoseLife()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = LifeSprites[--lifeCount];
    }

}
