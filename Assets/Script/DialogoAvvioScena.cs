using UnityEngine;
using System.Collections.Generic;

public class DialogoAvvioScena : MonoBehaviour
{
    [Header("Immagini della Intro")]
    [Tooltip("Inserisci qui la sequenza di immagini che deve apparire non appena si avvia la scena.")]
    public List<Sprite> immaginiIntro;

    [Header("Salvataggio")]
    [Tooltip("Chiave per ricordare se il dialogo è già stato visto.")]
    [SerializeField] private string chiaveSalvataggio = "IntroScenaVisualizzata";

    void Start()
    {
        // Controlla se il dialogo è già stato visto in questa scena
        if (PlayerPrefs.GetInt(chiaveSalvataggio, 0) == 0)
        {
            // Controlliamo che il DialogueManager sia presente nella scena
            if (DialogueManager.Instance != null && immaginiIntro != null && immaginiIntro.Count > 0)
            {
                // Avvia il dialogo immediatamente
                DialogueManager.Instance.AvviaDialogo(immaginiIntro);

                // Registra il fatto che è stato visto
                PlayerPrefs.SetInt(chiaveSalvataggio, 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            Debug.Log("Questa intro è già stata riprodotta. Il giocatore può giocare subito.");
        }
    }

    // Clicca con il tasto destro sul componente nell'Inspector per resettare durante i test
    [ContextMenu("Resetta Stato Intro Scena")]
    public void ResettaSalvataggio()
    {
        PlayerPrefs.DeleteKey(chiaveSalvataggio);
        Debug.Log("Salvataggio resettato! Al prossimo Play il dialogo ripartirà.");
    }
}