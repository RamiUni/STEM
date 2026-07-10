using UnityEngine;

public class PortaInterattiva : MonoBehaviour
{
    [Header("Punto di Arrivo")]
    [Tooltip("Trascina qui un GameObject vuoto posizionato all'interno della nuova stanza, nel punto esatto in cui il PG deve apparire.")]
    public Transform puntoDiAtterraggio;

    // Questa proprietà serve per far capire al codice del Player dove si trova la porta
    public Vector3 PosizioneInterazione => transform.position;
}
