using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : MonoBehaviour
{

    public GameObject electricBall;
    public GameObject electricHand;
    public Transform playerTransform;
    public NavMeshAgent agent;
    public float momentum;
    public float damage = 50f;
   
    public Animator playerAnimator;
    public AudioSource energy;
    public AudioSource earthShatter;

    public GameObject earthShatterPrefab;
   
    
    CharacterLocomotion charL;
    public PlayerHealth health;

     

   public void ThrowElectricBall1()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        charL = player.GetComponent<CharacterLocomotion>();
        health = player.GetComponent<PlayerHealth>();
        
        Vector3 direction = (playerTransform.position - transform.position);

        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform.position);
        Rigidbody rb = Instantiate(electricBall, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        energy.Play();
        rb.AddForce(direction*momentum, ForceMode.Impulse);
        
        health.TakeDamage(damage);
        
       

    }
    public void ShatterEarth()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Instantiate(earthShatterPrefab,player.transform.position, Quaternion.identity);
        earthShatter.Play();
        Debug.Log("ye ho rha1");
        health.TakeDamage(damage);  


    }

}
