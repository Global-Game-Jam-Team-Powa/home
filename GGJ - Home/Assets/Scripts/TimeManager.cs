using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
     Text time;
    GameController GM;

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
            GM.GameOver();
        }
    }
    void UpdateTime()
    {
        float timer = Time.timeSinceLevelLoad;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        time.text = minutes+":"+seconds;
    }
}
