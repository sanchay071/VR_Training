using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CuttingToolSelector : MonoBehaviour
{
    [Header("Two-Handed Tool References")]
    public XRGrabInteractable rightHandle;      // T-handle right
    public XRGrabInteractable leftHandle;       // T-handle left

    [Header("Marker Settings")]
    public GameObject marker;                    // Glowing torus
    public float rotationSpeed = 50f;

    private bool rightGrabbed = false;
    private bool leftGrabbed = false;
    private bool markerShown = false;

    void Start()
    {
        // Add listeners to T-handle for two-handed tool
        if (rightHandle != null)
        {
            rightHandle.selectEntered.AddListener((args) => { rightGrabbed = true; CheckTwoHandedGrab(); });
            rightHandle.selectExited.AddListener((args) => { rightGrabbed = false; });
        }

        if (leftHandle != null)
        {
            leftHandle.selectEntered.AddListener((args) => { leftGrabbed = true; CheckTwoHandedGrab(); });
            leftHandle.selectExited.AddListener((args) => { leftGrabbed = false; });
        }

        // Hide marker at start
        if (marker) marker.SetActive(false);
    }

    void Update()
    {
        // Rotate marker if active
        if (marker && marker.activeSelf)
            marker.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void CheckTwoHandedGrab()
    {
        if (!markerShown && rightGrabbed && leftGrabbed)
        {
            if (marker) marker.SetActive(true);
            markerShown = true; // prevent re-triggering
        }
    }
}
