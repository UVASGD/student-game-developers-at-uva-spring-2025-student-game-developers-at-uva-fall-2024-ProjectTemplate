using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChangeToCutscene : MonoBehaviour
{
    public string sceneName; // The name of the scene to load

    public void loadScene(float delay)
    {
        // Start a coroutine to load the scene after the specified delay
        StartCoroutine(LoadSceneAfterDelay(delay));
    }
    
    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        if (SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
        {
            Debug.LogWarning(sceneName +" doesnt Exists");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}