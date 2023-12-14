using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMain : MonoBehaviour
{
    public static BGMain instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}