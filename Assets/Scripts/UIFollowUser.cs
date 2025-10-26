using UnityEngine;

public class UIFollowUser : MonoBehaviour
{
    public Transform playerCamera;
    public float distance = 1.2f; // How far in front of the user
    public float heightOffset = 0.0f; // Optional height adjustment
    public bool followRotation = true;

    void LateUpdate()
    {
        if (playerCamera == null) return;

        Vector3 targetPos = playerCamera.position + playerCamera.forward * distance;
        targetPos.y += heightOffset;
        transform.position = targetPos;

        if (followRotation)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
        }
    }
}
