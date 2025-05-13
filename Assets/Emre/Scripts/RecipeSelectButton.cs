using UnityEngine;
using UnityEngine.UI;

public class RecipeSelectButton : MonoBehaviour
{
    [SerializeField] private Sprite recipeSprite;          // Bu tarife ait görsel
    [SerializeField] private Image displayImageTarget;     // Sol üstte çıkacak olan yer
    [SerializeField] private Canvas bookCanvas;            // Tarif kitabı canvas’ı

    public void OnClick()
    {
        if (recipeSprite != null)
        {
            displayImageTarget.sprite = recipeSprite;
            displayImageTarget.color = Color.white; // görünür yap
            displayImageTarget.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Recipe sprite is missing!");
        }

        bookCanvas.gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        FPSController fps = FindObjectOfType<FPSController>();
        if (fps != null)
            fps.controlsEnabled = true;
    }

}