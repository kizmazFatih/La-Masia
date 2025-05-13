using UnityEngine;

public class RecipeUIController : MonoBehaviour
{
    [SerializeField] private GameObject selectedRecipeImage;
    [SerializeField] private RecipeBookInteractable bookScript;
    void Update()
    {
        if (selectedRecipeImage.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            selectedRecipeImage.SetActive(false);
            bookScript.ForceCloseBookState();
        }
    }
}