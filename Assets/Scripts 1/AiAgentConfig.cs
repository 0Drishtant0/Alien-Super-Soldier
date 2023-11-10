using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float maxSightDistance = 12.0f;
    public float walkPointRange;
    public LayerMask whatisGround;
    public GameObject fire;
    public float timeBetweenAttacks;
    public float attackRange;
    
}
