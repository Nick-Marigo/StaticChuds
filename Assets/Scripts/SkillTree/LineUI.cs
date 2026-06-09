using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class LineUI : MonoBehaviour
{
 
    [SerializeField] RectTransform redFill;
    [SerializeField] float fillTime = 1.0f;

    RectTransform lineRect;
    Coroutine fillCoroutine;
    float targetWidth;

    void Awake()
    {
        lineRect = GetComponent<RectTransform>();
    }

    public void SetupLine(Vector2 startPosition, Vector2 endPosition)
    {
        Vector2 direction = endPosition - startPosition;
        float distance = direction.magnitude;
        targetWidth = distance;

        lineRect.pivot = new Vector2(0f, 0.5f);
        lineRect.anchoredPosition = startPosition;

        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle);

        ResetFill();
    }

    public void StartFill(Action onComplete)
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }

        fillCoroutine = StartCoroutine(FillRoutine(onComplete));
    }

    public void CancelFill()
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
            fillCoroutine = null;
        }

        ResetFill();
    }

    IEnumerator FillRoutine(Action onComplete)
    {
        float timer = 0f;

        while (timer < fillTime)
        {
            timer += Time.deltaTime;

            float percent = timer / fillTime;
            float width = Mathf.Lerp(0f, targetWidth, percent);

            redFill.sizeDelta = new Vector2(width, redFill.sizeDelta.y);

            yield return null;
        }

        redFill.sizeDelta = new Vector2(targetWidth, redFill.sizeDelta.y);
        fillCoroutine = null;

        onComplete?.Invoke();
    }

    void ResetFill()
    {
        
        redFill.sizeDelta = new Vector2(0f, redFill.sizeDelta.y);
    }

}
