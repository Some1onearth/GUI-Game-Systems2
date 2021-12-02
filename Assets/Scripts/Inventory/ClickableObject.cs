using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public delegate void LeftClick();
    //public delegate void RightClick();
    //public delegate void MiddleClick();

    public LeftClick leftClick;
    //public RightClick rightClick;
    //public MiddleClick middleClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(leftClick != null)
            {
                Debug.Log("Left Click");
                leftClick();
            }
        }
        /*
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            if (middleClick != null)
            {
                Debug.Log("Middle Click");
                middleClick();
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (rightClick != null)
            {
                Debug.Log("Right Click");
                rightClick();
            }
        }
        */
    }
}
