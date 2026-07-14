using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Sfide")]
    private bool sfidaInformaticaCompletata = false;
    private bool sfidaMatematicaCompletata = false;
    private bool sfidaIngegneriaCompletata = false;
    private bool sfidaScienzaCompletata = false;

    [Header("Dialogo Finale")]
    public List<Sprite> immaginiFinale;
    public GameObject immagineCrediti; // pannello con i titoli di coda

    void Awake()
    {
        instance = this;
    }

    // Chiamato da ogni sfida quando viene completata
    public void CompletaSfida(string nomeSfida)
    {
        switch (nomeSfida)
        {
            case "Informatica": sfidaInformaticaCompletata = true; break;
            case "Matematica": sfidaMatematicaCompletata = true; break;
            case "Ingegneria": sfidaIngegneriaCompletata = true; break;
            case "Scienza": sfidaScienzaCompletata = true; break;
        }

        // Controlla se tutte le sfide sono completate
        if (TutteLeSfideCompletate())
            AvviaFinale();
    }

    private bool TutteLeSfideCompletate()
    {
        return sfidaInformaticaCompletata &&
               sfidaMatematicaCompletata &&
               sfidaIngegneriaCompletata &&
               sfidaScienzaCompletata;
    }

    private bool finaleAvviato = false;

    private void AvviaFinale()
    {
        finaleAvviato = true;
        if (DialogueManager.Instance != null && immaginiFinale != null && immaginiFinale.Count > 0)
            DialogueManager.Instance.AvviaDialogo(immaginiFinale);
    }

    public void MostraCrediti()
    {
        // Mostra i crediti solo se il finale è stato avviato
        if (!finaleAvviato) return;
        if (immagineCrediti != null)
            immagineCrediti.SetActive(true);
    }
}