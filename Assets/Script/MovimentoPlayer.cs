using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [Range(1f, 20f)] public float velocitaGiocatore = 5f;

    [Header("Filtri di Collisione")]
    public LayerMask layerDelPavimento;
    public LayerMask layerPorte; // NUOVO: Crea e seleziona un Layer chiamato "Porte"

    private NavMeshAgent agent;
    private Camera mainCamera;

    // Variabile di supporto per gestire l'interazione con la porta
    private PortaInterattiva portaTarget;
    private bool inViaggioVersoPorta = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        if (agent != null) agent.speed = velocitaGiocatore;
    }

    void Update()
    {
        if (agent != null && agent.speed != velocitaGiocatore) agent.speed = velocitaGiocatore;

        // CONTROLLO DI DISTANZA: Se stiamo andando verso una porta, controlliamo se siamo arrivati davanti
        if (inViaggioVersoPorta && portaTarget != null)
        {
            // Calcoliamo la distanza rimanente sulla NavMesh
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.2f)
            {
                Teletrasporta(portaTarget.puntoDiAtterraggio.position);
                ResetObiettivoPorta();
            }
        }

        // INPUT MOUSE
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 1. TEST: Abbiamo cliccato una PORTA?
            if (Physics.Raycast(ray, out hit, 1000f, layerPorte))
            {
                PortaInterattiva porta = hit.collider.GetComponent<PortaInterattiva>();
                if (porta != null)
                {
                    portaTarget = porta;
                    inViaggioVersoPorta = true;
                    agent.SetDestination(portaTarget.PosizioneInterazione);
                    return; // Interrompiamo l'Update qui così non esegue il click sul pavimento
                }
            }

            // 2. TEST: Se non abbiamo cliccato una porta, controlliamo il pavimento normalmente
            if (Physics.Raycast(ray, out hit, 1000f, layerDelPavimento))
            {
                ResetObiettivoPorta();
                agent.SetDestination(hit.point);
            }
        }
    }

    void Teletrasporta(Vector3 nuovaPosizione)
    {
        // Per teletrasportare un NavMeshAgent senza bug, bisogna disattivarlo un istante,
        // cambiare posizione e riattivarlo.
        agent.enabled = false;
        transform.position = nuovaPosizione;
        agent.enabled = true;
    }

    void ResetObiettivoPorta()
    {
        portaTarget = null;
        inViaggioVersoPorta = false;
    }
}