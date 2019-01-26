using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float runSpeed;

    private Vector3 forward;
    private Vector3 right;

    private static Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // anim = transform.parent.GetComponent<Animator>();
        anim = transform.GetComponent<Animator>();

        forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0f, 90f, 0f)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("HorizontalKey") != 0f || Input.GetAxisRaw("VerticalKey") != 0f)
        {
            Move();
        }
        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
        }
    }

    private void Move()
    {
        bool isRun = Input.GetAxisRaw("RunKey") != 0f;
        float speed = isRun ? moveSpeed : runSpeed;

        if (isRun)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsWalking", true);
        }

        Vector3 direction = new Vector3(Input.GetAxisRaw("HorizontalKey"), 0f, Input.GetAxisRaw("VerticalKey"));
        Vector3 rightMovement = right * speed * Time.deltaTime * Input.GetAxisRaw("HorizontalKey");
        Vector3 upMovement = forward * speed * Time.deltaTime * Input.GetAxisRaw("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading; // Rotation
        transform.position += rightMovement + upMovement; // Movement
        // transform.parent.forward = heading; // Rotation
        // transform.parent.position += rightMovement + upMovement; // Movement
        
        // transform.Rotate(heading);
        // transform.Translate(transform.position + rightMovement + upMovement);
    }
}
