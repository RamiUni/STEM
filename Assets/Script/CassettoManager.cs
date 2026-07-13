using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CassettoManager : MonoBehaviour
{
    public static CassettoManager instance;

    [Header("UI")]
    public GameObject cassettoPanel;
    public Image cassettoImage;
    public Button provettaButton;
    public Button closeButton;

    [Header("Messaggio")]
    public GameObject messaggioPanel;
    public TextMeshProUGUI messaggioText;

    [Header("Audio")]
    public AudioClip raccoltaSound;
    private AudioSource audioSource;

    // Tipo della provetta nel cassetto attuale
    private string tipoProvettaAttuale;
    private CassettoClick cassettoAttuale;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        cassettoPanel.SetActive(false);
        messaggioPanel.SetActive(false);

        closeButton.onClick.AddListener(ChiudiCassetto);
        provettaButton.onClick.AddListener(RaccogliProvetta);
    }

    // Apre il cassetto con l'immagine e il tipo di provetta
    public void ApriCassetto(Sprite immagine, string tipo, CassettoClick sender)
    {
        tipoProvettaAttuale = tipo;
        cassettoAttuale = sender;
        cassettoImage.sprite = immagine;

        // Mostra o nasconde il bottone provetta in base a se è già stata raccolta
        provettaButton.gameObject.SetActive(!sender.raccolta);

        cassettoPanel.SetActive(true);
    }

    // Raccoglie la provetta dal cassetto
    public void RaccogliProvetta()
    {
        if (raccoltaSound != null)
            audioSource.PlayOneShot(raccoltaSound);

        // Marca il cassetto come raccolta
        cassettoAttuale.raccolta = true;

        // Nasconde il bottone provetta
        provettaButton.gameObject.SetActive(false);

        // Aggiorna il ProvetteManager
        ProvetteManager.instance.AggiuntaProvetta(tipoProvettaAttuale);

        // Chiude il cassetto
        ChiudiCassetto();
    }

    public void ChiudiCassetto()
    {
        cassettoPanel.SetActive(false);
    }
}