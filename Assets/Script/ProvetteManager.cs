using UnityEngine;
using TMPro;
using System.Collections;

public class ProvetteManager : MonoBehaviour
{
    public static ProvetteManager instance;

    [Header("Messaggio")]
    public GameObject messaggioPanel;
    public TextMeshProUGUI messaggioText;

    [Header("Contatori")]
    private int ossigenoTrovato = 0;
    private int idrogenoTrovato = 0;
    private int ossigenoNecessario = 2;
    private int idrogenoNecessario = 1;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        messaggioPanel.SetActive(false);
    }

    public void AggiuntaProvetta(string tipo)
    {
        if (tipo == "Ossigeno")
        {
            ossigenoTrovato++;
            MostraMessaggio("Hai trovato una provetta di Ossigeno! (" + ossigenoTrovato + "/" + ossigenoNecessario + ")");
        }
        else if (tipo == "Idrogeno")
        {
            idrogenoTrovato++;
            MostraMessaggio("Hai trovato una provetta di Idrogeno! (" + idrogenoTrovato + "/" + idrogenoNecessario + ")");
        }
    }

    public bool HaTutteLeProvette()
    {
        return ossigenoTrovato >= ossigenoNecessario && idrogenoTrovato >= idrogenoNecessario;
    }

    public void InteragisciConMixer()
    {
        if (!HaTutteLeProvette())
        {
            string mancano = "";
            if (ossigenoTrovato < ossigenoNecessario)
                mancano += "Ossigeno: " + ossigenoTrovato + "/" + ossigenoNecessario + " ";
            if (idrogenoTrovato < idrogenoNecessario)
                mancano += "Idrogeno: " + idrogenoTrovato + "/" + idrogenoNecessario;

            MostraMessaggio("Non hai tutte le provette! Mancano: " + mancano);
        }
        else
        {
            StartCoroutine(MixerCoroutine());
        }
    }

    private IEnumerator MixerCoroutine()
    {
        MostraMessaggio("Stai mescolando le provette...");
        yield return new WaitForSeconds(2f);
        MostraMessaggio("Hai creato l'acqua! H2O!");
        yield return new WaitForSeconds(3f);
        Debug.Log("Livello scienze completato!");
    }

    public void MostraMessaggio(string testo)
    {
        StopAllCoroutines();
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