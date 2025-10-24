using UnityEngine;

public class FixedInFrontOfCamera : MonoBehaviour
{
    public Camera playerCamera;   // Drag your Main Camera here
    public float distance = 1.5f; // How far in front of player
    public Vector3 offset = Vector3.zero; // Optional X/Y/Z offset

    void LateUpdate()
    {
        if (playerCamera == null) return;

        // Position in front of camera
        transform.position = playerCamera.transform.position + playerCamera.transform.forward * distance + offset;

        // Face the player
        transform.LookAt(playerCamera.transform);
        transform.Rotate(0, 180f, 0); // TMP canvas faces backwards by default
    }
}
