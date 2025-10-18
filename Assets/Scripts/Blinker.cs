using UnityEngine;

public class Blinker : MonoBehaviour
{
    public float blinkInterval = 0.5f;
    private Renderer objRenderer;
    private float timer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= blinkInterval)
        {
            objRenderer.enabled = !objRenderer.enabled; // Toggle visibility
            timer = 0f;
        }
    }
}
