using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutScenes : MonoBehaviour
{
    Boolean cutScene1playing = false;
   Boolean cutScene2playing = false;
    Boolean cutScene3playing = false;

    public GameObject playerCam;
    public GameObject cutScene1cam;
    public GameObject cutScene2cam;
    public GameObject cutScene3cam;
    void Start()
    {
        playerCam.SetActive(true);
        cutScene1cam.SetActive(false);
        cutScene2cam.SetActive(false);
        cutScene3cam.SetActive(false);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (!cutScene1playing&&other.gameObject.name=="Cutscene1")
        {
            cutScene1playing=true;
            playerCam.SetActive(false);
            cutScene1cam.SetActive(true) ;
            Invoke("SwitchToPlayerCam", 7.1f);
        }
        else if(!cutScene2playing&&other.gameObject.name=="Cutscene2")
        {
            cutScene2playing = true;
            playerCam.SetActive(false);
            cutScene2cam.SetActive(true);
            Invoke("SwitchToPlayerCam", 7.1f);
        }
        else if(!cutScene3playing&&other.gameObject.name=="Cutscene3")
        {
            cutScene3playing=true;
            playerCam.SetActive(false);
            cutScene3cam.SetActive(true) ;
            Invoke("SwitchToPlayerCam", 26f);

        }
        else if(other.gameObject.name=="NextScene")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void SwitchToPlayerCam()
    {
        playerCam.SetActive(true );
        cutScene1cam.SetActive(false) ;
        cutScene2cam.SetActive(false);
        cutScene3cam.SetActive(false) ;
    }
}
