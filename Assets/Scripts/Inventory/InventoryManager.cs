using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CanvasExample;
using System;

public class InventoryManager : MonoBehaviour
{
    #region Variables
    PlayerHandler playerHandler;
    //Main Inventory Menu
    public ItemData2[] items = new ItemData2[20];
    public Button[] invButtons;
    public static InventoryManager invMan;
    public Sprite emptySlot;
    public GameObject invScreen;
    public static bool showInv;
    public int playerMoney;
    public Text moneyDisplay;
    public Text[] itemValue;
    public int buttonIndex;

    //Description
    public GameObject[] descriptionScreen;
    public Text[] descName;
    public Text[] itemDescription;
    public Image[] descIcon;
    public Button[] discard;
    public Button[] use;
    public Text[] descIndexText;
    int descIndex;
    #endregion
    void Start()
    {
        if (invMan == null)
        {
            invMan = this;
        }
        else
        {
            Destroy(this);
        }
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        //invButtons = GetComponentsInChildren<ClickableObject>(); //gets the order items appear in the UI
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyBindsManager.inputKeys["Inventory"]))
        {
            showInv = !showInv;
            invScreen.SetActive(showInv);
            if (showInv)
            {
                //invScreen.SetActive(true);
                //cursor can be seen
                Cursor.visible = true;
                //cursor not locked
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
            }
            else
            {
                if (Chest.showChest)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    Time.timeScale = 0;
                }
                else
                {
                    //invScreen.SetActive(false);
                    //cursor can not be seen
                    Cursor.visible = false;
                    //cursor is locked3
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
                }
            }
        }
        moneyDisplay.text = "$ " + playerMoney.ToString(); //update player money 
    }
    #region equipping item functions
    public int FindArmourSlot(int item)
    {
        for (int i = 0; i < Equipping.equipping.armour.Length; i++)
        {
            if (Equipping.equipping.armour[i].itemName == "" || Equipping.equipping.armour[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void EquipArmour()
    {
        int slot;
        slot = FindArmourSlot(buttonIndex);
        if (slot >= 0)
        {
            Equipping.equipping.armour[slot] = items[buttonIndex];
            Equipping.equipping.UpdateArmourSlot(slot);

            ClearSlot(buttonIndex);
        }

        descriptionScreen[buttonIndex].SetActive(false);
    }

    public void EquipWeapon()
    {
        int slot;
        slot = FindWeaponSlot(buttonIndex);
        if (slot >= 0)
        {
            Equipping.equipping.weapon[slot] = items[buttonIndex];
            Equipping.equipping.UpdateWeaponSlot(slot);

            ClearSlot(buttonIndex);
        }

        descriptionScreen[buttonIndex].SetActive(false);
    }

    public int FindWeaponSlot(int index)
    {
        for (int i = 0; i < Equipping.equipping.weapon.Length; i++)
        {
            if (Equipping.equipping.weapon[i].itemName == "" || Equipping.equipping.weapon[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }
    #endregion

    #region shop functions
    public int FindShopSlotStack(int item) //btw int means it will return an int at the end of the function, void means nothing
    {
        for (int i = 0; i < Shop._shop.shopItems.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (items[buttonIndex].itemName == Shop._shop.shopItems[i].itemName && Shop._shop.shopItems[i].count <= 10 - items[buttonIndex].count)
            {
                return (i);
            }
        }
        return -1;
    }

    public int FindShopSlot(int item)
    {
        for (int i = 0; i < Shop._shop.shopItems.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (Shop._shop.shopItems[i].itemName == "" || Shop._shop.shopItems[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void Sell()
    {
        if (ShopInteract.showShop)
        {
            int slot;
            if (items[buttonIndex].stackable)
            {
                slot = FindShopSlotStack(buttonIndex);
                if (slot >= 0)
                {
                    Shop._shop.shopItems[slot].count += items[buttonIndex].count;
                    Shop._shop.UpdateShopSlot(slot);

                    UpdateMoneySell(buttonIndex);
                    ClearSlot(buttonIndex);
                    return;
                }
                else
                {
                    Debug.Log("no stack bye");
                }
            }
            slot = FindShopSlot(buttonIndex);
            Debug.Log(slot);
            if (slot >= 0)
            {
                Shop._shop.shopItems[slot] = items[buttonIndex];
                Shop._shop.UpdateShopSlot(slot);

                UpdateMoneySell(buttonIndex);
                ClearSlot(buttonIndex);
                return;
            }
        }
    }

    void UpdateMoneySell(int item)
    {
        playerMoney += items[item].value;
    }
    #endregion


    #region Chest functions
    public int FindChestSlotStack(int item)
    {
        Debug.Log("looking for slot");
        for (int i = 0; i < Chest._chest.chestItems.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (items[buttonIndex].itemName == Chest._chest.chestItems[i].itemName && Chest._chest.chestItems[i].count <= 10 - items[buttonIndex].count)
            {
                Debug.Log("available slot found");
                return (i);
            }
        }
        return -1;
    }

    public int FindChestSlot(int item)
    {
        for (int i = 0; i < Chest._chest.chestItems.Length; i++)
        {
            //check if the item name is empty
            if (Chest._chest.chestItems[i].itemName == "" || Chest._chest.chestItems[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void PutInChest()
    {
        if (Chest.showChest)
        {
            int slot;
            if (items[buttonIndex].stackable)
            {
                Debug.Log("stackable item contacted. Searching for available slot CHEST");
                slot = FindChestSlotStack(buttonIndex);
                Debug.Log("stack slot: " + slot);
                if (slot >= 0)
                {
                    Chest._chest.chestItems[slot].count += items[buttonIndex].count;
                    Chest._chest.UpdateChestSlot(slot);
                    Chest._chest.UpdateChestDesc(slot);

                    ClearSlot(buttonIndex);
                    return;
                }
                else
                {
                    Debug.Log("no stack bye");
                }
            }

            slot = FindChestSlot(buttonIndex);
            Debug.Log(slot);
            if (slot >= 0)
            {
                Chest._chest.chestItems[slot] = items[buttonIndex];
                Chest._chest.UpdateChestSlot(slot);
                Chest._chest.UpdateChestDesc(slot);

                ClearSlot(buttonIndex);
            }
        }
    }
    #endregion
    #region Description and Use functions
    public void UpdateDescription(int descIndex)
    {
        descIcon[descIndex].GetComponent<Image>().sprite = items[descIndex].icon; //update desc icon
        descName[descIndex].GetComponent<Text>().text = items[descIndex].itemName; //update desc name
        itemDescription[descIndex].GetComponent<Text>().text = items[descIndex].description; //update desc

        //get the index of the item in the inventory and put it on a text element
        descIndex = Array.IndexOf(items, items[descIndex]);
        descIndexText[descIndex].GetComponent<Text>().text = descIndex.ToString();

        Debug.Log("before discard update");
        //set the discard button function cuz idk how to get it to work with an on click
        //discard[descIndex].GetComponent<Button>().onClick.AddListener(ItemData2._itemData2.Drop);
        Debug.Log("after discard update");
        if (items[descIndex].itemType == "Armour")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Equip";
            //change function to armour equip
            use[descIndex].GetComponent<Button>().onClick.AddListener(EquipArmour);
        }
        if (items[descIndex].itemType == "Food")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Eat";
            //change function to eat food
            use[descIndex].GetComponent<Button>().onClick.AddListener(EatFood);
        }
        if (items[descIndex].itemType == "HealthPotion")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Drink";
            //change function to drink potion
            use[descIndex].GetComponent<Button>().onClick.AddListener(DrinkHealthPotion);
        }
        if (items[descIndex].itemType == "ManaPotion")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Drink";
            //change function to drink potion
            use[descIndex].GetComponent<Button>().onClick.AddListener(DrinkHealthPotion);
        }
        if (items[descIndex].itemType == "Weapon")
        {
            use[descIndex].GetComponentInChildren<Text>().text = "Equip";
            //change function to weapon equip
            use[descIndex].GetComponent<Button>().onClick.AddListener(EquipWeapon);
        }
    }

    public void EatFood()
    {
        Debug.Log("Get in mai bellleeeehhhh");

        //hide screen after use
        playerHandler.attributes[0].currentValue = playerHandler.attributes[0].currentValue + items[buttonIndex].itemBonus;
        ClearSlot(buttonIndex);
        HideDescription();
    }
    public void DrinkHealthPotion()
    {
        Debug.Log("Heal plz");

        playerHandler.attributes[0].currentValue = playerHandler.attributes[0].currentValue + items[buttonIndex].itemBonus;
        ClearSlot(buttonIndex);
        //hide screen after use
        HideDescription();
    }
    public void DrinkManaPotion()
    {
        Debug.Log("Mana plz");

        playerHandler.attributes[1].currentValue = playerHandler.attributes[1].currentValue + items[buttonIndex].itemBonus;
        ClearSlot(buttonIndex);
        //hide screen after use
        HideDescription();
    }

    public void ShowDescription()
    {
        Debug.Log("Before the loop showing description panel!");
        //on click...
        //get the itemName attached to the button clicked
        //if the itemName matches the descName on the desc panel
        for (int i = 0; i < invButtons.Length; i++)
        {
            if (/*EventSystem.current.currentSelectedGameObject.name*/invButtons[buttonIndex].name == descIndexText[i].GetComponent<Text>().text && !Chest.showChest && !Shop.showShop)
            {
                Debug.Log("Show description panel!!!!");
                //show that panel
                descriptionScreen[i].SetActive(true);
            }
            else //hide all others
            {
                Debug.Log("Description ain't showing booo");
                descriptionScreen[i].SetActive(false);
            }
        }
    }

    public void HideDescription()
    {
        for (int i = 0; i < invButtons.Length; i++)
        {
            if (descriptionScreen[i].activeSelf)
            {
                descriptionScreen[i].SetActive(false);
            }
        }
    }
    #endregion
    #region Slot management
    public int FindAvailableStackSlot(PickUpItem item) //without void this method will spit out an int
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (item.data.itemName == items[i].itemName && items[i].count <= 10 - item.data.count)
            {
                Debug.Log("available slot found!");
                return(i);
            }
        }
        return -1;
    }

    public int FindEmptySlot(PickUpItem item)
    {
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++)
        {
            if (InventoryManager.invMan.items[i].itemName == ""|| InventoryManager.invMan.items[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void UpdateSlot(int index)
    {
        //update item icon
        invButtons[index].GetComponent<Image>().sprite = items[index].icon;

        //update item text
        if (items[index].stackable)
        {
            invButtons[index].GetComponentInChildren<Text>().text = items[index].count + "";
        }
        else
        {
            invButtons[index].GetComponent<Image>().sprite = items[index].icon;
        }
        //update functionality
        //invButtons[index].GetComponent<ClickableObject>().leftClick = items[index].Use;
        //invButtons[index].GetComponent<ClickableObject>().rightClick = items[index].Drop;
        if (Shop.showShop)
        {
            itemValue[buttonIndex].GetComponent<Text>().text = "$ " + items[buttonIndex].value;
        }
        invButtons[buttonIndex].GetComponent<Button>().interactable = true;

        items[index].slot = index;
    }
    public void ClearSlot(int index)
    {
        //Remove item data
        items[index] = new ItemData2();

        //Remove icon
        invButtons[index].GetComponent<Image>().sprite = emptySlot;
        
        //Remove text
        invButtons[index].GetComponentInChildren<Text>().text = "";

        //Remove functionality
        //invButtons[index].GetComponent<ClickableObject>().leftClick = null;
        //invButtons[index].GetComponent<ClickableObject>().rightClick = null;
        //invButtons[index].GetComponent<ClickableObject>().middleClick = null;
    }
    #endregion

    #region Sorting
    public void ShowAll()
    {
        for (int i = 0; i < items.Length; i++)
        {
            //get icons back
            invButtons[i].GetComponent<Image>().color = Color.white;
            invButtons[i].GetComponentInChildren<Text>().color = Color.white;

            //bring back functionality
            invButtons[i].GetComponent<Button>().interactable = true;
        }
    }
    public void SortFood()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Food") //hide non-food items
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; 
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                invButtons[i].GetComponent<Button>().interactable = false;
            }
            if (items[i].itemType == "Food")//show food items
            {
                
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white;

                invButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }
    public void SortArmour()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Armour") //hide non-armour items
            {
                invButtons[i].GetComponent<Image>().color = Color.clear;
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                invButtons[i].GetComponent<Button>().interactable = false;
            }
            if (items[i].itemType == "Armour")//show armour items
            {

                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white;

                invButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }
    public void SortWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Weapon")
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icons
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                //stop functionality
                invButtons[i].GetComponent<Button>().interactable = false;
            }
            if (items[i].itemType == "Weapon")
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }
    public void SortPotion()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "HealthPotion" + "ManaPotion")
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icons
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                //stop functionality
                invButtons[i].GetComponent<Button>().interactable = false;
            }
            if (items[i].itemType == "HealthPotion" + "ManaPotion")
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }
    #endregion
    public void ButtonClick()
    {
        //this is used to get the selected button so we can grab the item info that's attached to it
        //get the button clicked
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        //check if the button name is an index number
        bool buttonNameCheck = clickedButton.name == "0" || clickedButton.name == "1" || clickedButton.name == "2" || clickedButton.name == "3" || clickedButton.name == "4" || clickedButton.name == "5" || clickedButton.name == "6" || clickedButton.name == "7" || clickedButton.name == "8" || clickedButton.name == "9" || clickedButton.name == "10" || clickedButton.name == "11" || clickedButton.name == "12" || clickedButton.name == "13" || clickedButton.name == "14" || clickedButton.name == "15" || clickedButton.name == "16" || clickedButton.name == "17" || clickedButton.name == "18" || clickedButton.name == "19";
        if (buttonNameCheck) //if yes
        {
            buttonIndex = Int32.Parse(clickedButton.name); //set the button name, aka the index, to an int to store the index
        }
    }
}
