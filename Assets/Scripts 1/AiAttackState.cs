using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : AiState
{

    Animator animator;
  
    public float timer ;
   
    public AiStateId GetId()
    {
        return AiStateId.Attack;
    }
    public void Enter(AiAgent agent)
    {
        Debug.Log("entered the Attack state");
     
        timer = agent.config.timeBetweenAttacks;
        animator = agent.GetComponent<Animator>();
    }

    public void Exit(AiAgent agent)
    {
        Debug.Log("left the attack state");
    }


    public void Update(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {

            agent.transform.LookAt(agent.playerTransform);
            animator.SetTrigger("attack1");
            timer = agent.config .timeBetweenAttacks;

        }

      Vector3 direction = agent.playerTransform.position - agent.transform.position; 
        if(direction.magnitude >agent.config.attackRange)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }


    }
}
