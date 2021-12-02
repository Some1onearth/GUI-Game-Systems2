using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : MonoBehaviour
{
    public static bool showShop;
    public GameObject shopScreen;
    public void OpenShopMenu()
    {
        Debug.Log("Shop menu on");
        Time.timeScale = 0;
        //shows shop panel
        shopScreen.SetActive(true);
        //cursor can be seen
        Cursor.visible = true;
        //cursor not locked
        Cursor.lockState = CursorLockMode.Confined;
        InventoryManager.invMan.invScreen.SetActive(true);
    }
}
