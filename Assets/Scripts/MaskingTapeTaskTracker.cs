using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MaskingTapeTaskTracker : MonoBehaviour
{
    public TaskUIManager taskUIManager;
    public XRSocketInteractor[] tapeSockets;

    private bool taskCompleted = false;

    public GameObject arrowIndicator;  // assign your arrow here

    void Start()
    {
        // Add listener for each socket
        foreach (var socket in tapeSockets)
        {
            socket.selectEntered.AddListener(OnTapePlaced);
        }
    }

    private void OnTapePlaced(SelectEnterEventArgs args)
    {
        // Check if all sockets are filled
        if (!taskCompleted && AllSocketsFilled())
        {
            taskCompleted = true;
            taskUIManager.CompleteTask();

            // Deactivate the arrow indicator
            if (arrowIndicator != null)
                arrowIndicator.SetActive(false);
        }
    }

    private bool AllSocketsFilled()
    {
        foreach (var socket in tapeSockets)
        {
            if (socket.GetOldestInteractableSelected() == null)
                return false;
        }
        return true;
    }
}
