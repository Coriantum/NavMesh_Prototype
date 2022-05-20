using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentDetection : MonoBehaviour
{
    public Transform agentTarget;
    public Transform initialPoint;
    NavMeshAgent agent;

    public Transform vision;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate() {
        RaycastHit hitData; // Info del rayo
        Vector3 directionToTarget = (agentTarget.position - transform.position).normalized; // Direccion del rayo
        int distanceToAgent = 15; // Distancia del rayo
       // float agentsDistance= Vector3.Distance(agentTarget.position, transform.position); // Distancia entre los dos agentes

        // Establecer limites de rotacion para limitar el angulo de vision del enemigo
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        float angleLimit = 30f;

        // Limites para dibujar los rayos
        Vector3 leftLimit = Quaternion.AngleAxis(-angleLimit, transform.forward) * transform.forward;
        Vector3 rightLimit = Quaternion.AngleAxis(angleLimit, transform.forward) * transform.forward;

        Debug.DrawRay(transform.position, directionToTarget * distanceToAgent, Color.yellow);
        Debug.DrawRay(transform.position, leftLimit * 10, Color.red);
        Debug.DrawRay(transform.position, rightLimit * 10, Color.green);
        
    // Si cumple el raycast y la distancia entre agentes es menor que la distancia del rayo
        if(Physics.Raycast(transform.position, directionToTarget, out hitData, distanceToAgent, -1, QueryTriggerInteraction.Ignore) && hitData.collider.tag == "Agent" && angle < angleLimit){
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
