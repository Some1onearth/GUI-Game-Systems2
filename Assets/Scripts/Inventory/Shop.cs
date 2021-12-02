using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class Shop : MonoBehaviour
{
    public static bool showShop;
    public ItemData2[] shopItems = new ItemData2[10];
    public Button[] shopButtons;
    public static Shop _shop;
    public GameObject shopMenu;
    public Sprite emptySlot;
    public Text[] shopPrices;
    int itemIndex;
    int buyBonus = 5;

    void Start()
    {
        if (_shop == null)
        {
            _shop = this; //only allows one of these scripts
        }
        else
        {
            //destroy it if there is more than this one
            Destroy(this);
        }
        for (int i = 0; i < shopItems.Length; i++)
        {
            UpdateShopSlot(i);
        }
    }
    public void CloseShopMenu()
    {
        Debug.Log("Shop menu off");
        Time.timeScale = 1;
        //closes shop panel
        shopMenu.SetActive(false);
        //cursor can not be seen
        Cursor.visible = false;
        //cursor is locked3
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UpdateShopSlot(int buttonIndex) //buttonIndex here is the button number we're updating
    {
        shopButtons[buttonIndex].GetComponent<Image>().sprite = shopItems[buttonIndex].icon; //get the matching icon and display

        if (shopItems[buttonIndex].stackable)
        {
            shopButtons[buttonIndex].GetComponentInChildren<Text>().text = shopItems[buttonIndex].count + ""; //get the matching name and display
        }
        else
        {
            shopButtons[buttonIndex].GetComponentInChildren<Text>().text = ""; //dont display a number if there is only 1
        }
        if (shopItems[buttonIndex].icon == null)
        {
            shopButtons[buttonIndex].GetComponent<Image>().sprite = emptySlot;
        }

        //update value slot
        shopPrices[buttonIndex].GetComponent<Text>().text = "$ " + shopItems[buttonIndex].value.ToString();

        if (shopItems[buttonIndex].itemName == null || shopItems[buttonIndex].itemName == "")
        {
            shopButtons[buttonIndex].GetComponent<Button>().interactable = false;
        }
        else
        {
            //update functionality
            shopButtons[buttonIndex].GetComponent<Button>().interactable = true;
        }

        shopItems[buttonIndex].slot = buttonIndex; //set the item slot to the button index pos
    }

    public void ClearShopSlot(int index)
    {
        //remove item data
        shopItems[index] = new ItemData2();

        //remove icon
        shopButtons[index].GetComponent<Image>().sprite = emptySlot;

        //remove text
        shopButtons[index].GetComponentInChildren<Text>().text = "";

        //remove functionality (pressing buttons should do nothing)
        shopButtons[index].GetComponent<Button>().interactable = false;

        //remove price display
        shopPrices[index].GetComponent<Text>().text = "";
    }

    public int FindInvSlotStackShop(int item) //for stackable
    {
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            //AND cost of item is not larger than the amount of money the player has
            if (shopItems[itemIndex].itemName == InventoryManager.invMan.items[i].itemName && InventoryManager.invMan.items[i].count <= 10 - shopItems[itemIndex].count && (shopItems[itemIndex].value + buyBonus) <= InventoryManager.invMan.playerMoney)
            {
                Debug.Log(shopItems[itemIndex].value! <= InventoryManager.invMan.playerMoney);
                return (i);
            }
        }
        return -1;
    }

    public int FindInvSlotShop(int item) //for non stack
    {
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            //AND the item value is not larger than the player money amount
            if ((InventoryManager.invMan.items[i].itemName == "" || InventoryManager.invMan.items[i].itemName == null) && (shopItems[itemIndex].value + buyBonus) <= InventoryManager.invMan.playerMoney)
            {
                return (i);
            }
        }
        return -1;
    }

    void OnShopClick()
    {
        #region Getting item info
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        bool buttonNameCheck = clickedButton.name == "0" || clickedButton.name == "1" || clickedButton.name == "2" || clickedButton.name == "3" || clickedButton.name == "4" || clickedButton.name == "5" || clickedButton.name == "6" || clickedButton.name == "7" || clickedButton.name == "8" || clickedButton.name == "9";
        if (buttonNameCheck)
        {
            itemIndex = Int32.Parse(clickedButton.name);
            Debug.Log("this button index is: " + itemIndex);
        }
        #endregion

        int slot;
        if (shopItems[itemIndex].stackable)
        {
            slot = FindInvSlotStackShop(itemIndex);
            Debug.Log("stack slot: " + slot);
            if (slot >= 0)
            {
                InventoryManager.invMan.items[slot].count += shopItems[itemIndex].count;
                InventoryManager.invMan.UpdateSlot(slot);
                InventoryManager.invMan.UpdateDescription(slot);

                //InventoryManager.invMan.playerMoney -= shopItems[itemIndex].value;
                UpdateMoneyBuy(itemIndex);
                ClearShopSlot(itemIndex);

                return;
            }
            else
            {
                Debug.Log("no stack bye");
            }
        }
        slot = FindInvSlotShop(itemIndex);
        Debug.Log(slot);
        if (slot >= 0)
        {
            InventoryManager.invMan.items[slot] = shopItems[itemIndex];
            InventoryManager.invMan.UpdateSlot(slot);
            InventoryManager.invMan.UpdateDescription(slot);

            //InventoryManager.invMan.playerMoney -= shopItems[itemIndex].value;
            UpdateMoneyBuy(itemIndex);
            ClearShopSlot(itemIndex);

            return;
        }
    }

    public void UpdateMoneyBuy(int item)
    {
        InventoryManager.invMan.playerMoney -= shopItems[item].value + buyBonus;
    }
}
