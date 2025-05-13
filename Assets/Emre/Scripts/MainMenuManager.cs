using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class MainMenuManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject exitButton;
    public GameObject settingsPanel;

    public CameraMover camMover;
    public Transform pathPoint1;
    public Transform camPointSettings;
    public Transform camPointExit;
    public Transform camPointTabela;

    private bool inSettings = false;
    void Start()
    {
        // Oyun başlar başlamaz Tabela'ya geçiş yap
        Transform[] toTabela = new Transform[]
        {
            pathPoint1,
            camPointTabela
        };
        camMover.MoveAlongPath(toTabela);
    }

    public void StartGame()
    {
        HideAllButtons();
        SceneManager.LoadScene(1);
        Debug.Log("Game started");
    }

    public void OpenSettings()
    {
        HideAllButtons();
        inSettings = true;

        Transform[] toSettings = new Transform[]
        {
            pathPoint1,
            camPointSettings
        };
        camMover.MoveAlongPath(toSettings);

        // Ayarlar paneli kamera geçtikten sonra açılsın
        StartCoroutine(EnableSettingsPanelAfterDelay(4f)); // süreyi geçiş sürene göre ayarla
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        HideAllButtons();

        Transform[] toExit = new Transform[]
        {
            pathPoint1,
            camPointExit
        };
        camMover.MoveAlongPath(toExit);

        StartCoroutine(QuitAfterDelay(7f)); // yukarı çıkma süresi kadar
    }

    private IEnumerator EnableSettingsPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        settingsPanel.SetActive(true);
    }

    private IEnumerator QuitAfterDelay(float delay)
    {
        Debug.Log("Oyun kapatılıyor...");
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }

    private void HideAllButtons()
    {
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        exitButton.SetActive(false);
    }

    private void ShowMainButtons()
    {
        startButton.SetActive(true);
        settingsButton.SetActive(true);
        exitButton.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inSettings)
            {
                inSettings = false;
                settingsPanel.SetActive(false);

                Transform[] toTabela = new Transform[]
                {
                    pathPoint1,
                    camPointTabela
                };
                camMover.MoveAlongPath(toTabela);

                StartCoroutine(ShowButtonsAfterDelay(3.8f));
            }
        }
    }

    private IEnumerator ShowButtonsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowMainButtons();
    }
    public void BackFromSettings()
    {
        settingsPanel.SetActive(false);

        Transform[] toTabela = new Transform[]
        {
            pathPoint1,
            camPointTabela
        };
        camMover.MoveAlongPath(toTabela);

        StartCoroutine(ShowButtonsAfterDelay(3.8f));
    }

}
