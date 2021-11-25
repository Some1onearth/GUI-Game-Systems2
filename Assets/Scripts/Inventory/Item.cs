using UnityEngine;

public class Item
{
    #region Variables
    // Item ID - for developers and programmers
    private int _id;
    // Display name
    private string _name;
    // Display Description
    private string _description;
    // Amount - Stackability
    private int _amount;
    // Price - Value
    private int _value;
    // Display Icon
    private Sprite _icon;
    // Mesh
    private GameObject _mesh;
    // Type
    private ItemTypes _type;
    // Basic Example Stats
    private int _heal;
    private int _armour;
    private int _damage;
    #endregion
    #region Properties
    public int ID
    {
        get { return _id; }
        set { _id = value; } // you can add behaviour/other calculations in these two lines eg. discounts in a shop
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
    public Sprite IconName
    {
        get { return _icon; }
        set { _icon = value; }
    }
    public GameObject MeshName
    {

        get { return _mesh; }
        set { _mesh = value; }
    }
    public ItemTypes ItemType
    {
        get { return _type; }
        set { _type = value; }
    }
    public int Heal
    {
        get { return _heal; }
        set { _heal = value; }
    }
    public int Armour
    {
        get { return _armour; }
        set { _armour = value; }
    }
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    #endregion
}

public enum ItemTypes
{
    Armour,
    Weapon,
    Potion,
    Money,
    Scroll,
    Food,
    Ingredient,
    Craftable,
    Misc
}