using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGhostController : MonoBehaviour
{
    private Health health;
    public EnemyLevel Level = EnemyLevel.Level_1;

    private float waitTime;
    public float speed;
    private bool goTarget;

    private int randomSpotCounter;
    private int randomSpot;
    private int randomTarget;
    private int randomOutSpot;

    public GameObject[] m_TargetList;
    public Transform[] moveSpots;
    public Transform[] outSpots;

    public GameObject ParentToHoldObject;
    private GameObject takenTargetMemory;
    private Transform takenTargetMemoryStartTransform;

    private Vector3 ghostPivot = new Vector3(-2.98f, -0.26f, 0.47f);

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();

        waitTime = Random.Range(1, 6);
        randomSpot = Random.Range(0, moveSpots.Length);
        randomTarget = Random.Range(0, m_TargetList.Length);
        randomOutSpot = Random.Range(0, outSpots.Length);
        randomSpotCounter = 1;
        goTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (randomSpotCounter < 4)
        {
            GoRandomSpot();
        }
        else if (goTarget)
        {
            Vector3 targetPosition = m_TargetList[randomTarget].gameObject.transform.position + ghostPivot;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else if (randomSpotCounter == 4)
        {
            GoRandomSpot();
        }
        else
        {
            GoRandomOutSpot();
        }
    }

    void GoRandomSpot()
    {
        Vector3 targetPosition = moveSpots[randomSpot].position + ghostPivot;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            randomSpotCounter++;
            randomSpot = Random.Range(0, moveSpots.Length);
        }
    }

    void GoRandomOutSpot()
    {
        Vector3 targetPosition = outSpots[randomOutSpot].gameObject.transform.position + ghostPivot;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (waitTime <= 0f)
            {
                if (takenTargetMemory != null) // Obje varsa objeyi birak ve adamin canindan gotur!!
                {
                    takenTargetMemory.transform.SetParent(takenTargetMemory.transform);
                    ChangeLayers(takenTargetMemory, "Default");
                    takenTargetMemory = null;
                    health.LoseHealth(this.Level);
                    takenTargetMemory = null;
                }
                
                randomSpotCounter = 1;
                goTarget = true;
                waitTime = Random.Range(1, 6);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            ChangeLayers(gameObject, "Not Solid");
        }
        if (other.tag == "Memories")
        {
            takenTargetMemoryStartTransform = other.transform;
            other.transform.SetParent(ParentToHoldObject.transform);
            other.transform.localPosition = ParentToHoldObject.transform.position;
            ChangeLayers(other.gameObject, "Not Solid");
            takenTargetMemory = other.gameObject;
            goTarget = false;
            randomTarget = Random.Range(0, m_TargetList.Length);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            ChangeLayers(gameObject, "Default");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            health.LoseHealth(this.Level);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            // ChangeLayers(gameObject, "Default");
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

    public Health getHealth()
    {
        return health;
    }

    private void OnBecameVisible()
    {
        FindObjectOfType<AudioManager>().Play("GhostAction");
    }
}
