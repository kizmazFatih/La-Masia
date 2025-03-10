using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f); // Büyüklük oranı
    public float scaleSpeed = 10f; // Hızlı tepki vermesi için

    private void Start()
    {
        originalScale = transform.localScale; // Butonun başlangıçtaki ölçeğini kaydet
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(targetScale)); // Buton büyüsün
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale)); // Buton küçülsün
    }

    private System.Collections.IEnumerator ScaleButton(Vector3 target)
    {
        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * scaleSpeed);
            yield return null;
        }
        transform.localScale = target; // Son değeri kesinleştir
    }
}