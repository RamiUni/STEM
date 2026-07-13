using UnityEngine;
using TMPro;
using System.Collections;

public class ProvetteManager : MonoBehaviour
{
    public static ProvetteManager instance;

    [Header("UI")]
    public GameObject messaggioPanel;
    public TextMeshProUGUI messaggioText;

    [Header("Provette")]
    // Contatori separati per ogni tipo
    private int ossigenoTrovato = 0;
    private int idrogeNoTrovato = 0;
    private int ossigenoNecessario = 2;
    private int idrogenoNecessario = 1;

    [Header("Audio")]
    public AudioClip raccoltaSound;
    public AudioClip mixerSound;
    private AudioSource audioSource;

    [Header("Animazione Player")]
    public Animator playerAnimator;
    public string raccoltaAnimName;
    public string mixerAnimName;

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

    // Controlla se il giocatore ha tutte le provette
    private bool HaTutteLeprovette()
    {
        return ossigenoTrovato >= ossigenoNecessario &&
               idrogeNoTrovato >= idrogenoNecessario;
    }

    // Chiamato da ProvetteClick passando il tipo di provetta
    public void RaccogliProvetta(GameObject contenitore, string tipo)
    {
        StartCoroutine(RaccoltaCoroutine(contenitore, tipo));
    }

    private IEnumerator RaccoltaCoroutine(GameObject contenitore, string tipo)
    {
        // Animazione raccolta
        if (playerAnimator != null)
            playerAnimator.SetTrigger(raccoltaAnimName);

        // Suono raccolta
        if (raccoltaSound != null)
            audioSource.PlayOneShot(raccoltaSound);

        yield return new WaitForSeconds(1f);

        // Disattiva il collider del contenitore
        foreach (Collider col in contenitore.GetComponentsInChildren<Collider>())
            col.enabled = false;

        // Incrementa il contatore giusto e mostra messaggio
        if (tipo == "Ossigeno")
        {
            ossigenoTrovato++;
            MostraMessaggio("Hai trovato una provetta contenente Ossigeno! " +
                "(" + ossigenoTrovato + "/" + ossigenoNecessario + ")");
        }
        else if (tipo == "Idrogeno")
        {
            idrogeNoTrovato++;
            MostraMessaggio("Hai trovato una provetta contenente Idrogeno! " +
                "(" + idrogeNoTrovato + "/" + idrogenoNecessario + ")");
        }
    }

    // Chiamato quando il giocatore clicca sul mixer
    public void InteragisciConMixer()
    {
        if (!HaTutteLeprovette())
        {
            // Messaggio specifico su cosa manca
            string mancano = "";
            if (ossigenoTrovato < ossigenoNecessario)
                mancano += "Ossigeno: " + ossigenoTrovato + "/" + ossigenoNecessario + " ";
            if (idrogeNoTrovato < idrogenoNecessario)
                mancano += "Idrogeno: " + idrogeNoTrovato + "/" + idrogenoNecessario;

            MostraMessaggio("Non hai tutte le provette! Mancano: " + mancano);
        }
        else
        {
            StartCoroutine(MixerCoroutine());
        }
    }

    private IEnumerator MixerCoroutine()
    {
        // Animazione mixer del player
        if (playerAnimator != null)
            playerAnimator.SetTrigger(mixerAnimName);

        MostraMessaggio("Stai mescolando le provette...");

        // Suono mixer
        if (mixerSound != null)
            audioSource.PlayOneShot(mixerSound);

        yield return new WaitForSeconds(2f);

        MostraMessaggio("Hai creato l'acqua! H2O!");

        yield return new WaitForSeconds(3f);

        Debug.Log("Livello scienze completato!");
    }

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
}