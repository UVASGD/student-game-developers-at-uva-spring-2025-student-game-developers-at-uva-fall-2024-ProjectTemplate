using UnityEngine;
using UnityEngine.SceneManagement;
public class PaintingSceneChangeInteactable : GameplayUIInteractable
{
    [SerializeField] private string sceneName;
    public override void Interact()
    {
        gameplayUI.activate(() => { loadScene(); });
    }
    private void loadScene()
    {
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
