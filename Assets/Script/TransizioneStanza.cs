using UnityEngine;
using UnityEngine.AI; // Necessario per gestire il NavMeshAgent del giocatore

public class TransizioneStanza : MonoBehaviour
{
    [Header("Punto di Arrivo (Teletrasporto)")]
    [Tooltip("Trascina qui l'oggetto vuoto posizionato dove il PG deve apparire nella nuova stanza.")]
    public Transform puntoDiAtterraggio;

    [Header("Gestione Telecamere")]
    [Tooltip("La telecamera da SPEGNERE (quella della stanza da cui stai uscendo).")]
    public GameObject cameraDaDisattivare;

    [Tooltip("La telecamera da ACCENDERE (quella della stanza in cui stai entrando).")]
    public GameObject cameraDaAttivare;

    // Questa funzione di Unity si attiva da sola quando qualcosa entra nel Box Collider
    private void OnTriggerEnter(Collider other)
    {
        // Controlliamo se l'oggetto che ha toccato la porta è il Giocatore
        if (other.CompareTag("Player"))
        {
            // 1. Proviamo a prendere il componente NavMeshAgent dal giocatore
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

            if (agent != null && puntoDiAtterraggio != null)
            {
                // Per teletrasportare un NavMeshAgent senza bug grafici o blocchi,
                // bisogna disattivarlo un istante, spostarlo e riattivarlo.
                agent.enabled = false;
                other.transform.position = puntoDiAtterraggio.position;
                agent.enabled = true;
            }

            // 2. Gestione del cambio inquadratura
            if (cameraDaDisattivare != null && cameraDaAttivare != null)
            {
                cameraDaDisattivare.SetActive(false); // Spegne la vecchia cam
                cameraDaAttivare.SetActive(true);    // Accende la nuova cam
            }
        }
    }
}
