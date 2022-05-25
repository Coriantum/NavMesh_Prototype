using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentDetection : MonoBehaviour
{
    public Transform agentTarget;
    public Transform initialPoint;
    public Transform routePoint1;
    public Transform routePoint2;

    NavMeshAgent agent;
    AgentState state;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = AgentState.Idle;
        destination = transform.position;
    }

    private void FixedUpdate() {

        bool inRangeAndVisible = IsInRangeAndVisible();
        bool inRange = IsInRange();
        
        // Dicho switch llamará al metodo que establece los estados
        switch(state) 
        {
            case AgentState.Idle:
                if(inRangeAndVisible){ 
                    SetState(AgentState.Chasing);
                }
                break;
            case AgentState.Chasing:
                if(! inRangeAndVisible){
                    SetState(AgentState.Returning);
                }
                else{
                    destination = agentTarget.position;
                }
                break;
            case AgentState.Returning:
                if(inRangeAndVisible){
                    SetState(AgentState.Chasing);
                } else if(agent.isStopped){
                    SetState(AgentState.Idle);
                } 
                break;
            
        }

        agent.destination = destination; // En todo momento el agente tendrá un destino
        
    }

    private bool IsInRange(){
        float agentsDistance= Vector3.Distance(agentTarget.position, transform.position); // Distancia entre los dos agentes
        int distanceToAgent = 20; // Distancia 

        return (agentsDistance < distanceToAgent);
    }


    private bool IsInRangeAndVisible(){
        RaycastHit hitData; // Info del rayo
        Vector3 directionToTarget = (agentTarget.position - transform.position).normalized; // Direccion del rayo
        int distanceToAgent = 30; // Distancia del rayo

        // Establecer limites de rotacion para limitar el angulo de vision del enemigo
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        float angleLimit = 30f;

        // Limites para dibujar los rayos
        Vector3 leftLimit = Quaternion.AngleAxis(angleLimit, transform.forward) * transform.forward;
        Vector3 rightLimit = Quaternion.AngleAxis(-angleLimit, transform.forward) * transform.forward;

        Debug.DrawLine(transform.position, directionToTarget * distanceToAgent, Color.yellow);
        Debug.DrawLine(transform.position, leftLimit * 10, Color.red);
        Debug.DrawLine(transform.position, rightLimit * 10, Color.green);

        // Devuelvo la condición: Si cumple el raycast, la distancia entre agentes es menor que la distancia del rayo y el angulo es menor que 30
        return (Physics.Raycast(transform.position, directionToTarget, out hitData, distanceToAgent, -1, QueryTriggerInteraction.Ignore) && hitData.collider.tag == "Agent" && angle < angleLimit);

    }
    
    /// <summary>
    /// Metodo que llama un nuevo estado, y el agente realizará una acción u otra
    /// </summary>
    /// <param name="newState"> Nuevo estado </param>
    void SetState(AgentState newState){
        if(newState != state){
            Debug.Log("new state" + newState);
            state = newState;
            switch(newState){
                case AgentState.Idle:
                    break;
                case AgentState.Chasing:
                    break;
                case AgentState.Returning:
                    destination = initialPoint.position; // Estado Return: Vuelve a su posicion inicial
                    break;

            }
        }
        
    }



}

public enum AgentState{
    Idle, // Esperando
    Chasing, // Persigue
    Returning, // Vuelve a su posicion 
    Centinela
}
