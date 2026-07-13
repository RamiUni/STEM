using UnityEngine;

public class CassettoClick : MonoBehaviour
{
    [Header("Contenuto cassetto")]
    public Sprite immagineCassetto;  // immagine del contenuto
    public string tipoProvetta;      // "Ossigeno" o "Idrogeno"

    // Pubblico così CassettoManager può leggerlo e modificarlo
    public bool raccolta = false;

    void OnMouseDown()
    {
        CassettoManager.instance.ApriCassetto(immagineCassetto, tipoProvetta, this);
    }

    void OnMouseEnter()
    {
        // tooltip opzionale
    }
}