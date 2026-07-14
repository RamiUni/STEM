using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;

    public GameObject bookPanel;        // trascina il Panel qui
    public TextMeshProUGUI bookTextField; // trascina il Text TMP qui
    public Button closeButton;          // trascina il Button qui

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bookPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseBook);
    }

    public void OpenBook(string text)
    {
        bookTextField.text = text;
        bookPanel.SetActive(true);
    }

    public void CloseBook()
    {
        bookPanel.SetActive(false);
    }

    public TextMeshProUGUI tooltipText; // trascina il TooltipText qui

    public void ShowTooltip()
    {
        tooltipText.gameObject.SetActive(true);
        tooltipText.text = "Leggi libro";
    }

    public void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false);
    }

    public Image bookImage; // trascina BookImage qui nell'Inspector

    public void OpenBook(BookData data)
    {
        bookTextField.text = data.bookText;

        // Mostra l'immagine solo se esiste
        if (data.bookImage != null)
        {
            bookImage.sprite = data.bookImage;
            bookImage.gameObject.SetActive(true);
        }
        else
        {
            bookImage.gameObject.SetActive(false);
        }

        bookPanel.SetActive(true);
    }
}