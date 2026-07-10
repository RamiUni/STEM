using UnityEngine;

public class CameraOrbitaleSpeculare : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform player;          // Il tuo Cubo
    public Transform centroStanza;    // L'oggetto vuoto "CentroStanza"

    private float raggioFisso;        // La distanza fissa dal centro della stanza
    private float altezzaFissaY;      // L'altezza fissa della telecamera (25)

    void Start()
    {
        if (centroStanza != null)
        {
            // Memorizziamo l'altezza Y iniziale (sarà 25 se la posizioni lì nell'Inspector)
            altezzaFissaY = transform.position.y;

            // Calcoliamo la distanza fissa (raggio) tra la posizione iniziale della telecamera e il centro sulla griglia (X, Z)
            Vector2 posCamXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 posCentroXZ = new Vector2(centroStanza.position.x, centroStanza.position.z);
            raggioFisso = Vector2.Distance(posCamXZ, posCentroXZ);
        }
    }

    void LateUpdate()
    {
        if (player != null && centroStanza != null)
        {
            // 1. Calcoliamo la direzione dal centro della stanza verso il giocatore (ignorando l'altezza Y)
            Vector3 direzioneCentroAlPlayer = player.position - centroStanza.position;
            direzioneCentroAlPlayer.y = 0; // Azzariamo la Y per calcolare l'angolo solo in 2D sul pavimento

            // Se il giocatore è esattamente al centro, non possiamo calcolare una direzione, quindi usiamo un vettore di default
            if (direzioneCentroAlPlayer.sqrMagnitude < 0.001f)
            {
                direzioneCentroAlPlayer = Vector3.forward;
            }

            // 2. Normalizziamo la direzione (la rendiamo lunga 1) e la invertiamo (moltiplicando per -1) per andare sul lato opposto
            Vector3 direzioneSpeculareNomalizzata = -direzioneCentroAlPlayer.normalized;

            // 3. Calcoliamo la nuova posizione della telecamera stando sul cerchio esterno:
            // Partiamo dal centro, ci spostiamo nella direzione opposta per la lunghezza del raggio fisso, e rimettiamo l'altezza Y a 25
            Vector3 nuovaPosizioneCam = centroStanza.position + (direzioneSpeculareNomalizzata * raggioFisso);
            nuovaPosizioneCam.y = altezzaFissaY;

            // 4. Applichiamo la posizione alla telecamera
            transform.position = nuovaPosizioneCam;

            // 5. La telecamera guarda sempre il giocatore
            transform.LookAt(player);
        }
    }
}