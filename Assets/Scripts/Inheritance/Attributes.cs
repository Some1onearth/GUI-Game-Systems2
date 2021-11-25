using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{
    #region Struct
    [Serializable]
    public struct Attribute
    {
        //the name of the attribute
        public string name;
        //the current value of the attribute eg 10
        public float currentValue;
        //the maximum value of the attribute eg 100
        public float maxValue;
        //the regen value eg heal over time or regen from spell or potion
        public float regenValue;
        //the bar that displays this amount eg health bar
        public Image displayImage;
    }
    #endregion
    #region Variables
    //all things you can kill, start with 3 attributes eg health, stamina and mana
    public Attribute[] attributes = new Attribute[3];

    #endregion
}
