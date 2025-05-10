using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject panel;

    public void Exit() {
        panel.SetActive(false);
    }
    
    public void setVolume (float volume) {
        Debug.Log("Volume: " + volume);
    }

    public void setBrightness (float brightness) {
        Debug.Log("Brightness: " + brightness);
    }

    public void setSensitivity (float sensitivity) {
        Debug.Log("Sensitivity: " + sensitivity);
    }

    public void setGraphics (float graphics) {
        Debug.Log("Graphics: " + graphics);
    }
}
