using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGhostController : MonoBehaviour
{
    private Health health;
    public EnemyLevel Level = EnemyLevel.Level_1;
    private LifeManager LifeManager;
    private TimeManager timeManager;

    private float waitTime;
    public float speed;
    private bool goTarget;

    private int randomSpotCounter;
    private int randomSpot;
    private int randomTarget;
    private int randomOutSpot;

    public List<GameObject> m_TargetList;
    public Transform[] moveSpots;
    public Transform[] outSpots;

    public GameObject ParentToHoldObject;
    public GameObject takenTargetMemory;

    private Vector3 ghostPivot = new Vector3(-2.98f, -0.26f, 0.47f);

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        LifeManager = GameObject.FindObjectOfType<LifeManager>();
        timeManager = GameObject.FindObjectOfType<TimeManager>();
        waitTime = Random.Range(1, 6);
        randomSpot = Random.Range(0, moveSpots.Length);
        randomTarget = Random.Range(0, m_TargetList.Count);
        randomOutSpot = Random.Range(0, outSpots.Length);
        randomSpotCounter = 1;
        goTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.isFinish || LifeManager.isFinish)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
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
    }

    void GoRandomSpot()
    {
        Vector3 targetPosition = moveSpots[randomSpot].position + ghostPivot;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
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
                    m_TargetList.Remove(takenTargetMemory);
                    Destroy(takenTargetMemory);
                    takenTargetMemory = null;
                    LifeManager.LoseLife();
                    randomTarget = Random.Range(0, m_TargetList.Count);
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
        if (other.tag == "Memories" && goTarget && takenTargetMemory == null)
        {
            other.transform.SetParent(ParentToHoldObject.transform);
            other.transform.localPosition = ParentToHoldObject.transform.position;
            ChangeLayers(other.gameObject, "Not Solid");
            takenTargetMemory = other.gameObject;
            other.GetComponent<Collider>().isTrigger = true;

            goTarget = false;
            randomTarget = Random.Range(0, m_TargetList.Count);
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

    public Health getHealth()
    {
        return health;
    }

    private void OnBecameVisible()
    {
        FindObjectOfType<AudioManager>().Play("GhostAction");
    }

    public void GetOut()
    {
        Debug.Log("Ghost Geberdi");
        randomSpotCounter = 5;
        if (takenTargetMemory != null)
        {
            takenTargetMemory.GetComponent<Memory>().GhostFeltTheMemory();
            takenTargetMemory = null;

        }
        goTarget = false;

    }
}
