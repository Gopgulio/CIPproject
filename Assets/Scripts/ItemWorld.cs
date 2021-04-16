using UnityEngine;

public class ItemWorld : MonoBehaviour
{

    public Item item;
    private SpriteRenderer spRenderer;

    void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    public static ItemWorld spawnItemWorld(Vector3 position, Item item)
    {
        Transform trans = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itWorld = trans.GetComponent<ItemWorld>();
        itWorld.setItem(item);

        return itWorld;
    }



    public void setItem(Item item)
    {
        this.item = item;
        spRenderer.sprite = item.getSprite();
    }

    public Item getItem()
    {
        return item;
    }

    public void destroySelf()
    {
        Destroy(gameObject);
    }
}
