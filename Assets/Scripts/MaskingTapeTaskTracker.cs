using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MaskingTapeTaskTracker : MonoBehaviour
{
    public XRSocketInteractor[] tapeSockets;
    public GameObject[] toolArrowIndicator;

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
        if (AllSocketsFilled())
        {
            // Deactivate the arrow indicator
            if (arrowIndicator != null)
                arrowIndicator.SetActive(false);

            if(toolArrowIndicator != null)
            {
                foreach (var indicator in toolArrowIndicator)
                {
                    if (indicator != null)
                        indicator.SetActive(true);
                }
            }
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
