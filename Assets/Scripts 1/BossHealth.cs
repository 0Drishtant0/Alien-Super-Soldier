
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth : MonoBehaviour
{    
    //Health elements
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    public UIHealthBar healthBar;
    public GameObject fire;
    AiAgent agent;
    
    //AI elements
    NavMeshAgent Nagent;

    SkinnedMeshRenderer skinnedMeshRenderer;

   Animator animator;
    //effects
    public float glowIntensity;
    public float glowDuration;
    float glowTimer;


    //random counter
    int count1 = 0;//to enter the rage state only once



    //main Camera
    public GameObject maincam;
    public GameObject ragecam;
    
    void Start()
    {
        ragecam.SetActive(false);
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        agent =  GetComponent<AiAgent>();
        animator = GetComponent<Animator>();
        Nagent = GetComponent<NavMeshAgent>();
       
        currentHealth = maxHealth;
        
       
        
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;

        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        animator.SetTrigger("damage");
        currentHealth -= amount;
       
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);

      
    
        if (currentHealth < maxHealth*.25 && count1==0)
        {




                GameObject[] firespots = GameObject.FindGameObjectsWithTag("FireSpots");
                foreach(GameObject fires in  firespots) {
                    Instantiate(fire,fires.transform.position, Quaternion.Euler(-90, 0, -90));
                }

               
        
                count1++;




             agent.stateMachine.ChangeState(AiStateId.Idle);
                maincam.SetActive(false);
                ragecam.SetActive(true);
                Invoke("resetback",129f);


        }
        if (currentHealth <= 0.0f)
        {
            Die();
           
             
            Nagent.speed = 0;


        }

        glowTimer = glowDuration;
    }
    private void resetback()
    {
        ragecam.SetActive(false);
        maincam.SetActive(true );

        agent.stateMachine.ChangeState(AiStateId.Rage);

    }
    private void Die()
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        agent.stateMachine.ChangeState(AiStateId.Death);
    }

    void Update()
    {
        glowTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(glowTimer / glowDuration);
        float intensity = lerp * glowIntensity + 1.0f;
        skinnedMeshRenderer.materials[0].color = Color.white* intensity;
        skinnedMeshRenderer.materials[1].color = Color.white * intensity;
    }
}
