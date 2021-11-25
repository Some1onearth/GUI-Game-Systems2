using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CanvasExample;
//this script can be found in the Component Menu section under the option Soy Sauce/Player Scripts/ First Person Movement
[AddComponentMenu("Soy Sauce/Player Scripts/First Person Movement")]
//This script requires the component Character controller to be attached to the Game Object
[RequireComponent(typeof(CharacterController))] //"type of" when connecting an object that needs something, need to specify which type of thing it requires


public class Movement : MonoBehaviour
{
    #region Extra Study
    //Input Manager(https://docs.unity3d.com/Manual/class-InputManager.html)
    //Input(https://docs.unity3d.com/ScriptReference/Input.html)
    //CharacterController allows you to move the character kinda like Rigidbody (https://docs.unity3d.com/ScriptReference/CharacterController.html
    #endregion
    #region Variable
    [Header("Character")]
    public Vector3 moveDir;
    //Vector3 called moveDir, we will use this to apply movement in worldspace
    [SerializeField]
    private CharacterController _charC;
    //Character controller called _charC (_ = private class)
    [Header("Character Speeds")]
    /*
    public float variables speed, walk = 5, run = 10, crouch = 2.5, jumpSpeed = 8, gravity = 20
    */
    public float speed; //separate them but like this to make it appear better in the inspector.
    public float walk = 5, run = 10, crouch = 2.5f, jumpSpeed = 8, gravity = 20;
    [Header("Input")]
    public Vector2 input;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //_charC is set to the Character Controller on this GameObject
        _charC = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        //IMGUIScript.inputKeys is now KeyBindsManager.inputKeys
        #region Fowrard and Backward
        if (Input.GetKey(KeyBindsManager.inputKeys["Forward"]))
        {
            input.y = 1;
        }
        else if (Input.GetKey(KeyBindsManager.inputKeys["Backward"]))
        {
            input.y = -1;
        }
        else
        {
            input.y = 0;
        }
        #endregion
        #region Speed change
        if (Input.GetKey(KeyBindsManager.inputKeys["Left"]))
        {
            input.x = -1;
        }
        else if (Input.GetKey(KeyBindsManager.inputKeys["Right"]))
        {
            input.x = 1;
        }
        else
        {
            input.x = 0;
        }
        #endregion
        #region Run and Crouch
        if (Input.GetKey(KeyBindsManager.inputKeys["Sprint"]))
        {
            speed = run;
        }
        else if (Input.GetKey(KeyBindsManager.inputKeys["Crouch"]))
        {
            speed = crouch;
        }
        else
        {
            speed = walk;
        }
        #endregion



        //if our character is grounded
        if (_charC.isGrounded)
        {
            //set moveDir to the inputs direction
            moveDir = new Vector3(input.x, 0, input.y);
            //moveDir's forward is changed from global z (forward to the Game Objects local Z (forward - allows us to move where player is facing
            moveDir = transform.TransformDirection(moveDir);
            //movieDir is multiplied by speed so we move at a decent pace
            moveDir *= speed;
            //if the input button for jump is pressed then
            if (Input.GetKey(KeyBindsManager.inputKeys["Jump"]))
            {
                // our movieDir.y is equal to our jump speed
                moveDir.y = jumpSpeed;
            }
        }
        //regardless of if we are grounded or not the players moveDir.y is always affected by gravity timesed by time.deltatime to normalize it
        moveDir.y -= gravity * Time.deltaTime;
        //we then tell the character Controller that it is moving in a direction multiplied Time.deltaTime
        _charC.Move(moveDir * Time.deltaTime);
    }
}
