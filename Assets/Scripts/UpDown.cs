using UnityEngine;

public class FloatingArrow : MonoBehaviour
{
    public float amplitude = 0.1f;  // How far up and down
    public float frequency = 1f;    // How fast it moves

    private Vector3 startPos;

    void Start()
    {
        // Store the initial position
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Calculate vertical offset using sine wave
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply offset to the starting position
        transform.localPosition = startPos + new Vector3(0, yOffset, 0);
    }
}
