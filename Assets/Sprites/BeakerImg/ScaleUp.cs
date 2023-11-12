using System.Collections;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float scaleSpeed = 2.0f;
    public float fadeOutTime = 1.0f;
    public void playSmoke()
    {
        StartCoroutine(ScaleAndFadeOutCoroutine());
    }

    IEnumerator ScaleAndFadeOutCoroutine()
    {
        Vector3 saveScale = transform.localScale;
        Color originalColor = GetComponent<Renderer>().material.color;
        float saveAlpha = originalColor.a;
        float elapsedTime = 0f;

        yield return new WaitForSeconds(0.25f);
        while (elapsedTime < fadeOutTime)
        {
            float scaleFactor = 1.0f + scaleSpeed * Time.deltaTime;
            transform.localScale *= scaleFactor;

            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeOutTime);
            originalColor.a = alpha;
            GetComponent<Renderer>().material.color = originalColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = saveScale;
        originalColor.a = saveAlpha;
        GetComponent<Renderer>().material.color = originalColor;
        gameObject.SetActive(false);
    }
}
