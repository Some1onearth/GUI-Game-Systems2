using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CanvasExample;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    #region Variables
    //list
    public static List<Item> inv = new List<Item>();
    public static bool showInv;
    public Item selectedItem;
    //public Vector2 scr;
    public static int money;
    public Vector2 scrollPos;
    public string sortType = "All";
    public string[] typeNames = new string[9] { "All", "Armour", "Weapon", "Potion", "Food", "Ingredient", "Craftable", "Scroll", "Misc" };
    public Transform dropLocation;
    public GameObject inventoryPanel, showInventory;
    public Text inventory;
    [System.Serializable]
    public struct EquippedItems
    {
        public string slotName;
        public Transform equippedLocation;
        public GameObject equippedItem;
    };
    public EquippedItems[] equippedItemsSlot;
    /*
      IMGUIScript.scr.x
        IMGUIScript.scr.y
    */
    #endregion

    //start - setting up items
    private void Start()
    {

    }
    //update - toggle inv and add more items
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.I))
        {
            for (int i = 0; i < 34; i++)
            {
                inv.Add(ItemData.CreateItem(Random.Range(0, 803)));
            }
        }
#endif
        if (Input.GetKeyDown(KeyBindsManager.inputKeys["Inventory"]))
        {
            showInv = !showInv;
            if (showInv)
            {
                inventoryPanel.SetActive(true);
                //cursor can be seen
                Cursor.visible = true;
                //cursor not locked
                Cursor.lockState = CursorLockMode.Confined;
                //time paused
                //Time.timeScale = 0;
                //PauseMenu.pauseMenu.Paused(); // can use this method but depends on how you plan it out
            }
            else
            {
                inventoryPanel.SetActive(false);
                //cursor can not be seen
                Cursor.visible = false;
                //cursor is locked3
                Cursor.lockState = CursorLockMode.Locked;
                //time is not paused
                //Time.timeScale = 1;
                //PauseMenu.pauseMenu.UnPaused();
            }
        }
    }
    public void DisplayInventoryItems()
    {
        //change the buttons in the inventory to display the correct list element
        
    }
    public void DisplaySelectedItemInfo()
    {
        //this would connect to text and image components, displaying the data of the selected item

    }

    #region IMGUI
    /*
    //Ongui - 
    private void OnGUI()
    {
        if (showInv && !PauseMenu.isPaused)
        {
            for (int i = 0; i < typeNames.Length; i++)
            {
                if (GUI.Button(new Rect(IMGUIScript.scr.x * 1f + i * (IMGUIScript.scr.x * 1.5f), IMGUIScript.scr.y * 0f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 0.25f), typeNames[i]))
                {
                    sortType = typeNames[i];
                }
            }
            DisplayInv();
            if (selectedItem != null)
            {
                UseItem();
            }
        }
    }
    //DisplayInv
    void DisplayInv()
    {
        //if we have a Type selected (filter)
        if (!(sortType == "All" || sortType == ""))
        {
            ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);
            //the amount of this type
            int a = 0;
            //new slot position of the item
            int s = 0;
            //fine all items of type in our inv
            for (int i = 0; i < inv.Count; i++)
            {
                //if current element matches type
                if (inv[i].ItemType == type)
                {
                    //add to amount of this type
                    a++;
                }
            }
            //display our type that has been filtered if its under 34
            if (a <= 34)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].ItemType == type)
                    {
                        if (GUI.Button(new Rect(IMGUIScript.scr.x * 0.5f, 0.25f * IMGUIScript.scr.y + s * (0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.25f), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                        s++;
                    }
                }
            }
            else
            {
                //our move position of our scroll window
                scrollPos =
                //the start of our scroll view
                GUI.BeginScrollView(
                //our position and size of our window
                new Rect(0, 0.25f * IMGUIScript.scr.y, 3.75f * IMGUIScript.scr.x, 8.5f * IMGUIScript.scr.y),
                //our current position in the scroll view
                scrollPos,
                //viewable area
                new Rect(0, 0, 0, a * 0.25f * IMGUIScript.scr.y),
                //can we see our Horiztonal bar?
                false,
                //can we see our Vertical bar?
                true);
                #region loop inside scroll space
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(IMGUIScript.scr.x * 0.5f, s * (0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.25f), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                    s++;
                }
                #endregion
                GUI.EndScrollView();
            }
        }
        //All items are shown
        else
        {
            if (inv.Count <= 34)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(IMGUIScript.scr.x * 0.5f, 0.25f * IMGUIScript.scr.y + (i * 0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.25f), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
            }
            //we have more items then screen space
            else
            {
                //our move position of our scroll window
                scrollPos =
                //the start of our scroll view
                GUI.BeginScrollView(
                //our position and size of our window
                new Rect(0, 0.25f * IMGUIScript.scr.y, 3.75f * IMGUIScript.scr.x, 8.5f * IMGUIScript.scr.y),
                //our current position in the scroll view
                scrollPos,
                //viewable area
                new Rect(0, 0, 0, inv.Count * 0.25f * IMGUIScript.scr.y),
                //can we see our Horiztonal bar?
                false,
                //can we see our Vertical bar?
                true);
                #region loop inside scroll space
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(IMGUIScript.scr.x * 0.5f, i * (0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.25f), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
                #endregion
                GUI.EndScrollView();
            }
        }
    }
    //UseItem
    void UseItem()
    {
        //name
        GUI.Box(new Rect(4f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y, 3 * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), selectedItem.Name);
        //icon
        //GUI.Box(new Rect(4f * IMGUIScript.scr.x, 0.5f * IMGUIScript.scr.y, 3 * IMGUIScript.scr.x, 3 * IMGUIScript.scr.y), selectedItem.IconName);
        //description, amount, value
        GUI.Box(new Rect(4f * IMGUIScript.scr.x, 3.5f * IMGUIScript.scr.y, 3 * IMGUIScript.scr.x, 0.75f * IMGUIScript.scr.y), selectedItem.Description + "\nAmount: " + selectedItem.Amount + "\nValue: $" + selectedItem.Value);
        //switch via type
        switch (selectedItem.ItemType)
        {
            case ItemTypes.Armour:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Wear"))
                {
                    //wear the thing

                }
                break;
            case ItemTypes.Weapon:
                if (equippedItemsSlot[1].equippedItem == null || selectedItem.Name != equippedItemsSlot[1].equippedItem.name)//this is checking the weapon and if its already equipped we shall check the slot
                {
                    if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Equip"))
                    {
                        //Equip the thing
                        if (equippedItemsSlot[1].equippedItem != null)
                        {
                            Destroy(equippedItemsSlot[1].equippedItem);
                        }
                        equippedItemsSlot[1].equippedItem = Instantiate(selectedItem.MeshName, equippedItemsSlot[1].equippedLocation);
                        equippedItemsSlot[1].equippedItem.name = selectedItem.Name;
                        equippedItemsSlot[1].equippedItem.GetComponent<ItemHandler>().enabled = false;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Unequipped"))
                    {
                        Destroy(equippedItemsSlot[1].equippedItem);
                        equippedItemsSlot[1].equippedItem = null;
                    }
                }
                break;
            case ItemTypes.Potion:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Drink"))
                {
                    //Drink the thing

                }
                break;
            case ItemTypes.Money:
                break;
            case ItemTypes.Scroll:
                break;
            case ItemTypes.Food:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Eat"))
                {
                    //consume the thing

                }
                break;
            case ItemTypes.Ingredient:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Cook"))
                {
                    //cook the thing

                }
                break;
            case ItemTypes.Craftable:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Craft"))
                {
                    //craft the thing

                }
                break;
            case ItemTypes.Misc:
                break;
            default:
                break;
        }
        //discard button
        if (GUI.Button(new Rect(5.5f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Discard"))
        {
            //check if the item is equipped
            for (int i = 0; i < equippedItemsSlot.Length; i++)
            {
                if (equippedItemsSlot[i].equippedItem != null && selectedItem.Name == equippedItemsSlot[i].equippedItem.name)
                {
                    //if so destroy from scene
                    Destroy(equippedItemsSlot[i].equippedItem);
                    equippedItemsSlot[i].equippedItem = null;
                }
            }

            //spawn item at drop location
            GameObject itemToDrop = Instantiate(selectedItem.MeshName, dropLocation.position, Quaternion.identity);
            //apply gravity and make sure its named correctly
            itemToDrop.name = selectedItem.Name;
            itemToDrop.AddComponent<Rigidbody>().useGravity = true;
            //if the amount > 1 if so reduce from list
            if (selectedItem.Amount > 1)
            {
                selectedItem.Amount--;
            }
            //else remove from list
            else
            {                
                inv.Remove(selectedItem);
                selectedItem = null;
                return;                
            }
        }
    }
    */
    #endregion
}
