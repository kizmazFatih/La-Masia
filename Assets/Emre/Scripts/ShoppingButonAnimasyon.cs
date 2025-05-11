using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ShoppingButonAnimasyon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Animator itemAnimator;   // Ürün üzerindeki animator bileşeni
    public AudioSource hoverSound;  // Üzerine gelindiğinde çalacak ses (opsiyonel)

    // Fare üzerine gelince animasyonu oynatır
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemAnimator.SetBool("isHovering", true);
        if (hoverSound != null)
            hoverSound.Play();
    }

    // Fare ayrılınca animasyonu durdurur
    public void OnPointerExit(PointerEventData eventData)
    {
        itemAnimator.SetBool("isHovering", false);
        if (hoverSound != null)
            hoverSound.Stop();
    }

    // Ürüne tıklanınca gerçekleşecek işlemler (opsiyonel)
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
    
}