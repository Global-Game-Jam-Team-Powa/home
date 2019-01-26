using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeated : MonoBehaviour
{
        Animator animatior;
    // Start is called before the first frame update
    void Start()
    {
        animatior = this.transform.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        animatior.Play(0);
    }
}
