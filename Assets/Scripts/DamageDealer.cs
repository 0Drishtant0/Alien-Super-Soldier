using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;


    private void Start()
    {
        canDealDamage = false;  
       
    }

    private void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            int layerMask = 1 << 9;
            Vector3 strikeDirection = transform.forward;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            { 

                      //Adding strike impulse
                    var rb2d = hit.collider.GetComponent<Rigidbody>();

                    if (rb2d != null)
                {
                    rb2d.AddForceAtPosition(strikeDirection * 20, hit.point, ForceMode.Impulse);
                }

                    //registering damage
                    var HitBox = hit.collider.GetComponent<HitBox>();
                    if(HitBox)

                {
                    HitBox.onSwordHit(weaponDamage, strikeDirection);
                }
                     


            }
        }
    }

    public void StartDealDamage()
    {  

        canDealDamage =true;
       
    }
    public void EndDealDamage()
    {
        canDealDamage =false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position-transform.up*weaponLength);
    }
}
