using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LavagnaManager : MonoBehaviour
{
    // Riferimento statico
    public static LavagnaManager instance;

    [Header("Pannello principale")]
    public GameObject lavagnaPanel;
    public Button closeButton;

    [Header("Schermata formula")]
    public GameObject screen1;
    public Image formulaImage;

    [Header("Immagine errore")]
    public GameObject errorImage;

    [Header("Input")]
    public TMP_InputField inputField;
    public Button submitButton;

    [Header("Audio")]
    public AudioClip successSound;   // trascina il suono di successo
    public AudioClip errorSound;     // trascina il suono di errore
    private AudioSource audioSource;

    [Header("Tooltip")]
    public TextMeshProUGUI tooltipText;

    // Risposta corretta e riferimento alla lavagna cliccata
    private string correctAnswer;
    private LavagnaClick currentLavagna;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Prende l'AudioSource dalla scena
        audioSource = FindObjectOfType<AudioSource>();

        // Nasconde tutto all'avvio
        lavagnaPanel.SetActive(false);
        errorImage.SetActive(false);
        tooltipText.gameObject.SetActive(false);

        // Collega i bottoni
        closeButton.onClick.AddListener(CloseLavagna);
        submitButton.onClick.AddListener(CheckAnswer);
    }

    void Update()
    {
        // Se l'immagine errore è attiva e il giocatore clicca, la nasconde
        if (errorImage.activeSelf && Input.GetMouseButtonDown(0))
        {
            errorImage.SetActive(false);
            inputField.text = "";
        }
    }

    // Apre la lavagna
    public void OpenLavagna(string answer, Sprite formula, bool completed, LavagnaClick sender)
    {
        // Salva riferimento e risposta
        currentLavagna = sender;
        correctAnswer = answer;

        // Imposta l'immagine della formula
        formulaImage.sprite = formula;

        // Mostra il pannello
        lavagnaPanel.SetActive(true);
        errorImage.SetActive(false);
        HideTooltip();

        // Se già completata mostra solo l'immagine formula senza input
        if (completed)
        {
            inputField.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);
        }
        else
        {
            inputField.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(true);
            inputField.text = "";
            screen1.SetActive(true);
        }
    }

    // Chiude il pannello
    public void CloseLavagna()
    {
        lavagnaPanel.SetActive(false);
    }

    // Controlla la risposta
    public void CheckAnswer()
    {
        // Prende il numero digitato dal giocatore
        string playerAnswer = inputField.text.Trim();

        if (playerAnswer == correctAnswer.Trim())
        {
            // Risposta giusta — suono successo e chiudi
            if (successSound != null)
                audioSource.PlayOneShot(successSound);

            // Marca la lavagna come completata
            if (currentLavagna != null)
                currentLavagna.SetCompleted();

            // Chiude il pannello
            CloseLavagna();
        }
        else
        {
            // Risposta sbagliata — mostra immagine errore e suono
            if (errorSound != null)
                audioSource.PlayOneShot(errorSound);

            errorImage.SetActive(true);
            inputField.text = "";
        }
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
        GameManager.instance.CompletaSfida("Matematica");
    }
    
}