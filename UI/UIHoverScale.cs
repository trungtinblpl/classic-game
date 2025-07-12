using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class UIHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Coroutine currentCoroutine;
    [SerializeField] private float scaleFactor = 1.1f;
    [SerializeField] private float duration = 0.1f;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleTo(originalScale * scaleFactor));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleTo(originalScale));
        }
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
