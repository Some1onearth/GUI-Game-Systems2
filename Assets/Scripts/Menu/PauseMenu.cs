using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using CanvasExample;
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu pauseMenu;
    public GameObject pauseMenuCanvas, optionsMenu;
//#if UNITY_EDITOR
//    [Serializable]
//    public struct KeySetup
//    {
//        public string keyName;
//        public string defaultKey;
//        public string tempKey;
//    }
//    [Header("Keybinds")]
//    public KeySetup[] keySetUp;
//#endif

    public static bool isPaused; //public for testing purposes, not necessarily needed to be public, static in case other parts need to use it

    void Start()
    {
//#if UNITY_EDITOR
        //if we don't have an entry in our key dictionary
        /*
        if (!IMGUIScript.inputKeys.ContainsKey("Forward"))
        {
            
        }
        if (IMGUIScript.inputKeys == null)
        {
            
        }
        These two above can work the same as below
        */
        //if (KeyBindsManager.inputKeys.Count <= 0) //Count is a dictionary's length
        //{
        //    #region Keybinds
        //    //For loop to add the keys to the Dictionary with Save or Default depending on load
        //    for (int i = 0; i < keySetUp.Length; i++)//for all the keys in our base set up array
        //    {
        //        //add key according to the saved string or default
        //        KeyBindsManager.inputKeys.Add(keySetUp[i].keyName, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keySetUp[i].keyName, keySetUp[i].defaultKey)));
        //    }
        //    #endregion
        //}
        //loop through and set up our keys according to keySetUp
        //For loop to add the keys to the Dictionary with Save or Default depending on load
        //for all the keys in our base set up array
        //add key according to the save string or default
//#endif
        UnPaused();
    }

    public void Paused() //when paused is triggered
    {
        pauseMenuCanvas.SetActive(true);
        //stop our time
        Time.timeScale = 0;
        //free our cursor
        Cursor.lockState = CursorLockMode.Confined;
        //see our cursor
        Cursor.visible = true;
    }
    public void UnPaused() //when unpaused is triggered
    {
        //unpause our game if attached to a button...doesn't matter if it's an ESC toggle
        //isPaused = false;
        pauseMenuCanvas.SetActive(false);
        //start time
        Time.timeScale = 1;
        //lock our cursor
        Cursor.lockState = CursorLockMode.Locked;
        //hide our cursor
        Cursor.visible = false;
    }
    private void Update()
    {
        //GetKeyDown    On Press
        //GetKey        While Held
        //GetKeyUp      On Release
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                pauseMenuCanvas.SetActive(true);
                Paused();
                isPaused = true;
            }
            else
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    Paused();
                    isPaused = true;
                }
                else
                {
                    if (!InventoryManager.showInv)
                    {
                        UnPaused();
                    }
                    isPaused = false;
                }
            }
        }
    }
    #region OnGUI
    //private void OnGUI()
    //{
    //    if (isPaused)
    //    {
    //        MenuLayout();
    //    }
    //}
    //void MenuLayout()
    //{
    //    GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
    //    //Background
    //    GUI.Box(new Rect(2 * IMGUIScript.scr.x, 1 * IMGUIScript.scr.y, 12 * IMGUIScript.scr.x, 6 * IMGUIScript.scr.y), "Pause Background");
    //    //Title
    //    GUI.Box(new Rect(7 * IMGUIScript.scr.x, 2 * IMGUIScript.scr.y, 2 * IMGUIScript.scr.x, 0.5f * IMGUIScript.scr.y), "Pause Menu");
    //    //Return // if GUI button on the screen is pressed
    //    if (GUI.Button(new Rect(7 * IMGUIScript.scr.x, 3 * IMGUIScript.scr.y, 2 * IMGUIScript.scr.x, 0.5f * IMGUIScript.scr.y), "Return"))
    //    {
    //        if (!Inventory.showInv)
    //        {
    //        //unpause our game
    //        UnPaused();
    //        }
    //        isPaused = false;
    //    }
    //    //MainMenu
    //    if (GUI.Button(new Rect(7 * IMGUIScript.scr.x, 4 * IMGUIScript.scr.y, 2 * IMGUIScript.scr.x, 0.5f * IMGUIScript.scr.y), "Title Screen"))
    //    {
    //        Time.timeScale = 1;
    //        isPaused = false;
    //        //change scene
    //        SceneManager.LoadScene(0);
    //    }
    //    //Exit
    //    if (GUI.Button(new Rect(7 * IMGUIScript.scr.x, 5 * IMGUIScript.scr.y, 2 * IMGUIScript.scr.x, 0.5f * IMGUIScript.scr.y), "Exit Game"))
    //    {
    //        #if UNITY_EDITOR 
    //        UnityEditor.EditorApplication.isPlaying = false;
    //        #endif
    //        Application.Quit();
    //    }
    //}
    #endregion
}
