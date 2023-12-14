using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGChase : MonoBehaviour
{
    public static BGChase instance;

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