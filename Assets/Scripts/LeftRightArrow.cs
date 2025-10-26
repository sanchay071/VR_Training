using UnityEngine;

public class FloatingArrow2 : MonoBehaviour
{
    public float amplitude = 0.1f;  // How far along the local X axis
    public float frequency = 1f;    // How fast it moves

    private Vector3 startPos;

    void Start()
    {
        // Store the initial world position
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate offset using sine wave
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Move along the local X axis (taking rotation into account)
        transform.position = startPos + transform.right * offset;
    }
}
