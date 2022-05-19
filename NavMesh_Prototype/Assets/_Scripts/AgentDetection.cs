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
        RaycastHit hitData; // Info del rayo
        Vector3 directionToTarget = (agentTarget.position - transform.position).normalized; // Direccion del rayo
        int distanceToAgent = 15; // Distancia del rayo
        float agentsDistance= Vector3.Distance(agentTarget.position, transform.position); // Distancia entre los dos agentes

        Debug.DrawRay(transform.position, directionToTarget * distanceToAgent, Color.yellow);
        
    // Si cumple el raycast y la distancia entre agentes es menor que la distancia del rayo
        if(Physics.Raycast(transform.position, directionToTarget, out hitData, distanceToAgent, -1, QueryTriggerInteraction.Ignore) && hitData.collider.tag == "Agent"){
            agent.destination = agentTarget.position;
            Debug.Log("Va al agent"); 

        } else {
            Debug.Log("Va al punto inicial");
            agent.destination = initialPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = agentTarget.position;
    }
}
