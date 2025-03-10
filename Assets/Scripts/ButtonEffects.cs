using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public Image buttonImage;
    public AudioManager audioManager;
    public Color highlightColor = Color.yellow; // Butonun parlayacağı renk
    private Color originalColor;
    //public Animator buttonAnimator;

    void Start()
    {
        originalColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //buttonAnimator.SetTrigger("Hover");
        buttonImage.color = highlightColor; // Buton rengi değişsin
        audioManager.PlayHoverSound(); // Hover sesi çalsın
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //buttonAnimator.SetTrigger("Idle");
        buttonImage.color = Color.white; // Tıklayınca tam belirgin hale gelsin
        audioManager.PlayClickSound(); // Click sesi çalsın
    }
}