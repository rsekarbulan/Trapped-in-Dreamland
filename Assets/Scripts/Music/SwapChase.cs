using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapChase : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Convo After Chase Scene")
            BGChase.instance.GetComponent<AudioSource>().Pause();
    }
}