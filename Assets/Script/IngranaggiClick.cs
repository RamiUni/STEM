using UnityEngine;
using System.Collections;

public class IngranaggiClick : MonoBehaviour
{
    private bool raccolta = false;

    void OnMouseDown()
    {
        if (raccolta) return;
        raccolta = true;
        StartCoroutine(MuoviEraccogli());
    }

    private IEnumerator MuoviEraccogli()
    {
        UnityEngine.AI.NavMeshAgent agent = GameObject.FindWithTag("Player")
            .GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.SetDestination(transform.position);

        while (agent.remainingDistance > 1.5f || agent.pathPending)
            yield return null;

        agent.SetDestination(agent.transform.position);

        // Passa il contenitore stesso al manager
        IngranaggiManager.instance.RaccogliIngranaggio(gameObject);
    }

    void OnMouseEnter()
    {
        // tooltip opzionale
    }

    void OnMouseExit()
    {
        // tooltip opzionale
    }
}