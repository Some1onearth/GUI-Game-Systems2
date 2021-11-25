using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script can be found in the Component Menu section under the option Soy Sauce/NPC Scripts/Linear Dialogue
[AddComponentMenu("Soy Sauce / NPC Scripts / Linear Dialogue")]

public class LinearDlg : MonoBehaviour
{
    #region Variables
    [Header("Refernces")]
    //boolean to toggle if we can see a characters dialogue box
    public bool showDlg;
    //array for text for our dialogue
    public string[] dlgText;
    //index for our current line of dialogue
    public int index;
    //name of this specific NPC
    public string charName; //can use new before string or use characterName/charName
    #endregion

    #region OnGui
    private void OnGUI()
    {
        //if our dialogue can be seen on screen
        if (showDlg)
        {
            //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
            GUI.Box(new Rect(0, IMGUIScript.scr.y * 6, Screen.width, IMGUIScript.scr.y * 3), charName + ": " + dlgText[index]);

            //if not at the end of the dialogue
            if (index < dlgText.Length - 1)
            {
                //next buttons allows us to skip forward to the next line fo dialoague
                if (GUI.Button(new Rect(IMGUIScript.scr.x * 15, IMGUIScript.scr.y * 8.5f, IMGUIScript.scr.x, IMGUIScript.scr.y * 0.5f), "Next"))
                {
                    index++;
                }
            }
            //else we are at the end
            else
            {
                if (GUI.Button(new Rect(IMGUIScript.scr.x * 15, IMGUIScript.scr.y * 8.5f, IMGUIScript.scr.x, IMGUIScript.scr.y * 0.5f), "Bye."))
                {
                    //close the dialogue box
                    showDlg = false;
                    //set index back to 0
                    index = 0;
                    //allow mouselook to be turned back on

                    //get the movement on the character and turn that back on
                    Time.timeScale = 1;
                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible
                    Cursor.visible = false;
                }
            }
        }
        #endregion
    }
}
