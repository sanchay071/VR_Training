using UnityEngine;
using System.Collections;
using System;

public class GlassFloatHelper : MonoBehaviour
{
    public void BeginFloat(GameObject glass, Action onComplete = null)
    {
        StartCoroutine(FloatGlassSmoothly(glass, onComplete));
    }

    private IEnumerator FloatGlassSmoothly(GameObject glassObject, Action onComplete)
    {
        if (glassObject == null) yield break;

        Vector3 startPos = glassObject.transform.position;
        Vector3 endPos = startPos + (glassObject.transform.right * -1f); // Move 1m to left
        float duration = 3f; // Float over 3 seconds
        float elapsed = 0f;

        while (elapsed < duration)
        {
            glassObject.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        glassObject.transform.position = endPos;
        Debug.Log("Glass finished floating.");

        // Invoke callback if provided
        onComplete?.Invoke();

        // Optional cleanup — remove this helper after done
        Destroy(this);
    }
}
