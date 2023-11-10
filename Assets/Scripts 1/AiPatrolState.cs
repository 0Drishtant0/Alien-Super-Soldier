using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class AiPatrolState : AiState
{

    public Vector3 walkPoint;
    public  bool walkPointSet;
  




    public AiStateId GetId()
    {
        return AiStateId.Patrol;

    }
    public void Enter(AiAgent agent)
    {

        Debug.Log("entered the patrol state");
        walkPointSet = false;

    }

    public void Exit(AiAgent agent)
    {
       Debug.Log("Left the Patrol state");
    }


    public void Update(AiAgent agent)
    {
        Vector3 direction = agent.playerTransform.position - agent.transform.position;
        if (direction.magnitude < agent.config.maxSightDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
        if (!walkPointSet)
        {
           
            SearchWalkPoint(agent);

            Debug.Log("sending navmesh agent to destination");
            agent.navMeshAgent.destination = walkPoint;

        }
            
          

    
           

        Vector3 distanceToWalkPoint = agent.transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude <= 5f)
        {
            walkPointSet = false;
        }
           
    }


    private void SearchWalkPoint(AiAgent agent)
    {
        //Calculate random point in range
        float randomZ = Random.Range(-agent.config.walkPointRange, agent.config.walkPointRange);
        float randomX = Random.Range(-agent.config.walkPointRange, agent.config.walkPointRange);

        walkPoint = new Vector3(agent.transform.position.x + randomX, agent.transform.position.y, agent.transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -agent.transform.up, 2f, agent.config.whatisGround))
        {
            walkPointSet = true;
            Debug.Log("walkpointSet is set to true");

        }
    }

}
