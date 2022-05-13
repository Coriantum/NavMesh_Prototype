using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentDetection : MonoBehaviour
{
    public Transform agentTarget;
    public Transform initialPoint;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate() {
        RaycastHit hitData;
        if(Physics.Raycast(transform.position, agentTarget.position - transform.position, out hitData)){
            agent.destination = initialPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = agentTarget.position;
    }
}
