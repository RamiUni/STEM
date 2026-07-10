using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCManager : MonoBehaviour
{
    // Riferimento statico per accedere da altri script
    public static PCManager instance;

    [Header("Pannello principale")]
    public GameObject pcPanel;          // Il pannello del PC
    public Button closeButton;          // Bottone per chiudere

    [Header("Schermate")]
    public GameObject screen1;          // Prima schermata con il puzzle
    public GameObject screen2;          // Seconda schermata di successo

    [Header("Input")]
    public TMP_InputField inputField;   // Campo di testo dove il giocatore scrive
    public Button submitButton;         // Bottone per confermare la risposta

    [Header("Risposta corretta")]
    public string correctAnswer;        // Scrivi qui la risposta giusta nell'Inspector

    [Header("Tooltip")]
    public TextMeshProUGUI tooltipText; // Il tooltip "Interagisci"

    void Awake()
    {
        // Imposta l'istanza statica
        instance = this;
    }

    void Start()
    {
        // Nasconde tutto all'inizio
        pcPanel.SetActive(false);
        tooltipText.gameObject.SetActive(false);

        // Collega i bottoni alle funzioni
        closeButton.onClick.AddListener(ClosePC);
        submitButton.onClick.AddListener(CheckAnswer);
    }

    // Apre il pannello del PC
    public void OpenPC()
    {
        // Mostra il pannello
        pcPanel.SetActive(true);

        // Mostra la prima schermata e nasconde la seconda
        screen1.SetActive(true);
        screen2.SetActive(false);

        // Svuota il campo di testo
        inputField.text = "";

        // Nasconde il tooltip quando apri il PC
        HideTooltip();
    }

    // Chiude il pannello del PC
    public void ClosePC()
    {
        pcPanel.SetActive(false);
    }

    // Controlla se la risposta è giusta
    public void CheckAnswer()
    {
        // Prende il testo scritto dal giocatore, rimuove spazi e ignora maiuscole/minuscole
        string playerAnswer = inputField.text.Trim().ToLower();

        // Confronta con la risposta corretta
        if (playerAnswer == correctAnswer.Trim().ToLower())
        {
            // Risposta giusta — cambia schermata
            screen1.SetActive(false);
            screen2.SetActive(true);
        }
        else
        {
            // Risposta sbagliata — svuota il campo e magari aggiungi un feedback
            inputField.text = "";
            Debug.Log("Risposta sbagliata!");
        }
    }

    // Mostra il tooltip "Interagisci"
    public void ShowTooltip()
    {
        // Controlla che il tooltip esista prima di usarlo
        if (tooltipText == null) return;
        tooltipText.gameObject.SetActive(true);
        tooltipText.text = "Interagisci";
    }

    // Nasconde il tooltip
    public void HideTooltip()
    {
        // Controlla che il tooltip esista prima di usarlo
        if (tooltipText == null) return;
        tooltipText.gameObject.SetActive(false);
    }
}