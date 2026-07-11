using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [Range(1f, 20f)] public float velocitaGiocatore = 3.5f;

    [Header("Audio")]
    public AudioClip walkSound;

    private NavMeshAgent agent;
    private Camera mainCamera;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        agent.speed = velocitaGiocatore;
        animator = GetComponentInChildren<Animator>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = walkSound;
        audioSource.loop = true;
    }

    void Update()
    {
        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);

        if (agent.speed != velocitaGiocatore)
            agent.speed = velocitaGiocatore;

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

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = ~LayerMask.GetMask("Player");

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider.GetComponent<BookClick>() != null) return;
                if (hit.collider.GetComponent<PCClick>() != null) return;

                agent.SetDestination(hit.point);
            }
        }
    }
}