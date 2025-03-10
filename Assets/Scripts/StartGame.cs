using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class StartGame : MonoBehaviour
{
    public void LoadGame()
    {
        StartCoroutine(LoadGameWithDelay());
    }

    private IEnumerator LoadGameWithDelay()
    {
        yield return new WaitForSeconds(2f); // 2 saniye bekle
        SceneManager.LoadScene("GameScene"); // Sahneyi yükle
    }
}