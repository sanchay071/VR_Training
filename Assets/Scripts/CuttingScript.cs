using System.Collections;
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

    [Header("Cutting Path")]
    public Collider[] cuttingPoints;       // 7 sphere colliders assigned in order
    public GameObject glassObject;         // Glass object to float after cutting

    [Header("Floating Settings")]
    public float floatDistance = 1f;       // Distance to float to the left
    public float floatSpeed = 0.3f;        // Speed of floating

    [Header("Cutting Settings")]
    public bool cuttingActive = false;     // True when cutting is enabled

    private int currentCutIndex = 0;       // Tracks which sphere user reached
    private bool rightGrabbed = false;
    private bool leftGrabbed = false;
    private bool rightInside = false;
    private bool leftInside = false;
    private bool cutComplete = false;

    private Vector3 floatStartPos;
    private bool isFloating = false;

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

        // Disable all cutting point colliders initially
        if (cuttingPoints != null)
        {
            foreach (var c in cuttingPoints)
                c.enabled = false;
        }
    }

    void Update()
    {
        // Rotate the marker if it's active
        if (marker && marker.activeSelf)
            marker.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Handle floating
        //if (isFloating && glassObject)
        //{
        //    Vector3 targetPos = floatStartPos + Vector3.right * floatDistance;
        //    glassObject.transform.position = Vector3.MoveTowards(
        //        glassObject.transform.position,
        //        targetPos,
        //        floatSpeed * Time.deltaTime
        //    );

        //    if (Vector3.Distance(glassObject.transform.position, targetPos) < 0.001f)
        //        isFloating = false;
        //}
    }

    private void CheckBothGrabbed()
    {
        if (cutComplete) return; // prevent marker reactivation after cut

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

        // Enable first cutting point
        if (cuttingPoints != null && cuttingPoints.Length > 0)
        {
            cuttingPoints[0].enabled = true;
            Debug.Log("First cutting point activated.");
        }
    }

    // Call this when a cutting point collider is triggered
    public void OnCuttingPointTriggered(Collider point)
    {
        if (!cuttingActive || cutComplete) return;

        if (point == cuttingPoints[currentCutIndex])
        {
            Debug.Log($"Cutting point {currentCutIndex + 1} reached!");

            // Disable current point
            point.enabled = false;

            // Move to next
            currentCutIndex++;

            if (currentCutIndex >= cuttingPoints.Length)
            {
                CompleteCut();
            }
            else
            {
                // Enable next point
                cuttingPoints[currentCutIndex].enabled = true;
            }
        }
    }
    private void CompleteCut()
    {
        cutComplete = true;
        isFloating = true;
        floatStartPos = glassObject.transform.position;

        Debug.Log("Cut complete! Glass floating left.");

        // Optional: add Rigidbody if glass should be interactable
        if (glassObject && glassObject.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = glassObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    
        // start coroutine from glass object to keep it running
        if (glassObject != null)
        {
            glassObject.AddComponent<GlassFloatHelper>().BeginFloat(glassObject);
        }

        // Now it’s safe to disable marker
        gameObject.SetActive(false);

        if (startEdgeTextArrow)
            startEdgeTextArrow.SetActive(false);
    }
}