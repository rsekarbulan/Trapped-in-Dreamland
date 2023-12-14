using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishChase : MonoBehaviour
{
    public Fader fader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishChase"))
        {
            Destroy(collision.gameObject);
            /*SceneManager.LoadScene("To Chase Scene");*/
            fader.FadeToLevel(8);
        }
    }
}
