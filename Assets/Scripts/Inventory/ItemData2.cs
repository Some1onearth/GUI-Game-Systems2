using UnityEngine;

[System.Serializable]
public class ItemData2
{
    public static ItemData2 _itemData2;
    public string itemName;
    public bool stackable;
    public int count;
    public Sprite icon;
    public GameObject droppedObj;
    public int slot;
    public int value; //item worth
    public string itemType; //for sorting
    public int itemBonus; 
    public string description;

    public void Drop()
    {
        Debug.Log("We have dropped " + itemName);
        GameObject droppedItem = GameObject.Instantiate(Resources.Load("PickupItems/" + itemName) as GameObject, PlayerPickup.dropPos ,Quaternion.identity);
        droppedItem.GetComponent<PickUpItem>().data = this;
        InventoryManager.invMan.ClearSlot(slot);
        InventoryManager.invMan.HideDescription();
    }
}
