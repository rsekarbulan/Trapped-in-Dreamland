using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFinalBoss : MonoBehaviour
{

    public Fader fader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StartFinalBoss"))
        {
            Destroy(collision.gameObject);
            /*SceneManager.LoadScene("To Final Boss");*/
            fader.FadeToLevel(11);
        }
    }
}
