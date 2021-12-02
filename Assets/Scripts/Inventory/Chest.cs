using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour
{
    public static Chest _chest;
    public static bool showChest;
    public ItemData2[] chestItems = new ItemData2[20];
    public Button[] chestButtons;
    public int[] itemsToSpawn;
    int itemIndex;


    //public List<Item> chestInv = new List<Item>();
    //public Item selectedChestItem; // OnGUI stuff

    public Sprite emptyCSlot;
    public GameObject[] chestDescScreen;
    public Text[] chestDescName;
    public Text[] chestItemDesc;
    public Image[] chestDescIcon;
    public Text[] descIndexText;
    public GameObject chestItem, chestMenu;

    private void Start()
    {
        /*
        itemsToSpawn = new int[Random.Range(1, 11)];
        for (int i = 0; i < itemsToSpawn.Length; i++)
        {
            itemsToSpawn[i] = Random.Range(0, 801);
            chestInv.Add(ItemData.CreateItem(itemsToSpawn[i]));
        }
        */
        if (_chest == null)
        {
            //if this is there isn't a Chest script, this is it now
            _chest = this;
        }
        else
        {
            //otherwise, destroy it
            Destroy(this);
        }
        for (int i = 0; i < chestButtons.Length; i++)
        {
            UpdateChestSlot(i);
            UpdateChestDesc(i);
        }
    }
    public void CloseChestMenu()
    {
        Debug.Log("Chest menu off");
        Time.timeScale = 1;
        chestMenu.SetActive(false);
        //cursor can not be seen
        Cursor.visible = false;
        //cursor is locked3
        Cursor.lockState = CursorLockMode.Locked;
    }
    #region Visuals
    public void UpdateChestSlot(int buttonIndex) //buttonIndex here is the button number we're updating
    {
        chestButtons[buttonIndex].GetComponent<Image>().sprite = chestItems[buttonIndex].icon; //get the matching icon and display

        if (chestItems[buttonIndex].stackable)
        {
            chestButtons[buttonIndex].GetComponentInChildren<Text>().text = chestItems[buttonIndex].count + ""; //get the matching name and display
        }
        else
        {
            chestButtons[buttonIndex].GetComponentInChildren<Text>().text = ""; //dont display a number if there is only 1
        }
        if (chestItems[buttonIndex].icon == null)
        {
            chestButtons[buttonIndex].GetComponent<Image>().sprite = emptyCSlot;
        }

        if (chestItems[buttonIndex].itemName == null || chestItems[buttonIndex].itemName == "")
        {
            chestButtons[buttonIndex].GetComponent<Button>().interactable = false;
        }
        else
        {
            //update functionality
            chestButtons[buttonIndex].GetComponent<Button>().interactable = true;
        }

        chestItems[buttonIndex].slot = buttonIndex; //set the item slot to the button index pos
    }

    public void ClearChestSlot(int index)
    {
        //remove item data
        chestItems[index] = new ItemData2();

        //remove icon
        chestButtons[index].GetComponent<Image>().sprite = emptyCSlot;

        //remove text
        chestButtons[index].GetComponentInChildren<Text>().text = "";

        //remove functionality (pressing buttons should do nothing)
        chestButtons[index].GetComponent<Button>().interactable = false;
    }

    public void UpdateChestDesc(int index)
    {
        chestDescIcon[index].GetComponent<Image>().sprite = chestItems[index].icon; //update desc icon
        chestDescName[index].GetComponent<Text>().text = chestItems[index].itemName; //update desc name
        chestItemDesc[index].GetComponent<Text>().text = chestItems[index].description; //update desc
        descIndexText[index].GetComponent<Text>().text = chestItems[index].slot.ToString();
    }

    public void ShowChestDesc()
    {
        if (!InventoryManager.showInv)
        {
            for (int i = 0; i < chestButtons.Length; i++)
            {
                if (chestButtons[itemIndex].name == descIndexText[i].GetComponent<Text>().text)
                {
                    Debug.Log("desc index " + chestButtons[itemIndex].name == chestItems[itemIndex].slot.ToString());
                    Debug.Log("showing panel...");
                    //show that panel
                    chestDescScreen[i].SetActive(true);
                }
                else //hide all others
                {
                    Debug.Log("hiding desc");
                    chestDescScreen[i].SetActive(false);
                }
            }
        }
    }

    public void HideChestDescription()
    {
        for (int i = 0; i < chestButtons.Length; i++)
        {
            if (chestDescScreen[i].activeSelf)
            {
                chestDescScreen[i].SetActive(false);
            }
        }
    }
    #endregion

    #region Putting in inventory
    public int FindInvSlotStack(int item) //for stackable
    {
        Debug.Log("looking for slot");
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (chestItems[itemIndex].itemName == InventoryManager.invMan.items[i].itemName && InventoryManager.invMan.items[i].count <= 10 - chestItems[itemIndex].count)
            {
                Debug.Log("available slot found");
                return (i);
            }
        }
        return -1;
    }

    public int FindInvSlot(int item) //for non stack
    {
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (InventoryManager.invMan.items[i].itemName == "" || InventoryManager.invMan.items[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

    public void OnChestClick()
    {
        if (InventoryManager.showInv)
        {
            int slot;
            if (chestItems[itemIndex].stackable)
            {
                Debug.Log("stackable item contacted. Searching for available slot CHEST");
                slot = FindInvSlotStack(itemIndex);
                Debug.Log("stack slot: " + slot);
                if (slot >= 0)
                {
                    InventoryManager.invMan.items[slot].count += chestItems[itemIndex].count;
                    InventoryManager.invMan.UpdateSlot(slot);
                    InventoryManager.invMan.UpdateDescription(slot);

                    ClearChestSlot(itemIndex);
                    return;
                }
                else
                {
                    Debug.Log("no stack bye");
                }
            }
            slot = FindInvSlot(itemIndex);
            Debug.Log(slot);
            if (slot >= 0)
            {
                InventoryManager.invMan.items[slot] = chestItems[itemIndex];
                InventoryManager.invMan.UpdateSlot(slot);
                InventoryManager.invMan.UpdateDescription(slot);

                ClearChestSlot(itemIndex);
            }
        }
    }
    #endregion

    public void GetButtonIndex()
    {
        #region Getting item info
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        bool buttonNameCheck = clickedButton.name == "0" || clickedButton.name == "1" || clickedButton.name == "2" || clickedButton.name == "3" || clickedButton.name == "4" || clickedButton.name == "5" || clickedButton.name == "6" || clickedButton.name == "7" || clickedButton.name == "8" || clickedButton.name == "9";

        if (buttonNameCheck)
        {
            itemIndex = Int32.Parse(clickedButton.name);
            Debug.Log("this button index is: " + itemIndex);
        }
        else
        {
            Debug.Log("doesn't match");
        }
        #endregion
    }

    #region OnGUI
    /*
    private void OnGUI()
    {
        if (showChest)
        {
            for (int i = 0; i < chestInv.Count; i++)
            {
                if (GUI.Button(new Rect(IMGUIScript.scr.x*12.75f, IMGUIScript.scr.y * 0.25f + i * (IMGUIScript.scr.y * 0.25f), IMGUIScript.scr.x * 3f, IMGUIScript.scr.y * 0.25f),chestInv[i].Name))
                {
                    selectedChestItem = chestInv[i];
                }
            }
            if (selectedChestItem == null)
            {
                return;
            }
            else
            {
                if (GUI.Button(new Rect(IMGUIScript.scr.x * 12.5f, IMGUIScript.scr.y * 6.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.25f), "Take"))
                {
                    Inventory.inv.Add(ItemData.CreateItem(selectedChestItem.ID));
                    chestInv.Remove(selectedChestItem);
                    selectedChestItem = null;
                }
            }
        }
    }
    */
    #endregion
}
