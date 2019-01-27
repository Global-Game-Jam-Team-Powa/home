using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGhostController : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;

    private Health health;
    public EnemyLevel Level = EnemyLevel.Level_1;

    public float startWaitTime;
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

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        // m_TargetList = FindObjectsOfType<TargetMemory>();

        waitTime = startWaitTime;

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
            GameObject m_Target = m_TargetList[randomTarget].gameObject;
            m_NavMeshAgent.SetDestination(m_Target.transform.position);
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
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0f)
            {
                randomSpotCounter++;
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void GoRandomOutSpot()
    {
        GameObject m_Target = outSpots[randomOutSpot].gameObject;
        m_NavMeshAgent.SetDestination(m_Target.transform.position);
        if (Vector3.Distance(transform.position, m_Target.transform.position) < 0.1f)
        {
            // TODO:Obje varsa objeyi bırak ve adamın canından götür!!
            randomSpotCounter = 1;
            goTarget = true;
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
            // TODO: buraya eline alma yap!
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
}
