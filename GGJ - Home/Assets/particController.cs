using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particController : MonoBehaviour
{

    ParticleSystem PS;
    // Start is called before the first frame update
    void Start()
    {
        PS = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PS.particleCount <= 2)
        {
            PS.Pause();
        }
    }
}
