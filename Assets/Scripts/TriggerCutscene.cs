using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] Vector3 cameraForceDirecrion;
    [SerializeField] AnimationClip anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void forceCamera(GameObject head)
    {

    }
    void startAnimation()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            GameObject player = other.gameObject;
            GameObject head = player.transform.GetChild(0).gameObject;//head should be first child
            forceCamera(head);
            startAnimation();
        }

    }
}
