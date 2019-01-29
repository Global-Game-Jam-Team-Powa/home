using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    public List<Sprite> LifeSprites;
    public int lifeCount = 3; //4 tane life barı olacak
    public GameObject player;
    public bool isFinish;

    GameController GM;
    private void Start()
    {
        GM = GameObject.FindObjectOfType<GameController>();
    }
    public void LoseLife()
    {
        gameObject.GetComponent<Image>().sprite = LifeSprites[--lifeCount];
        if (lifeCount == 0 && !isFinish)
        {
            isFinish = true;
            player.transform.GetComponent<Animator>().SetBool("IsDeath", true);
            StartCoroutine("GameOver");
        }
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        GM.GameOver();
    }

}
