using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject WeaponOnHand;
    public GameObject PickUpWeaponText;
    public GameObject ParentToHoldObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LeaveWeapon();
        }
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

        WeaponOnHand.transform.position = new Vector3(WeaponOnHand.transform.position.x, WeaponOnHand.GetComponent<Weapon>().GetStartY(), WeaponOnHand.transform.position.z);
        WeaponOnHand.transform.rotation = Quaternion.identity;


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
        ObjectToBeTaken.transform.localPosition = Vector3.zero;

        //Eldeki silahı setle
        WeaponOnHand = ObjectToBeTaken;
    }

    void ExchangeWeapon(GameObject ObjectToBeTaken)
    {
        LeaveWeapon();
        TakeWeapon(ObjectToBeTaken);
    }

}
