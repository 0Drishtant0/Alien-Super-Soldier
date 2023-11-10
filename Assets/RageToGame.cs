using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RageToGame : MonoBehaviour
{
    public GameObject maincam;
    public GameObject storyCam;
    void Start()
    {
        maincam.SetActive(false);
        storyCam.SetActive(true);
        Invoke("GoBacktofight", 129f);
        
    }

public void GoBacktofight ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
