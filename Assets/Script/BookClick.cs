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
        BookManager.instance.OpenBook(bookData.bookText);
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