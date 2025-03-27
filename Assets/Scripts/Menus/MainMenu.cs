using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject settings_menu;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Debug.Log("QUIT");
        Application.Quit();
    }

    public void Settings()
    {
        if (main_menu != null && settings_menu != null)
        {
            main_menu.SetActive(!main_menu.activeSelf);
            settings_menu.SetActive(!settings_menu.activeSelf);
        }
    }
}
