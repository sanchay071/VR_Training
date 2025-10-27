using UnityEngine;

public class CuttingPoint : MonoBehaviour
{
    public CuttingMarkerTrigger controller;

    private void OnTriggerEnter(Collider other)
    {
        if (controller && other.CompareTag("CuttingTool"))
        {
            controller.OnCuttingPointTriggered(GetComponent<Collider>());
        }
    }
}
