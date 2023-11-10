using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
   
    float timer = 0.0f;
    

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {
        Debug.Log("entered the Chase player State");
    }

    public void Update(AiAgent agent)
    {
        agent.transform.LookAt(agent.playerTransform.position);

        if (!agent.enabled)
        {
            return;
        }
        timer -= Time.deltaTime;

        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }
        if (timer <= 0.0f)
        {
            Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0f;

            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.position;
                }
            }  

            timer = agent.config.maxTime;
        }



        Vector3 dir = agent.playerTransform.position - agent.transform.position;
        if (dir.magnitude > agent.config.maxSightDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }
        if (dir.magnitude < agent.config.attackRange)
        {
            agent.stateMachine.ChangeState(AiStateId.Attack);
        }

    }
  
    public void Exit(AiAgent agent)
    {
        Debug.Log("left the chase state");
    }


}
