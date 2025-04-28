using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pause_menu;

    // [SerializeField] private MouseSensitivityHandler mouseHandler;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K))
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // mouseHandler.enabled = false;
        // StartCoroutine(UnlockCursorNextFrame());
    }

    /*
    private IEnumerator UnlockCursorNextFrame()
    {
        yield return null; // Wait one frame

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Debug.Log("[PauseMenu] Cursor unlocked + visible");
    }
    */

    public void Resume()
    {
        MenuController menuController = FindAnyObjectByType<MenuController>();
        if (menuController != null)
        {
            menuController.ApplyButton();
        }

        pause_menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        /*
        if (mouseHandler != null)
        {
            mouseHandler.enabled = true; // Reactivate so new sensitivity is used
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        */
        // Debug.Log($"[MouseHandler] isActive = {mouseHandler.isActive}");
    }

    public void MainMenuButton()
    {
        MenuController menuController = FindAnyObjectByType<MenuController>();
        if (menuController != null)
        {
            menuController.PlayButtonSound();
        }

        SceneManager.LoadScene(0);

        // mouseHandler.enabled = false;

        AudioManager.audioManagerInstance.StopMusic();
        AudioManager.audioManagerInstance.PlayMusic(AudioManager.audioManagerInstance.menuBackground);
    }
}