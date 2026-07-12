using UnityEngine;
using TMPro;
using System.Collections;

public class IngranaggiManager : MonoBehaviour
{
    public static IngranaggiManager instance;

    [Header("UI")]
    public GameObject messaggioPanel;
    public TextMeshProUGUI messaggioText;

    [Header("Ingranaggi")]
    public int totalIngranaggi = 3;        // quanti ingranaggi ci sono nella stanza
    private int ingranaggiTrovati = 0;

    [Header("Audio")]
    public AudioClip raccoltaSound;        // suono raccolta ingranaggio
    public AudioClip riparazioneSound;     // suono riparazione braccio
    public AudioClip braccioSound;         // suono meccanico del braccio
    private AudioSource audioSource;

    [Header("Animazione Player")]
    public Animator playerAnimator;        // animator del personaggio
    public string raccoltaAnimName;        // nome animazione raccolta
    public string riparazioneAnimName;     // nome animazione riparazione

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        messaggioPanel.SetActive(false);
    }

    // Chiamato quando il giocatore raccoglie un ingranaggio
    public void RaccogliIngranaggio(GameObject ingranaggio)
    {
        // Avvia la coroutine per gestire raccolta con animazione
        StartCoroutine(RaccoltaCoroutine(ingranaggio));
    }

    private IEnumerator RaccoltaCoroutine(GameObject ingranaggio)
    {
        // Animazione raccolta del player
        if (playerAnimator != null)
            playerAnimator.SetTrigger(raccoltaAnimName);

        // Suono raccolta
        if (raccoltaSound != null)
            audioSource.PlayOneShot(raccoltaSound);

        // Aspetta la durata dell'animazione
        yield return new WaitForSeconds(1f);

        // Disattiva il collider così non è più cliccabile ma rimane visibile
        ingranaggio.GetComponent<Collider>().enabled = false;

        // Incrementa il contatore
        ingranaggiTrovati++;

        // Mostra messaggio
        MostraMessaggio("Ingranaggio trovato! (" + ingranaggiTrovati + "/" + totalIngranaggi + ")");
    }

    // Chiamato quando il giocatore clicca sul braccio meccanico
    public void InteragisciConBraccio()
    {
        if (ingranaggiTrovati < totalIngranaggi)
        {
            // Non ha tutti gli ingranaggi
            MostraMessaggio("Non hai tutti gli ingranaggi! Ne mancano ancora " + (totalIngranaggi - ingranaggiTrovati));
        }
        else
        {
            // Ha tutti gli ingranaggi — avvia riparazione
            StartCoroutine(RiparazioneCoroutine());
        }
    }

    private IEnumerator RiparazioneCoroutine()
    {
        // Animazione riparazione del player
        if (playerAnimator != null)
            playerAnimator.SetTrigger(riparazioneAnimName);

        // Suono riparazione
        if (riparazioneSound != null)
            audioSource.PlayOneShot(riparazioneSound);

        MostraMessaggio("Stai riparando il braccio meccanico...");

        // Aspetta la durata dell'animazione di riparazione
        yield return new WaitForSeconds(2f);

        // Suono meccanico del braccio
        if (braccioSound != null)
            audioSource.PlayOneShot(braccioSound);

        MostraMessaggio("Tutti gli ingranaggi sono stati messi al loro posto!");

        // Aspetta un po' poi chiudi il livello
        yield return new WaitForSeconds(3f);

        // Fine livello — qui puoi chiamare il tuo sistema di completamento stanza
        ChiudiLivello();
    }

    // Mostra un messaggio per qualche secondo poi lo nasconde
    public void MostraMessaggio(string testo)
    {
        StopCoroutine("NascondiMessaggio");
        messaggioText.text = testo;
        messaggioPanel.SetActive(true);
        StartCoroutine(NascondiMessaggio());
    }

    private IEnumerator NascondiMessaggio()
    {
        yield return new WaitForSeconds(3f);
        messaggioPanel.SetActive(false);
    }

    private void ChiudiLivello()
    {
        // Per ora stampa in console — poi collegherai il tuo sistema di porte
        Debug.Log("Livello ingranaggi completato!");
    }
}