using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script can be found in the Component Menu section under the option Soy Sauce/Player Scripts/Mouse Look
[AddComponentMenu("Soy Sauce/Player Scripts/First Person Mouse Look")]
public class MouseLook : MonoBehaviour
{
    //What are Enums??
    /*enums are what we call state value variables
      they are comma separated lists of identifiers
      we can use them to create Types or Categories*/
    #region RotationalAxis
    //Create a public enum called RotationAxis
    public enum RotationalAxis
    {
        MouseX,
        MouseY
    }
    #endregion
    #region Variables
    [Header("Rotation")] //create a public reference to the rotational axis called axis and set a default axis = add RotationalAxis and axis, the default being RotationalAxis.MouseX;
    public RotationalAxis axis = RotationalAxis.MouseX;
    [Header("Sensitivity")] //public floats for our x and y sensitivity = reminder, Vector2 have both x and y
    public Vector2 sensitivity = new Vector2(200,200);
    [Header("Y Rotation Clamp")]
    //public floats max and min y rotation
    public Vector2 rotationRangeY = new Vector2(-60, 60);
    //float for rotation Y // we will have to invert our mouse position later to calculate our mouse look correctly
    float _rotY;

    #endregion
    void Start()
    {
        //Lock Cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        //Hide Cursor from view
        Cursor.visible = false;
        //if our game object has a rigidbody attached to it
        if (GetComponent<Rigidbody>())
        {
            //set the rigidbodys freezeRotation to true
            GetComponent<Rigidbody>().freezeRotation = true; //stops rigidbody from messing up the mouse look, due to conflict with characterController
        }

        //if our game object has a Camera attached to it
        if (GetComponent<Camera>())
        {
            //Set our rotation for a MouseY axis
            axis = RotationalAxis.MouseY;
        }

    }

    // Update is called once per frame
    void Update()
    {
        #region Mouse X
        //if we rotatiing on the X
        if(axis == RotationalAxis.MouseX)
        {
            //transform the rotation on our game objects Y by our Mouse input Mouse X times X sensitvity
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime, 0);
        }
        #endregion
        #region Mouse Y
        //else we are only rotating on the Y
        else
        {
            //our rotation Y is plus equals our mouse input for Mouse Y times Y sensitvity
            _rotY += Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;
            //the rotation Y is clamped using Mathf and we are clamping the Y rotation to the Y min and Y max
            _rotY = Mathf.Clamp(_rotY, rotationRangeY.x, rotationRangeY.y);
            //transform our local ruler angle to the next vector3 rotation -_rotY on the x axis
            transform.localEulerAngles = new Vector3(-_rotY, 0, 0);
        }
        #endregion
    }
}
