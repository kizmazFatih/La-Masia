using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Animator coffeeAnimator; // Kahve dolma animatörü
    public AudioSource fillSound;   // Kahve dolma sesi
    private bool isFilling = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFilling)
        {
            isFilling = true;
            coffeeAnimator.SetBool("isFilling", true); // Animasyonu başlat
            fillSound.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isFilling = false;
        coffeeAnimator.SetBool("isFilling", false); // Animasyonu sıfırla
        fillSound.Stop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //StartCoroutine(BreakButtonEffect());
    }
/*
    IEnumerator BreakButtonEffect()
    {
        // Cam kırılma efekti buraya eklenebilir.
        fillSound.Stop();
        yield return new WaitForSeconds(0.5f);
        //gameObject.SetActive(false);
    }*/
}