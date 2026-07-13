using UnityEngine;

public class BookData : MonoBehaviour
{
    [TextArea(3, 10)]
    public string bookText;

    [Header("Immagini di questo libro")]
    public Sprite bookImage;
}
