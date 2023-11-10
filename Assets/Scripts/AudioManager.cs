using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource footStepClip;
    [SerializeField] private AudioSource swordSwingClip;
    [SerializeField] private AudioSource jumpClip;
    [SerializeField] private AudioSource drawWeaponClip;
    [SerializeField] private AudioSource sheathWeaponClip;
    [SerializeField] private AudioSource attack1Clip;
    [SerializeField] private AudioSource attack2Clip;
    [SerializeField] private AudioSource attack3Clip;
    [SerializeField] private AudioSource attack4Clip;
    public void PlayFootStep()
    {
        footStepClip.Play();
    }

    public void PlayJump()
    {
        jumpClip.Play(); 
    }

    public void PlayDrawWeapon()
    {
        drawWeaponClip.Play();
    }

    public void PlaySheathWeapon()
    {
        sheathWeaponClip.Play();
    }

    public void PlayAttack1()
    {
        attack1Clip.Play();
    }

    public void PlayAttack2()
    {
        attack2Clip.Play();
    }

    public void PlayAttack3()
    {
        attack3Clip.Play();
    }
    public void PlayAttack4()
    {
        attack4Clip.Play();
    }
}
