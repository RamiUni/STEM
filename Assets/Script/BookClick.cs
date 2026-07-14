using UnityEngine;

public class BookClick : MonoBehaviour
{
    private BookData bookData;

    void Start()
    {
        bookData = GetComponent<BookData>();
    }

    void OnMouseDown()
    {
        BookData data = GetComponent<BookData>();
        BookManager.instance.OpenBook(data);
    }

    void OnMouseEnter()
    {
        BookManager.instance.ShowTooltip();
    }

    void OnMouseExit()
    {
        BookManager.instance.HideTooltip();
    }
}