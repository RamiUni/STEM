using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Se esiste già un MusicManager non crearne un altro
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        // Sopravvive al cambio scena
        DontDestroyOnLoad(gameObject);
    }
}