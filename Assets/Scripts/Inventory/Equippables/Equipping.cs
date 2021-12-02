using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Equipping : MonoBehaviour
{
    
    public ItemData2[] armour = new ItemData2[2];
    public Image[] armourIcons;
    public static Equipping equipping;
    public Sprite emptyEquipSlot;

    public ItemData2[] weapon = new ItemData2[2];
    public Image[] weaponIcons;
    public Sprite emptyWeaponSlot;

    private void Start()
    {
        if (equipping == null)
        {
            equipping = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void UpdateArmourSlot(int index)
    {
        armourIcons[index].GetComponent<Image>().sprite = armour[index].icon;
    }
    public void UpdateWeaponSlot(int index)
    {
        weaponIcons[index].GetComponent<Image>().sprite = weapon[index].icon;
    }   
}
