using UnityEngine;

public class LavagnaClick : MonoBehaviour
{
    [Header("Risposta corretta (numero 1, 2 o 3)")]
    public string correctAnswer;

    [Header("Immagine formula")]
    public Sprite formulaSprite;

    // Stato di completamento di questa lavagna
    private bool completed = false;

    void OnMouseDown()
    {
        // Se già completata non fare niente
        if (completed) return;

        LavagnaManager.instance.OpenLavagna(
            correctAnswer,
            formulaSprite,
            completed,
            this
        );
    }

    // Chiamato dal manager quando la risposta è corretta
    public void SetCompleted()
    {
        completed = true;
    }

    void OnMouseEnter()
    {
        // Non mostrare tooltip se già completata
        if (completed) return;
        LavagnaManager.instance.ShowTooltip();
    }

    void OnMouseExit()
    {
        LavagnaManager.instance.HideTooltip();
    }
}