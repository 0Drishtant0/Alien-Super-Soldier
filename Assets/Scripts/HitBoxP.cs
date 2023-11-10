using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxP : MonoBehaviour
{
    public PlayerHealth health;
    
    public void PlayerTakeDamage(float damage)
    {
      Debug.Log("Damage taken");
      health.TakeDamage(damage);

    }
}
