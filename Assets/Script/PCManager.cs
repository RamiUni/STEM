using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCManager : MonoBehaviour
{
    // Riferimento statico per accedere da altri script
    public static PCManager instance;

    [Header("Pannello principale")]
    public GameObject pcPanel;
    public Button closeButton;

    [Header("Schermate")]
    public GameObject screen1;
    public GameObject screen2;

    [Header("Schermata errore")]
    public GameObject errorScreen;

    [Header("Input")]
    public TMP_InputField inputField;
    public Button submitButton;

    [Header("Immagini")]
    public Image puzzleImage;
    public Image successImage;

    [Header("Tooltip")]
    public TextMeshProUGUI tooltipText;

    // Variabile privata che salva la risposta corretta del PC attuale
    private string correctAnswer;

    void Awake()
    {
        // Imposta l'istanza statica
        instance = this;
    }

    void Start()
    {
        // Nasconde tutto all'avvio
        pcPanel.SetActive(false);
        tooltipText.gameObject.SetActive(false);

        // Collega il bottone chiudi principale
        closeButton.onClick.AddListener(ClosePC);

        // Collega il bottone conferma alla funzione CheckAnswer
        submitButton.onClick.AddListener(CheckAnswer);

        // Collega il bottone chiudi dentro Screen2
        Button closeSuccessBtn = screen2.GetComponentInChildren<Button>();
        closeSuccessBtn.onClick.AddListener(ClosePC);
    }

    void Update()
    {
        // Se la schermata errore è attiva e il giocatore clicca, la chiude
        if (errorScreen.activeSelf && Input.GetMouseButtonDown(0))
        {
            errorScreen.SetActive(false);
        }
    }

    // Apre il pannello del PC con risposta e immagini specifiche
    // Riferimento al PC attualmente aperto
    private PCClick currentPC;

    public void OpenPC(string answer, Sprite puzzle, Sprite success, bool completed, PCClick sender)
    {
        // Salva il riferimento al PC mittente
        currentPC = sender;

        // Imposta le immagini specifiche di questo PC
        puzzleImage.sprite = puzzle;
        successImage.sprite = success;

        pcPanel.SetActive(true);
        HideTooltip();

        // Se questo PC specifico è già completato mostra successo
        if (completed)
        {
            screen1.SetActive(false);
            screen2.SetActive(true);
            errorScreen.SetActive(false);
        }
        else
        {
            correctAnswer = answer;
            screen1.SetActive(true);
            screen2.SetActive(false);
            errorScreen.SetActive(false);
            inputField.text = "";
        }
    }

    public void CheckAnswer()
    {
        string playerAnswer = inputField.text.Trim().ToLower();

        if (playerAnswer == correctAnswer.Trim().ToLower())
        {
            screen1.SetActive(false);
            screen2.SetActive(true);

            // Marca come completato il PC specifico che è aperto
            if (currentPC != null)
                currentPC.SetCompleted();
        }
        else
        {
            errorScreen.SetActive(true);
            inputField.text = "";
        }
    }

    // Chiude il pannello del PC
    public void ClosePC()
    {
        pcPanel.SetActive(false);
    }

    // Mostra tooltip
    public void ShowTooltip()
    {
        if (tooltipText == null) return;
        tooltipText.gameObject.SetActive(true);
        tooltipText.text = "Interagisci";
    }

    // Nasconde tooltip
    public void HideTooltip()
    {
        if (tooltipText == null) return;
        tooltipText.gameObject.SetActive(false);
    }

    private void ChiudiLivello()
    {
        Debug.Log("Livello ingranaggi completato!");
        GameManager.instance.CompletaSfida("Informatica");
    }
    
}