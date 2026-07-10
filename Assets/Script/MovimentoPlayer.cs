using UnityEngine;
// Fondamentale per usare il NavMeshAgent
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    // Questa variabile apparirà nell'Ispettore di Unity. 
    // [Range] crea uno slider comodo da muovere con il mouse da 1 a 20.
    [Header("Impostazioni Movimento")]
    [Range(1f, 20f)] public float velocitaGiocatore = 3.5f;

    private NavMeshAgent agent;
    private Camera mainCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        // Applichiamo la velocità iniziale impostata nello script al NavMeshAgent
        agent.speed = velocitaGiocatore;
    }

    void Update()
    {
        // Questo controllo serve per aggiornare la velocità in tempo reale 
        // se la sposti nell'Ispettore mentre il gioco è in esecuzione (Playmode)
        if (agent.speed != velocitaGiocatore)
        {
            agent.speed = velocitaGiocatore;
        }

        // Logica del click del mouse (identica a prima)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}