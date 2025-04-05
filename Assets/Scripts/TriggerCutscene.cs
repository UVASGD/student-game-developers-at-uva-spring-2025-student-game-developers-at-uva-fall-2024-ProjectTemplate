using UnityEngine;
using DG.Tweening;
public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] Vector3 cameraForceDirecrion;
    [SerializeField] AnimationClip anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.Init(false, false, DOTween.logBehaviour);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void forceCamera(GameObject head)
    {
        PlayerCameraMovement pcm = head.GetComponent<PlayerCameraMovement>();
        pcm.lockPan();
        head.transform.DORotate(cameraForceDirecrion, 0.5f);
        if(anim != null)
        {
            
        }
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
