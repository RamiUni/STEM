using UnityEngine;
using System.Collections;

public class ProvetteClick : MonoBehaviour
{
    public string tipo;
    private bool raccolta = false;

    void OnMouseDown()
    {
        Debug.Log("Click su: " + gameObject.name + " raccolta=" + raccolta);
        if (raccolta) return;
        raccolta = true;
        StartCoroutine(MuoviEraccogli());
    }

    private IEnumerator MuoviEraccogli()
    {
        UnityEngine.AI.NavMeshAgent agent = GameObject.FindWithTag("Player")
            .GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Cerca punto NavMesh vicino al contenitore
        UnityEngine.AI.NavMeshHit navHit;
        Vector3 destinazione = transform.position;

        if (UnityEngine.AI.NavMesh.SamplePosition(
            transform.position, out navHit, 10f,
            UnityEngine.AI.NavMesh.AllAreas))
        {
            destinazione = navHit.position;
            Debug.Log("NavMesh trovato a: " + destinazione);
        }
        else
        {
            Debug.Log("NavMesh NON trovato!");
        }

        agent.SetDestination(destinazione);

        float timeout = 10f;
        while (agent.remainingDistance > 3f || agent.pathPending)
        {
            timeout -= Time.deltaTime;
            if (timeout <= 0) { Debug.Log("Timeout"); break; }
            yield return null;
        }

        Debug.Log("Uscito dal while — remainingDistance: " + agent.remainingDistance);
        agent.SetDestination(agent.transform.position);
        ProvetteManager.instance.RaccogliProvetta(gameObject, tipo);

        agent.SetDestination(agent.transform.position);
        ProvetteManager.instance.RaccogliProvetta(gameObject, tipo);
    }
}