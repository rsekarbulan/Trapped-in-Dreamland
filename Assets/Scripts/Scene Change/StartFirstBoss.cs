using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFirstBoss : MonoBehaviour
{

    public Fader Fader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StartFirstBoss"))
        {
            Destroy(collision.gameObject);
            Fader.FadeToLevel(3);
        }
    }
}
