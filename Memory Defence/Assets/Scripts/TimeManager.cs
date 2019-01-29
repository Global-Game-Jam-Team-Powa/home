using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    Text time;
    GameController GM;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        time = this.GetComponent<Text>();
        GM = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        if (Time.timeSinceLevelLoad > 300)
        {
            player.transform.GetComponent<Animator>().SetBool("IsWin", true);
           // GM.GameWin();
        }
    }
    void UpdateTime()
    {
        float timer = 299 - Time.timeSinceLevelLoad;
        if (timer > 0f)
        {
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");
            time.text = minutes + ":" + seconds;
        }
        else
        {
            time.text = "00:00";
        }

    }
}
