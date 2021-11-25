using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CanvasExample;
//this script can be found in the Component Menu section under the option Soy Sauce/Player Scripts/Player Interaction
[AddComponentMenu("Soy Sauce/Player Scripts/Player Interaction")]
public class Interact : MonoBehaviour
{

    void Update()
    {
        //if our interact key is pressed
        if (Input.GetKeyDown(KeyBindsManager.inputKeys["Interact"]))
        {
            #region RayCasting Info
            //RAY - A ray is an infinite line starting at origin and going in some direction.
            //RAYCASTING - Casts a ray, from point origin, in direction, of length maxDistance, against all colliders in the Scene.
            //RAYCASTHIT - Structure used to get information back from a raycast
            #endregion
            //create ray
            Ray interactRay; //this is our line...Our Ray/Line doesn't have an origin, or a direction
            //assign an origin
            interactRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //this ray is shooting from the main camera's screen point center of screen
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interactRay, out hitInfo, 10))
            {
                #region NPC
                //if the collider we hit is tagged NPC
                if (hitInfo.collider.tag == "NPC")
                {
                    //Debug that we hit an NPC
                    Debug.Log("NPC");
                    if (hitInfo.collider.gameObject.GetComponent<LinearDlg>())
                    {
                        hitInfo.collider.gameObject.GetComponent<LinearDlg>().showDlg = true;
                        //remove camera rotation
                        Cursor.lockState = CursorLockMode.Confined;
                        //remove player movement (aka stop it)
                        Time.timeScale = 0;
                        //show the cursor
                        Cursor.visible = true;
                        //unlock the cursor
                    }
                }
                #endregion
                #region Item
                //if the collider we hit is tagged Item
                if (hitInfo.collider.CompareTag("Item")) //does the same thing as .tag for now
                {
                    //Debug that we hit an Item
                    Debug.Log("Our Interact ray hit an Item");
                    ItemHandler handler = hitInfo.transform.GetComponent<ItemHandler>();
                    if (handler != null)
                    {
                        handler.OnCollection();
                    }
                }
                #endregion
                #region Chest
                //if the collider we hit is tagged Item
                if (hitInfo.collider.CompareTag("Chest")) //does the same thing as .tag for now
                {
                    //Debug that we hit an Item
                    Debug.Log("Our Interact ray hit an Chest");
                    Chest currenChest = hitInfo.transform.GetComponent<Chest>();
                    if (currenChest != null)
                    {
                        currenChest.showChest = !currenChest.showChest;
                    }
                }
                #endregion
            }
        }
    }
}
