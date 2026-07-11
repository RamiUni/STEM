using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [Range(1f, 20f)] public float velocitaGiocatore = 3.5f;

    private NavMeshAgent agent;
    private Camera mainCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        agent.speed = velocitaGiocatore;
    }

    void Update()
    {
        if (agent.speed != velocitaGiocatore)
            agent.speed = velocitaGiocatore;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click rilevato");

            // Controlla se il click è su un elemento UI visibile
            // IsPointerOverGameObject blocca solo se c'è UI attiva sotto il cursore
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Click su UI, ignoro");
                return;
            }

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = ~LayerMask.GetMask("Player");

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                Debug.Log("Raycast colpito: " + hit.collider.name);

                if (hit.collider.GetComponent<BookClick>() != null) { Debug.Log("Colpito libro"); return; }
                if (hit.collider.GetComponent<PCClick>() != null) { Debug.Log("Colpito PC"); return; }

                Debug.Log("Destinazione: " + hit.point);
                agent.SetDestination(hit.point);
            }
            else
            {
                Debug.Log("Raycast non ha colpito niente");
            }
        }
    }
}