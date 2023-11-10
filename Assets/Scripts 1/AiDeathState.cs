using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Death; 
    }

    public void Enter(AiAgent agent)
    {

        Debug.Log("entered the death state");
        agent.ragdoll.ActivateRagdoll();
        agent.healthBar.gameObject.SetActive(false);
    }

    public void Exit(AiAgent agent)
    {
        Debug.Log("drrrrr bad guy ded");
    }

    public void Update(AiAgent agent)
    {
       
    }

}
