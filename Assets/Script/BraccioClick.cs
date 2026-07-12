using UnityEngine;
using System.Collections;

public class BraccioClick : MonoBehaviour
{
    private bool riparato = false;

    void OnMouseDown()
    {
        if (riparato) return;

        StartCoroutine(MuoviERipara());
    }

    private IEnumerator MuoviERipara()
    {
        // Prende il NavMeshAgent del player
        UnityEngine.AI.NavMeshAgent agent = GameObject.FindWithTag("Player")
            .GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Manda il player verso il braccio
        agent.SetDestination(transform.position);

        // Aspetta che il player arrivi vicino
        while (agent.remainingDistance > 1.5f || agent.pathPending)
            yield return null;

        // Ferma il player
        agent.SetDestination(agent.transform.position);

        // Interagisci col braccio
        IngranaggiManager.instance.InteragisciConBraccio();

        // Se ha tutti gli ingranaggi marca come riparato
        riparato = true;
    }
}