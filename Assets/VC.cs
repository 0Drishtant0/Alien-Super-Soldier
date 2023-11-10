using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VC : MonoBehaviour
{
    public VideoPlayer player;

    void Start()
    {
        player.loopPointReached += EndReached;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            {
            EndReached(player);
        }

    }
    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}

