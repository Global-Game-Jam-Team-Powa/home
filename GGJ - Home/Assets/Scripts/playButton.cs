using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playButton : MonoBehaviour
{
    public void play_buttonn_Onclick()
    {
        SceneManager.LoadScene("Game");
    }
}
