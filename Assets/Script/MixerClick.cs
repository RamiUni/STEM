using UnityEngine;

public class MixerClick : MonoBehaviour
{
    void OnMouseDown()
    {
        ProvetteManager.instance.InteragisciConMixer();
    }
}