using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("Componenti UI")]
    public GameObject pannelloDialogo; // Il pannello di sfondo del dialogo
    public Image displayImmagine;      // L'oggetto UI di tipo Image che mostra le vignette

    private List<Sprite> immaginiAttuali;
    private int indiceCorrente = 0;
    private bool dialogoAttivo = false;

    private void Awake()
    {
        // Singleton per accedervi facilmente da altri script
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // Se il dialogo è attivo e l'utente clicca col tasto sinistro del mouse
        if (dialogoAttivo && Input.GetMouseButtonDown(0))
        {
            MostraProssimaImmagine();
        }
    }

    // Funzione chiamata dalla stanza per avviare la sequenza
    public void AvviaDialogo(List<Sprite> nuoveImmagini)
    {
        if (nuoveImmagini == null || nuoveImmagini.Count == 0) return;

        immaginiAttuali = nuoveImmagini;
        indiceCorrente = 0;
        dialogoAttivo = true;
        pannelloDialogo.SetActive(true);

        AggiornaInterfaccia();
    }

    private void MostraProssimaImmagine()
    {
        indiceCorrente++;

        if (indiceCorrente < immaginiAttuali.Count)
        {
            AggiornaInterfaccia();
        }
        else
        {
            TerminaDialogo();
        }
    }

    private void AggiornaInterfaccia()
    {
        displayImmagine.sprite = immaginiAttuali[indiceCorrente];
    }

    private void TerminaDialogo()
    {
        dialogoAttivo = false;
        pannelloDialogo.SetActive(false);

        // Notifica il GameManager che il dialogo è finito
        if (GameManager.instance != null)
            GameManager.instance.MostraCrediti();
    }
}