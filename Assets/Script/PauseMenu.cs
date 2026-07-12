using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel; // il tuo pannello opzioni esistente

    private bool isPaused = false;

    void Update()
    {
        // Apre/chiude il menu pausa con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        // Ferma il tempo di gioco
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        // Riprende il tempo di gioco
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        // Riporta il timeScale a 1 prima di uscire
        Time.timeScale = 1f;
        Application.Quit();
    }


    public void OpenOptions()
    {
        //Debug.Log("OpenOptions chiamato");
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseOptions()
    {
        //Debug.Log("CloseOptions chiamato");
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}