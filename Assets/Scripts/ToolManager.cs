using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DisableIndicatorsWhenCorrectToolsGrabbed : MonoBehaviour
{
    [Header("Correct T Handles")]
    public XRGrabInteractable tHandleLeft;
    public XRGrabInteractable tHandleRight;

    [Header("UI References")]
    public GameObject[] wrongToolPopups; // All wrong tool messages
    public GameObject[] arrowIndicators; // All arrows (including correct one)

    private bool leftGrabbed = false;
    private bool rightGrabbed = false;

    void Start()
    {
        // Subscribe to XR grab events
        if (tHandleLeft)
        {
            tHandleLeft.selectEntered.AddListener(OnLeftGrabbed);
            tHandleLeft.selectExited.AddListener(OnLeftReleased);
        }
        if (tHandleRight)
        {
            tHandleRight.selectEntered.AddListener(OnRightGrabbed);
            tHandleRight.selectExited.AddListener(OnRightReleased);
        }
    }

    private void OnLeftGrabbed(SelectEnterEventArgs args)
    {
        leftGrabbed = true;
        CheckBothGrabbed();
    }

    private void OnRightGrabbed(SelectEnterEventArgs args)
    {
        rightGrabbed = true;
        CheckBothGrabbed();
    }

    private void OnLeftReleased(SelectExitEventArgs args)
    {
        leftGrabbed = false;
    }

    private void OnRightReleased(SelectExitEventArgs args)
    {
        rightGrabbed = false;
    }

    private void CheckBothGrabbed()
    {
        // When both correct T handles are grabbed:
        if (leftGrabbed && rightGrabbed)
        {
            // Disable wrong tool popups
            foreach (var popup in wrongToolPopups)
                if (popup) popup.SetActive(false);

            // Disable all arrows
            foreach (var arrow in arrowIndicators)
                if (arrow) arrow.SetActive(false);

            // Optional: make permanent
            this.enabled = false;
        }
    }
}
