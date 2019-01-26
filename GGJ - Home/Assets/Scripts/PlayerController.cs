using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float runSpeed;
    private int Power = 10;

    private Vector3 forward;
    private Vector3 right;

    private static Animator anim;

    public GameObject WeaponOnHand;
    public GameObject PickUpWeaponText;
    public GameObject ParentToHoldObject;

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LeaveWeapon();
        }

        if (Input.GetAxisRaw("Throw") != 0f)
        {
            anim.SetBool("IsThrowing", true);
        }
        else if (Input.GetAxisRaw("HorizontalKey") != 0f || Input.GetAxisRaw("VerticalKey") != 0f)
        {
            Move();
        }
        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsThrowing", false);
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Weapon>() != null)
        {
            PickUpWeaponText.SetActive(true);

            if (Input.GetKey(KeyCode.F) && WeaponOnHand != collision.gameObject)
            {
                WeaponLogic(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Weapon>() != null)
        {
            PickUpWeaponText.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<Weapon>() != null)
        {
            PickUpWeaponText.SetActive(true);

            if (Input.GetKey(KeyCode.F) && WeaponOnHand != collision.gameObject)
            {
                WeaponLogic(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<AIGhostController>() != null)
        {
            bool WeaponAttack = false;
            foreach (ContactPoint cols in collision.contacts)
            {
                if (cols.thisCollider.gameObject.GetComponent<Weapon>() != null)
                {
                    collision.gameObject.GetComponent<Health>().LoseHealth(WeaponOnHand.GetComponent<Weapon>().Power + Power, collision.gameObject);
                    WeaponAttack = true;
                    break;
                }
            }

            if (!WeaponAttack)
            {
                collision.gameObject.GetComponent<Health>().LoseHealth(Power, collision.gameObject);
            }
        }

    }

    void WeaponLogic(GameObject WeaponToTake)
    {
        if (WeaponOnHand != null)
        {  //Elimde Weapon Varsa

            ExchangeWeapon(WeaponToTake);
        }
        else //Elim Boşsa
        {
            TakeWeapon(WeaponToTake);
        }
    }

    //Ele alınca rigidbody ekle,
    //elden bırakınca sil rigidbody sil

    void LeaveWeapon()
    {
        WeaponOnHand.gameObject.transform.SetParent(this.transform.parent);
        WeaponOnHand.transform.GetComponent<Collider>().isTrigger = true;

        Vector3 Size = WeaponOnHand.GetComponent<Collider>().bounds.size;

        WeaponOnHand.transform.rotation = Quaternion.identity;
        WeaponOnHand.transform.position = new Vector3(WeaponOnHand.transform.position.x, Size.y / 2 + 1.063445f, WeaponOnHand.transform.position.z);
        WeaponOnHand = null;

        //bırakınca yere düşmeli 
    }

    void TakeWeapon(GameObject ObjectToBeTaken)
    {
        //parentı setle
        ObjectToBeTaken.transform.SetParent(ParentToHoldObject.transform);

        //triggerı kapat
        ObjectToBeTaken.transform.GetComponent<Collider>().isTrigger = false;

        //yeri parenta göre ayarla
        ObjectToBeTaken.transform.localPosition = ObjectToBeTaken.GetComponent<Weapon>().PredeterminedPosition;

        ObjectToBeTaken.transform.localRotation =  Quaternion.Euler(ParentToHoldObject.transform.rotation.x + ObjectToBeTaken.GetComponent<Weapon>().PredeterminedRotation.x, 
                                                                    ParentToHoldObject.transform.rotation.y + ObjectToBeTaken.GetComponent<Weapon>().PredeterminedRotation.y, 
                                                                    ParentToHoldObject.transform.rotation.z + ObjectToBeTaken.GetComponent<Weapon>().PredeterminedRotation.z);

           // ObjectToBeTaken.gameObject.GetComponent<Weapon>().takenRotation;

        //Eldeki silahı setle
        WeaponOnHand = ObjectToBeTaken;
    }

    void ExchangeWeapon(GameObject ObjectToBeTaken)
    {
        LeaveWeapon();
        TakeWeapon(ObjectToBeTaken);
    }
}
