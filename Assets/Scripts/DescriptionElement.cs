using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI descriptionBox;
    public string descriptionText;
    void Awake()
    {
        descriptionBox = GameObject.Find("descriptionText").GetComponent<TextMeshProUGUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBox.SetText(descriptionText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.SetText("");
    }

    public void setText(string text)
    {
        this.descriptionText = text;
    }
}
