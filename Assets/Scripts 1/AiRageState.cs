
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class AiRageState : AiState
{

    Animator animator;

    public float timer;
    int numberofattacks = 0;
    BossHealth health;
    PlayerHealth playerHealth;
    public void Enter(AiAgent agent)
    {


        Debug.Log("Rage State mein toh aa gye");
        timer = agent.config.timeBetweenAttacks;
        animator = agent.GetComponent<Animator>();
        
        health = agent.GetComponent<BossHealth>();
        health.currentHealth = health.maxHealth/2;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.currentHealth = 100f;
    }

    public void Exit(AiAgent agent)
    {
        Debug.Log("left the rage state , TH:FOIEFBIEHLLGIIGUGYLGYIGIG");
    }

    public AiStateId GetId()
    {
        return AiStateId.Rage;

    }

    public void Update(AiAgent agent)
    {   

        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {

            agent.transform.LookAt(agent.playerTransform);
            animator.SetTrigger("attack2");
            timer = agent.config.timeBetweenAttacks;
            numberofattacks++;

        }
      


    }
}
