using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Trascinate il vostro Cubo (Player) in questa casella nell'Ispettore di Unity
    public Transform target;

    void LateUpdate()
    {
        // Controlla che sia stato effettivamente assegnato un Player
        if (target != null)
        {
            // Dice alla telecamera di ruotare per guardare sempre il punto in cui si trova il Player
            transform.LookAt(target);
        }
    }
}
