using UnityEngine;

public class PCClick : MonoBehaviour
{
    // Quando il giocatore clicca sul PC
    void OnMouseDown()
    {
        // Apre il pannello del PC tramite il manager
        PCManager.instance.OpenPC();
    }

    // Quando il cursore entra sopra il PC
    void OnMouseEnter()
    {
        // Mostra il tooltip "Interagisci"
        PCManager.instance.ShowTooltip();
    }

    // Quando il cursore esce dal PC
    void OnMouseExit()
    {
        // Nasconde il tooltip
        PCManager.instance.HideTooltip();
    }
}