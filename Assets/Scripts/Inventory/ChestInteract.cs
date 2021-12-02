using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : MonoBehaviour
{
    public static bool showChest;
    public GameObject chestMenu;
    public void OpenChestMenu()
    {
        Debug.Log("Shop menu on");
        Time.timeScale = 0;
        chestMenu.SetActive(true);
        //cursor can be seen
        Cursor.visible = true;
        //cursor not locked
        Cursor.lockState = CursorLockMode.Confined;
    }
}
