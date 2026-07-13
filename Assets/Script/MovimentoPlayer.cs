using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems; // Manteniamo la gestione della UI

public class PlayerMovement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [Range(1f, 20f)] public float velocitaGiocatore = 3.5f;
    [Tooltip("Più alto è il valore, più velocemente e morbidamente si girerà il PG verso la direzione del movimento")]
    public float velocitaRotazioneInCurva = 15f;

    [Header("Filtro di Collisione")]
    [Tooltip("Seleziona qui il Layer associato al tuo pavimento")]
    public LayerMask layerDelPavimento;

    [Header("Audio")]
    public AudioClip walkSound;

    private NavMeshAgent agent;
    private Camera cameraAttiva; // Modificato per supportare più telecamere
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocitaGiocatore;

        // Disattiviamo la rotazione rigida automatica dell'agent.
        // La gestiamo noi nell'Update in modo molto più fluido e morbido!
        agent.updateRotation = false;

        // Diamo una spinta all'accelerazione per evitare che il PG slitti come sul ghiaccio
        agent.acceleration = 30f;

        animator = GetComponentInChildren<Animator>();

        // Configurazione Audio del tuo vecchio script
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = walkSound;
        audioSource.loop = true;
    }

    void Update()
    {
        // Manteniamo la gestione delle animazioni
        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);

        if (agent.speed != velocitaGiocatore)
            agent.speed = velocitaGiocatore;

        // Manteniamo la gestione del suono dei passi
        if (agent.velocity.magnitude > 0.1f)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }

        // ROTAZIONE MORBIDA (Tocco di classe per la fluidità)
        // Se il personaggio sta camminando, lo ruotiamo gradualmente verso la direzione della NavMesh
        if (agent.velocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * velocitaRotazioneInCurva);
        }

        // INPUT MOUSE
        if (Input.GetMouseButtonDown(0))
        {
            // IL BLOCCO CANVAS: Posizionato in cima a tutto. Se tocchi un'interfaccia o un bottone,
            // blocca immediatamente il click impedendo al PG di correre per la stanza.
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            // LA SVOLTA: Pesca la telecamera della stanza in cui ti trovi attualmente
            cameraAttiva = Camera.main;

            if (cameraAttiva != null)
            {
                Ray ray = cameraAttiva.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Spariamo il raggio SOLO sul layer del pavimento.
                // Avendo il filtro specifico, non servono più tutti quegli "if" per bloccare i click 
                // su BookClick, PCClick, ecc., perché il mouse cercherà solo il pavimento!
                if (Physics.Raycast(ray, out hit, 1000f, layerDelPavimento))
                {
                    agent.SetDestination(hit.point);
                }
            }
            else
            {
                Debug.LogWarning("Nessuna telecamera attiva con il Tag 'MainCamera' trovata nella scena!");
            }
        }
    }
}