using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Oyun Kapatıldı!");
        
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Editor içinde oyunu durdur
#else
        Application.Quit(); // Build alındığında çalışır
#endif
    }
}