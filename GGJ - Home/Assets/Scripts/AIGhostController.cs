using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIGhostController : MonoBehaviour
{

    private NavMeshAgent m_NavMeshAgent;
    public GameObject m_Target;

    private Health health;
    public EnemyLevel Level = EnemyLevel.Level_1;

    public float startWaitTime;
    private float waitTime;

    private int randomSpot;
    private Transform[] moveSpots;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_NavMeshAgent.SetDestination(m_Target.transform.position);
        if (waitTime <= 0f)
        {

        }
        else
        {
            waitTime -= Time.deltaTime;
        }
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
