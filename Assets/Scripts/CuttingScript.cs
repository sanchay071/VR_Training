using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CuttingMarkerTrigger : MonoBehaviour
{
    [Header("Handle References")]
    public XRGrabInteractable rightHandle;
    public XRGrabInteractable leftHandle;

    [Header("Visuals")]
    public GameObject marker;              // The spinning torus
    public float rotationSpeed = 50f;      // Speed of marker rotation
    public GameObject startEdgeTextArrow;  // Arrow + text at cutting start

    [Header("Cutting Settings")]
    public bool cuttingActive = false;     // True when cutting is enabled

    private bool rightGrabbed = false;
    private bool leftGrabbed = false;
    private bool rightInside = false;
    private bool leftInside = false;

    void Start()
    {
        // Hide visuals initially
        if (marker) marker.SetActive(false);
        if (startEdgeTextArrow) startEdgeTextArrow.SetActive(false);

        // Add grab listeners for both handles
        if (rightHandle != null)
        {
            rightHandle.selectEntered.AddListener((args) => { rightGrabbed = true; CheckBothGrabbed(); });
            rightHandle.selectExited.AddListener((args) => { rightGrabbed = false; });
        }

        if (leftHandle != null)
        {
            leftHandle.selectEntered.AddListener((args) => { leftGrabbed = true; CheckBothGrabbed(); });
            leftHandle.selectExited.AddListener((args) => { leftGrabbed = false; });
        }
    }

    void Update()
    {
        // Rotate the marker if it's active
        if (marker && marker.activeSelf)
            marker.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void CheckBothGrabbed()
    {
        // When both handles are grabbed, activate the marker
        if (rightGrabbed && leftGrabbed && marker && !marker.activeSelf)
        {
            marker.SetActive(true);
            Debug.Log("Both handles grabbed — marker activated.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect when both handles are inside the marker trigger
        if (other.gameObject == rightHandle.gameObject)
            rightInside = true;

        if (other.gameObject == leftHandle.gameObject)
            leftInside = true;

        if (rightInside && leftInside && !cuttingActive)
            StartCutting();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == rightHandle.gameObject)
            rightInside = false;

        if (other.gameObject == leftHandle.gameObject)
            leftInside = false;
    }

    private void StartCutting()
    {
        cuttingActive = true;

        // Hide marker and show start arrow/text
        if (marker) marker.SetActive(false);
        if (startEdgeTextArrow) startEdgeTextArrow.SetActive(true);

        Debug.Log("Cutting enabled — begin at the edge!");
    }
}
