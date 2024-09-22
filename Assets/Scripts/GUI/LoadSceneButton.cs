using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
        // You can use SceneManager.LoadSceneAsync() to load the scene asynchronously, which can improve performance for large scenes.
        // SceneManager.LoadSceneAsync()
        //
        // You can use SceneManager.GetActiveScene().name to get the name of the current scene and load it using
        //
        // You can use SceneManager.LoadScene(1) to load the scene at index 1 in the Build Settings.

    }
}
