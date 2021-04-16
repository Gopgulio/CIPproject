using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInvUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{

    public GameObject contextualMenu;
    private Vector3 contextualMenuPos;
    private InventoryUI inventUi;

    public Item selectedItem;


    void Awake()
    {
        contextualMenu = GameObject.Find("context_menu");
        inventUi = GameObject.Find("GameManaging").GetComponent<InventoryUI>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventUi.setCurrentSelection(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventUi.setCurrentSelection(gameObject);

            RectTransform context = contextualMenu.GetComponent<RectTransform>();

            context.position = eventData.pointerCurrentRaycast.worldPosition;

            contextualMenuPos = context.localPosition;
            contextualMenuPos.z = 0;
            context.localPosition = contextualMenuPos;
            context.SetAsLastSibling();
        }


    }


    public void setItem(Item selectedItem)
    {
        this.selectedItem = selectedItem;
    }
    public Item getItem()
    {
        return selectedItem;
    }
}
