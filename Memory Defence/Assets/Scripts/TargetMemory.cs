using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMemory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            ChangeLayers(gameObject, "Not Solid");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            ChangeLayers(gameObject, "Default");
        }
    }

    private void ChangeLayers(GameObject gameObject, string newLayer)
    {
        gameObject.layer = LayerMask.NameToLayer(newLayer);
        foreach (Transform child in gameObject.transform)
        {
            ChangeLayers(child.gameObject, newLayer);
        }
    }
}
