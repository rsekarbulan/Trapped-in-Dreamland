using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToFirstBossScene : MonoBehaviour
{
    void OnEnable()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}