using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class CharacterAiming : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15;
    [SerializeField] private float aimDuration = 0.3f;
    [SerializeField] private Rig aimLayer;
    [SerializeField] private AudioSource bulletSound;

    private Camera mainCamera;
    private RaycastWeapon weapon;

    public bool canShoot = true;




    
    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
      
         
        
    }

    // Update is called once per frame
    private void FixedUpdate()



    {


      


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;  
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);  
        }
    }

    private void LateUpdate()
    {
        if (canShoot)
        {
            if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift))
            {
                aimLayer.weight += Time.deltaTime / aimDuration;
                if (Input.GetMouseButtonDown(0))
                {
                    weapon.StartFiring();
                    bulletSound.Play();
                }
                if (weapon.isFiring)
                {
                    weapon.UpdateFiring(Time.deltaTime);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    weapon.StopFiring();
                }
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }
            weapon.UpdateBullets(Time.deltaTime);
        }
    }
}
