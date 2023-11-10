using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;

    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Ragdoll ragdoll;
    public UIHealthBar healthBar;
    private GameObject Cam;

    public Transform playerTransform;
    void Start()

    {

        skinnedMeshRenderer= GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        healthBar = Cam.GetComponentInChildren<UIHealthBar>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine = new AiStateMachine(this);
        
        //registering new states
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiPatrolState());
        stateMachine.RegisterState(new AiRageState());
        stateMachine.RegisterState(new AiAttackState());


        stateMachine.ChangeState(initialState);
    }

 
    void Update()
    {
        stateMachine.Update();
    }
}
