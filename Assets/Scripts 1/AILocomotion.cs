using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AILocomotion : MonoBehaviour
{

    

    NavMeshAgent agent;
    Animator animator;

    private void Start()
    {
       

            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

        

    }

    private void Update()
    {
        

        if (agent.hasPath)
        {
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
        else
            animator.SetFloat("speed", 0);



    }
}
