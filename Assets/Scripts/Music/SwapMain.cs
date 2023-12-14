using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapMain : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "First Boss")
            BGMain.instance.GetComponent<AudioSource>().Pause();
    }
}