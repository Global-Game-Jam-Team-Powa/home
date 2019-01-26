using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start() => animator = this.GetComponent<Animator>();

    // Update is called once per frame
    void Update()
    {

        float move = 0.5f;
        if (Input.GetKeyDown(KeyCode.Space))
        {

            animator.Play(0);

        }
        else
        {
          //  animator.Play(1);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position = new Vector3(transform.position.x - move, transform.position.y, transform.position.z);
         //   animator.Play(2);

        }
        else
        {
         //   animator.Play(1);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x+ move, transform.position.y, transform.position.z );
           // animator.Play(2);

        }
        else
        {
          //  animator.Play(1);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z- move);
           // animator.Play(2);

        }
        else
        {
           // animator.Play(1);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z+ move);
         //   animator.Play(2);

        }
        else
        {
         //   animator.Play(1);

        }
    }
    
}
