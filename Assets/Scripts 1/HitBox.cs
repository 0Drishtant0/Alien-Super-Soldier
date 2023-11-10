using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public BossHealth health;
        public void onSwordHit(float damage,Vector3 direction)
    {
       health.TakeDamage(damage, direction);
    }

    
    public void OnRayCastHit(RaycastWeapon weapon, Vector3 direction)
    {
        health.TakeDamage(weapon.damage, direction);
    }
}
