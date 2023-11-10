using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerMaxHealth=100f;
    public float currentHealth ;
    Ragdoll ragdoll;
    GameObject Boss;
    AiAgent agent;
    public bool isInvincible = false;
    public PostProcessVolume Volume;
    public UIHealthBar healthBarplayer;
    Animator playerAnimator;

    void Start()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        ragdoll = GetComponent<Ragdoll>();
        Boss = GameObject.FindGameObjectWithTag("Enemy");
        agent = Boss.GetComponent<AiAgent>();
        currentHealth = playerMaxHealth;
        playerAnimator = GetComponent<Animator>();

    }


//Event Calls from rolling animations


    public void CanTakeDamage()
    {
        isInvincible = false;
    }
    public void  CannotTakeDamage()
    {
        isInvincible = true;
    }




    public void TakeDamage(float damage)
    {    
        Debug.Log("taking damage");

        if(!isInvincible)
        {

 
            currentHealth -= damage;
            healthBarplayer.SetHealthBarPercentage(currentHealth / playerMaxHealth);

        }
       
        if(currentHealth <=0.0f) 
        {
           
            Die();
        }
        
    }
    private void Die()
    {

        playerAnimator.SetBool("dead", true);
        Invoke("GameOver", 2f);
       //  agent.stateMachine.ChangeState(AiStateId.Patrol);


    }
    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }


}
