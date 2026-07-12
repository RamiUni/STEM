using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems; // Manteniamo la gestione della UI

public class PlayerMovement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [Range(1f, 20f)] public float velocitaGiocatore = 3.5f;

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

        // INPUT MOUSE
        if (Input.GetMouseButtonDown(0))
        {
            // Blocca il movimento se stai cliccando su un elemento dell'interfaccia/UI
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