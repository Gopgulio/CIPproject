using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    private Inventory invent;
    private Transform itemSlotContainer;
    private Image itemImage;
    private Transform inventUiTransf;
    private GameObject template;
    public bool omitInvent = false;

    private Vector3 rtransPos;
    private TextMeshProUGUI amountTxt;

    public GameObject currentItemSelection;
    private PlayerMovement player;


    void Start()
    {
        template = GameObject.Find("item_templ");
        itemSlotContainer = template.transform;
        inventUiTransf = GameObject.Find("invent_ui").transform;
        player = GameObject.Find("Prc").GetComponent<PlayerMovement>();
        omittingUI();
    }

    void Update()
    {

    }
    public void setInventory(Inventory invent)
    {
        this.invent = invent;

        invent.onItemListChanged += invent_onItemListChanged;
        refreshInventory();
    }

    private void refreshInventory()
    {
        foreach (Transform child in inventUiTransf)
        {
            if (child.name == "item_templ(Clone)")
            {
                Destroy(child.gameObject);
            }

        }

        int x = 0;
        int y = 0;
        float horizontalDist = 183f;
        float verticalDist = -114f;

        if (y < 2)
        {
            foreach (Item item in invent.getItems())
            {
                RectTransform itemSlotRectTransform = Instantiate(itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.transform.SetParent(inventUiTransf);
                itemSlotRectTransform.gameObject.SetActive(true);


                itemSlotRectTransform.gameObject.GetComponent<ItemInvUI>().setItem(item);
                itemSlotRectTransform.gameObject.GetComponent<DescriptionElement>().setText(item.getDescription());

                itemSlotRectTransform.localScale = new Vector3(184.6654f, 184.6654f, 184.6654f);
                itemSlotRectTransform.anchoredPosition = new Vector2(-353 + x * horizontalDist, 101 + y * verticalDist);
                rtransPos = itemSlotRectTransform.localPosition;
                rtransPos.z = -4288f;
                itemSlotRectTransform.localPosition = rtransPos;

                itemImage = itemSlotRectTransform.gameObject.GetComponent<Image>();
                itemImage.sprite = item.getSprite();

                amountTxt = itemSlotRectTransform.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                if (item.amount > 1)
                {
                    amountTxt.SetText(item.amount.ToString());
                }
                else
                {
                    amountTxt.SetText("");
                }


                x++;
                if (x == 3)
                {
                    x = 0;
                    y++;
                }

            }
        }

    }

    public void omittingUI()
    {
        Vector3 inventPos = inventUiTransf.position;
        if (omitInvent == false)
        {
            inventPos.y += 1000f;
            inventUiTransf.position = inventPos;
            omitInvent = true;
            Time.timeScale = 1;
        }
        else
        {
            inventPos.y -= 1000f;
            inventUiTransf.position = inventPos;
            omitInvent = false;
            Time.timeScale = 0;
        }
    }

    private void invent_onItemListChanged(object sender, System.EventArgs e)
    {
        refreshInventory();
    }

    public void dropItem()
    {
        Item selected = currentItemSelection.GetComponent<ItemInvUI>().getItem();
        Item duplicate = new Item { iType = selected.iType, amount = selected.amount };
        selected.amount = 1;
        ItemWorld.spawnItemWorld(Utils.getCurrentPlayerTransform().position, duplicate);
        removeItem(selected);
        refreshInventory();

    }

    public void useItem()
    {
        Item selected = currentItemSelection.GetComponent<ItemInvUI>().getItem();
        switch (selected.iType)
        {
            case Item.itemType.medkit:
                player.currentHealth += 40;
                removeItem(selected);
                break;
            case Item.itemType.handgun:
                break;
        }
    }

    public void setCurrentSelection(GameObject currentItemSelection)
    {
        this.currentItemSelection = currentItemSelection;
    }

    private void removeItem(Item sItem)
    {
        foreach (Item item in invent.getItems().ToArray())
        {
            if (item == sItem)
            {
                invent.removeItem(item);
            }

        }
    }
}
