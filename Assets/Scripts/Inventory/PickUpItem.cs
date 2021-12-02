using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PickUpItem : MonoBehaviour
{
    public ItemData2 data = new ItemData2();

    public virtual void PickedUp()
    {
        int slot;
        if (data.stackable)
        {
            Debug.Log("Stackable item contacted, Searching for available slot");
            slot = InventoryManager.invMan.FindAvailableStackSlot(this);
            if (slot >= 0)
            {
                InventoryManager.invMan.items[slot].count += data.count;
                InventoryManager.invMan.UpdateSlot(slot);
                Debug.Log("An item has been added to a stack");
                Destroy(gameObject);
                return;
            }
            else
            {
                Debug.Log("No slot with enough space was found");
            }
        }
        Debug.Log("Searching for empty slot");
        slot = InventoryManager.invMan.FindEmptySlot(this);
        if (slot >= 0)
        {
            InventoryManager.invMan.items[slot] = data;
            InventoryManager.invMan.UpdateSlot(slot);
            InventoryManager.invMan.UpdateDescription(slot);
            Debug.Log("An Item has been picked up into a new slot");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No valid slot was found");
        }
    }

}
