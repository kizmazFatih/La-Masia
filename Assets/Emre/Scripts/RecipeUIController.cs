using UnityEngine;

public class RecipeUIController : MonoBehaviour
{
    [SerializeField] private GameObject selectedRecipeImage;

    void Update()
    {
        if (selectedRecipeImage.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            selectedRecipeImage.SetActive(false);
        }
    }
}