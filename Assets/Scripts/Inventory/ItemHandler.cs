using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public int itemId = 0;
    public ItemTypes itemTypes;
    public int amount = 1;
    public void OnCollection()
    {
        if (itemTypes == ItemTypes.Misc)//are me money
        {
            Inventory.money += amount;
        }
        else if(itemTypes == ItemTypes.Potion|| itemTypes == ItemTypes.Craftable || itemTypes == ItemTypes.Food || itemTypes == ItemTypes.Ingredient || itemTypes == ItemTypes.Scroll)//are we stackable
        {
            //do we have the item
            int found = 0;
            //what is the index of that item
            int addIndex = 0;
            //Search for that info
            for (int i = 0; i < Inventory.inv.Count; i++)
            {
                if (itemId == Inventory.inv[i].ID)
                {
                    found = 1;
                    addIndex = i;
                    break;
                }
            }
            //if we have the item then increase the current items Amount by the amount
            if (found == 1)
            {
                Inventory.inv[addIndex].Amount += amount;
            }
            //if we don't have that item add the item and set the Amount to equal amount
            else
            {
                Inventory.inv.Add(ItemData.CreateItem(itemId));
                Inventory.inv[Inventory.inv.Count - 1].Amount = amount;
            }
        }
        else//nah?? just add
        {
            Inventory.inv.Add(ItemData.CreateItem(itemId));
        }
        Destroy(gameObject);//once added destroy item from world
    }
}
