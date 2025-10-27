using UnityEngine;
using System.Collections;

public class GlassFloatHelper : MonoBehaviour
{
    public void BeginFloat(GameObject glass)
    {
        StartCoroutine(FloatGlassSmoothly(glass));
    }

    private IEnumerator FloatGlassSmoothly(GameObject glassObject)
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

        // Optional cleanup — remove this helper after done
        Destroy(this);
    }
}
