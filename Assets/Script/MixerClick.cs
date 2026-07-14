using UnityEngine;

public class MixerClick : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Mixer cliccato");
        ProvetteManager.instance.InteragisciConMixer();
    }

}