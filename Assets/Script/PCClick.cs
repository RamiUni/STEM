using UnityEngine;

public class PCClick : MonoBehaviour
{
    [Header("Risposta di questo PC")]
    public string thisAnswer;

    [Header("Immagini di questo PC")]
    public Sprite puzzleSprite;
    public Sprite successSprite;

    // Ogni PC tiene traccia del proprio completamento
    private bool completed = false;

    void OnMouseDown()
    {
        // Passa sé stesso come riferimento al manager
        PCManager.instance.OpenPC(thisAnswer, puzzleSprite, successSprite, completed, this);
    }

    // Chiamato dal PCManager quando la risposta è corretta
    public void SetCompleted()
    {
        completed = true;
    }

    void OnMouseEnter()
    {
        PCManager.instance.ShowTooltip();
    }

    void OnMouseExit()
    {
        PCManager.instance.HideTooltip();
    }
}