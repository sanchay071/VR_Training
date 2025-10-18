using UnityEngine;

public class ActivateComputerScreen : MonoBehaviour
{
    public GameObject instructionCanvas1;    // To disable
    public GameObject instructionCanvas2;    // To enable
    public GameObject computerScreenCanvas;  // To enable

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))  // tag your finger/hand
        {
            if (instructionCanvas1) instructionCanvas1.SetActive(false);
            if (instructionCanvas2) instructionCanvas2.SetActive(true);
            if (computerScreenCanvas) computerScreenCanvas.SetActive(true);
        }
    }
}
