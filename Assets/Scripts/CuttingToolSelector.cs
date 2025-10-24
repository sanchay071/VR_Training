using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CuttingToolSelector : MonoBehaviour
{
    [Header("Tool References")]
    public XRGrabInteractable[] allTools;       // All single-hand tools
    public XRGrabInteractable correctTool;      // The correct single-hand tool (if any)
    public XRGrabInteractable[] wrongTools;     // Wrong tools

    [Header("Two-Handed Tool References")]
    public XRGrabInteractable rightHandle;      // T-handle right
    public XRGrabInteractable leftHandle;       // T-handle left

    [Header("UI References")]
    public GameObject wrongToolPopup;           // Canvas with blur + message
    public TextMeshProUGUI popupMessageText;    // Text inside popup
    public TaskUIManager taskUIManager;

    [Header("Marker Settings")]
    public GameObject marker;                    // Glowing torus
    public float rotationSpeed = 50f;

    private bool taskCompleted = false;
    private bool rightGrabbed = false;
    private bool leftGrabbed = false;

    [Header("Wrong Tool Messages")]
    [TextArea] public string[] wrongToolMessages;  // Explanations for each wrong tool

    // Track the currently grabbed wrong tool
    private XRGrabInteractable currentGrabbedWrongTool;
    private Vector3 wrongToolStartPos;
    private Quaternion wrongToolStartRot;

    void Start()
    {
        // Add listeners to all single-hand tools
        foreach (var tool in allTools)
        {
            tool.selectEntered.AddListener(OnToolGrabbed);
        }

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

        // Hide popup and marker at start
        if (wrongToolPopup) wrongToolPopup.SetActive(false);
        if (marker) marker.SetActive(false);
    }

    void Update()
    {
        // Rotate marker if active
        if (marker && marker.activeSelf)
            marker.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    // Single-hand tool logic
    private void OnToolGrabbed(SelectEnterEventArgs args)
    {
        if (taskCompleted) return;

        XRGrabInteractable grabbedTool = args.interactableObject as XRGrabInteractable;

        // Check if correct single-hand tool
        if (grabbedTool == correctTool)
        {
            taskUIManager.CompleteTask();
            taskCompleted = true;

            if (marker) marker.SetActive(true);
        }
        else
        {
            ShowWrongToolPopup(grabbedTool);
        }
    }

    // Two-handed tool logic
    private void CheckTwoHandedGrab()
    {
        if (!taskCompleted && rightGrabbed && leftGrabbed)
        {
            taskUIManager.CompleteTask();
            taskCompleted = true;

            if (marker) marker.SetActive(true);
        }
    }

    // Wrong tool popup
    private void ShowWrongToolPopup(XRGrabInteractable tool)
    {
        if (wrongToolPopup) wrongToolPopup.SetActive(true);

        // Disable the grabbed tool temporarily
        tool.enabled = false;

        // Store its starting position/rotation for reset
        currentGrabbedWrongTool = tool;
        wrongToolStartPos = tool.transform.localPosition;
        wrongToolStartRot = tool.transform.localRotation;

        // Set custom message
        if (popupMessageText != null)
        {
            int index = System.Array.IndexOf(wrongTools, tool);
            if (index >= 0 && index < wrongToolMessages.Length)
                popupMessageText.text = wrongToolMessages[index];
            else
                popupMessageText.text = "Wrong tool!";
        }
    }

    // Call this from OK button on the popup
    public void OnWrongToolOkClicked()
    {
        if (currentGrabbedWrongTool != null)
        {
            // Reset position and rotation
            currentGrabbedWrongTool.transform.localPosition = wrongToolStartPos;
            currentGrabbedWrongTool.transform.localRotation = wrongToolStartRot;
            currentGrabbedWrongTool.enabled = true;

            currentGrabbedWrongTool = null;
        }

        if (wrongToolPopup) wrongToolPopup.SetActive(false);
    }
}