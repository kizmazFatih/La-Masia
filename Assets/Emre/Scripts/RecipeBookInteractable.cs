using System.Collections;
using UnityEngine;

public class RecipeBookInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform myCanvas;
    [SerializeField] private Canvas bookCanvas;
    private bool isOpen = false;

    private void Start()
    {
        bookCanvas.gameObject.SetActive(false);
    }

    public Transform ShowMyUI()
    {
        // UI göstermiyoruz
        return myCanvas.transform.parent;
    }

    public void Interact(Transform handle)
    {
        if (!isOpen)
        {
            OpenBook();
        }
    }

    public void Release(Transform handle)
    {
        // Gerek yok
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBook();
        }
    }

    void OpenBook()
    {
        bookCanvas.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // FPS karakter hareketini durdur
        FPSController fps = FindObjectOfType<FPSController>();
        if (fps != null)
        {
            fps.controlsEnabled = false;
        }

        isOpen = true;
    }

    void CloseBook()
    {
        bookCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // FPS karakter hareketini geri aç
        FPSController fps = FindObjectOfType<FPSController>();
        if (fps != null)
        {
            fps.controlsEnabled = true;
        }

        isOpen = false;
    }
}