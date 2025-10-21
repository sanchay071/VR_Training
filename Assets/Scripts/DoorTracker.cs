using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorTaskTrigger : MonoBehaviour
{
    public TaskUIManager taskUIManager;
    private bool taskCompleted = false;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Optional: do something when grabbed
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (!taskCompleted && IsDoorOpen())
        {
            taskUIManager.CompleteTask();
            taskCompleted = true;
        }
    }

    private bool IsDoorOpen()
    {
        // Example threshold: 70 degrees rotation on Y-axis
        return transform.localEulerAngles.z > 70f;
    }
}
