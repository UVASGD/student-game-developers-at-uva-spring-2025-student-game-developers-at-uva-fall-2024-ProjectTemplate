using UnityEngine;

public class TempParent : MonoBehaviour
{
    public static TempParent Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }
}
