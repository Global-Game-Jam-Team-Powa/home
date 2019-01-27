using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{

    public Transform StartTransform;

    // Start is called before the first frame update
    void Start()
    {
        this.StartTransform = this.transform;   
    }

    void GhostFeltTheMemory()
    {
        this.transform.SetPositionAndRotation(StartTransform.position,StartTransform.rotation);
    }

}
