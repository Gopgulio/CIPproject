using System;
using System.Collections.Generic;

public class Inventory
{
    public event EventHandler onItemListChanged;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        /*addItem(new Item { iType = Item.itemType.handgun, amount = 1 });
        addItem(new Item { iType = Item.itemType.smg, amount = 1 });
        addItem(new Item { iType = Item.itemType.handgun, amount = 1 });*/
    }

    public void addItem(Item item)
    {
        if (item.isStackable())
        {
            bool hasItem = false;
            foreach (Item invItem in itemList)
            {
                if (invItem.iType == item.iType)
                {
                    invItem.amount += item.amount;
                    hasItem = true;
                }
            }
            if (hasItem == false)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        onItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void removeItem(Item item)
    {
        if (item.isStackable())
        {
            Item itemInInventory = null;
            foreach (Item invItem in itemList)
            {
                if (invItem.iType == item.iType)
                {
                    invItem.amount = item.amount - 1;
                    itemInInventory = invItem;
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }

        onItemListChanged?.Invoke(this, EventArgs.Empty);
    }


    public List<Item> getItems()
    {
        return itemList;
    }

}
