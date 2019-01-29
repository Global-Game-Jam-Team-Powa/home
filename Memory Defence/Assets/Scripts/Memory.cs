using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{

    public Vector3 StartPosition;
    public Quaternion StartRotation;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent;
        StartPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
       StartRotation = Quaternion.Euler(this.transform.position.x, this.transform.position.y, this.transform.position.z
            );

    }

   public void GhostFeltTheMemory()
    {
        this.transform.parent = null;
        this.transform.SetPositionAndRotation(StartPosition,StartRotation);
    }

}
