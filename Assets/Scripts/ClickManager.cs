using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector2 mousePos2D;
    public LayerMask inter;
    private GameObject playerPos;

    public float interRadius;
    private float outlineRadius;
    private float distance;
    private RaycastHit2D hit;

    private GameObject currentInteraction;
    private GameObject mouseOverObject = null;

    public Material mat;
    private Material original;
    public Material matStenc;
    private bool glow;

    private SpriteRenderer sprender;
    private DialManager dmngr;
    private DialTrigger dtl;

    private InventoryUI invUi;
    private Inventory invent;
    private GameObject contextMenu;
    public Vector3 contextOriginalPos;

    void Start()
    {
        invent = new Inventory();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        invUi = GetComponent<InventoryUI>();
        invUi.setInventory(invent);

        contextMenu = GameObject.Find("context_menu");
        contextOriginalPos = contextMenu.transform.position;
        //ItemWorld.spawnItemWorld(new Vector3(-0.2447472f, -0.4559628f), new Item { iType = Item.itemType.smg, amount = 1 });

        dmngr = GetComponent<DialManager>();
        outlineRadius = interRadius + 1f;
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = new Vector2(mousePos.x, mousePos.y);

        distance = Vector3.Distance(playerPos.transform.position, mousePos2D);

        hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, inter);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject != mouseOverObject)
            {

                mouseOverObject = hit.collider.gameObject;
                sprender = mouseOverObject.GetComponent<SpriteRenderer>();
                original = sprender.material;
                glow = mouseOverObject.GetComponent<AdditionalProperties>().glow == true;


            }
            if (glow == true && distance <= outlineRadius)
            {
                if (original.shader.name == "Custom/StencilObj")
                {
                    sprender.material = matStenc;
                }
                else
                {
                    sprender.material = mat;
                }
            }

            pickupItem();

            dialInteraction();



        }

        if (!hit && sprender != null)
        {
            sprender.material = original;
        }

        if (currentInteraction != null && invUi.omitInvent == true)
        {
            dialContinue();
        }
        if (Input.GetMouseButtonDown(0) && invUi.omitInvent == false)
        {
            returnContext();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && currentInteraction == null)
        {
            invUi.omittingUI();
        }
    }


    private void dialInteraction()
    {

        if (Input.GetMouseButtonDown(1) && distance <= interRadius && dmngr.dialText.text == "")
        {
            currentInteraction = hit.collider.gameObject;
            dtl = currentInteraction.GetComponent<DialTrigger>();
            if (dtl != null && dtl.enabled == true)
            {
                dtl.triggerDialogue();
                Time.timeScale = 0;
            }
        }

    }

    private void dialContinue()
    {
        if (Input.GetMouseButtonDown(0) && dtl != null)
        {
            dmngr.displayNext();
            if (dmngr.dialText.text == "")
            {
                Time.timeScale = 1;
                if (dtl.oneTimeTrigger == true)
                {
                    dtl.enabled = false;
                }
                currentInteraction = null;
            }

        }
    }
    private void pickupItem()
    {
        if (Input.GetMouseButtonDown(1) && distance <= interRadius)
        {
            ItemWorld itWorld = mouseOverObject.GetComponent<ItemWorld>();
            if (itWorld != null)
            {
                invent.addItem(itWorld.getItem());
                itWorld.destroySelf();
            }
        }

    }

    private void returnContext()
    {
        contextMenu.transform.position = contextOriginalPos;
        //Debug.Log("Moved");
    }


}
