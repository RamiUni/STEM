using UnityEngine;
using System.Collections;

public class MixerClick : MonoBehaviour
{
    private bool usato = false;

    void OnMouseDown()
    {
        Debug.Log("Cliccato mixer");
        if (usato) return;
        usato = true;
        StartCoroutine(MuoviEUsa());
    }

    private IEnumerator MuoviEUsa()
    {
        UnityEngine.AI.NavMeshAgent agent = GameObject.FindWithTag("Player")
            .GetComponent<UnityEngine.AI.NavMeshAgent>();

        Vector3 posizioneMixer = transform.position;
        posizioneMixer.y = 0;

        UnityEngine.AI.NavMeshHit navHit;
        Vector3 destinazione = posizioneMixer;

        if (UnityEngine.AI.NavMesh.SamplePosition(posizioneMixer, out navHit, 10f, UnityEngine.AI.NavMesh.AllAreas))
            destinazione = navHit.position;

        agent.SetDestination(destinazione);

        float timeout = 10f;
        while (agent.remainingDistance > 3f || agent.pathPending)
        {
            timeout -= Time.deltaTime;
            if (timeout <= 0) break;
            yield return null;
        }

        agent.SetDestination(agent.transform.position);
        ProvetteManager.instance.InteragisciConMixer();
    }
}