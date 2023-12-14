using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public Fader fader;

    void OnEnable()
    {
        // Check the name of the previous scene
        string currentScene = SceneManager.GetActiveScene().name;

        // If sekarang scene Opening
        if (currentScene == "Opening")
        {
            /*SceneManager.LoadScene("Main Game", LoadSceneMode.Single);*/
            fader.FadeToLevel(2);
        }

        if (currentScene == "To First Boss")
        {
            /*SceneManager.LoadScene("Main After First Boss", LoadSceneMode.Single);*/
            fader.FadeToLevel(4);
        }

        if(currentScene == "To Chase Scene")
        {
            /*SceneManager.LoadScene("After Chase Scene", LoadSceneMode.Single);*/
            fader.FadeToLevel(7);
        }

        if (currentScene == "After Chase Scene")
        {
            /*SceneManager.LoadScene("Convo After Chase Scene", LoadSceneMode.Single);*/
            fader.FadeToLevel(9);
        }

        if (currentScene == "Convo After Chase Scene")
        {
            /*SceneManager.LoadScene("Main After Chase Scene", LoadSceneMode.Single);*/
            fader.FadeToLevel(10);
        }

        if (currentScene == "To Final Boss")
        {
            /*SceneManager.LoadScene("Final Boss", LoadSceneMode.Single);*/
            fader.FadeToLevel(12);
        }

        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
    }

}