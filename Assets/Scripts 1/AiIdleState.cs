using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }
    public void Enter(AiAgent agent)
    {
        Debug.Log("entered the idle state");
       
    }


    public void Update(AiAgent agent)
    {
         
    }
    public void Exit(AiAgent agent)
    {
        Debug.Log("Left the Idle state");
    }

}
