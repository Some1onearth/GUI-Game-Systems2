using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int itemID)
    {
        #region Variables
        int id = 0;
        string name = "";
        string description = "";
        int amount = 0;
        int value = 0;
        string icon = "";
        string mesh = "";
        ItemTypes type = ItemTypes.Misc;
        int heal = 0;
        int armour = 0;
        int damage = 0;
        #endregion
        switch (itemID)
        {
            #region Armour 0-99
            case 0:
                id = 0;
                name = "Rags";
                description = "A mouldy piece of cloth.";
                amount = 1;
                value = 1;
                icon = "Armour/Rags";
                mesh = "Armour/Rags";
                type = ItemTypes.Armour;
                armour = 1;
                break;
            case 1:
                name = "Plate Armour";
                description = "An armour made out of cerimic plates.";
                amount = 1;
                value = 1;
                icon = "Armour/PlateArmour";
                mesh = "Armour/PlateArmour";
                type = ItemTypes.Armour;
                armour = 1;
                break;
            case 2:
                name = "Armour";
                description = "An armour made out of scrap metal.";
                amount = 1;
                value = 10;
                icon = "Armour/Armour";
                mesh = "Armour/Armour";
                type = ItemTypes.Armour;
                armour = 10;
                break;

            #endregion
            #region Weapon 100-199
            case 100:
                name = "Rat Flail";
                description = "A flail made from a rat.";
                amount = 1;
                value = 1;
                icon = "Weapon/RatFlail";
                mesh = "Weapon/RatFlail";
                type = ItemTypes.Weapon;
                damage = 1;
                break;
            case 101:
                name = "Bow";
                description = "A bow you bow down to.";
                amount = 1;
                value = 7;
                icon = "Weapon/Bow";
                mesh = "Weapon/Bow";
                type = ItemTypes.Weapon;
                damage = 10;
                break;
            case 102:
                name = "Axe";
                description = "";
                amount = 1;
                value = 12;
                damage = 17;
                icon = "Weapon/Axe";
                mesh = "Weapon/Axe";
                type = ItemTypes.Weapon;
                break;
            case 103:
                name = "Sword";
                description = "";
                amount = 1;
                value = 12;
                damage = 17;
                icon = "Weapon/Sword";
                mesh = "Weapon/Sword";
                type = ItemTypes.Weapon;
                break;
            #endregion
            #region Potion 200-299
            case 200:
                name = "Mystery Liquid";
                description = "";
                amount = 1;
                value = 1;
                icon = "Potion/MysteryLiquid";
                mesh = "Potion/MysteryLiquid";
                type = ItemTypes.Potion;
                heal = 2;
                damage = 5;
                break;
            case 201:
                name = "Healing Potion";
                description = "";
                amount = 1;
                value = 10;
                icon = "Potion/hp";
                mesh = "Potion/hp";
                type = ItemTypes.Potion;
                heal = 10;
                break;
            #endregion
            #region Food 300-399
            case 300:
                name = "Diseased Bread";
                description = "A mouldy piece of bread.";
                amount = 1;
                value = 1;
                icon = "Food/DiseasedBread";
                mesh = "Food/DiseasedBread";
                type = ItemTypes.Food;
                damage = 5;
                break;
            case 301:
                name = "Apple";
                description = "";
                amount = 1;
                value = 3;
                icon = "Food/Apple";
                mesh = "Food/Apple";
                type = ItemTypes.Food;
                heal = 5;
                break;
            case 303:
                name = "Cabbage";
                description = "";
                amount = 1;
                value = 100;
                heal = 2;
                icon = "Food/Cabbage";
                mesh = "Food/Cabbage";
                type = ItemTypes.Food;
                break;
            #endregion
            #region Ingredient 400-499
            case 400:
                name = "Egg";
                description = "";
                amount = 1;
                value = 1;
                icon = "Ingredient/Egg";
                mesh = "Ingredient/Egg";
                break;
            #endregion
            #region Craftable 500-599
            case 500:
                name = "Iron";
                description = "";
                amount = 1;
                value = 5;
                icon = "Craftable/Iron";
                mesh = "Craftable/Iron";
                type = ItemTypes.Craftable;
                break;
            #endregion
            #region Money 600-699
            case 600:
                name = "Coin";
                description = "";
                amount = 1;
                value = 1;
                icon = "Craftable/Iron";
                mesh = "Craftable/Iron";
                type = ItemTypes.Money;
                break;
            #endregion
            #region Scroll 700-799
            case 700:
                name = "Mysterious Scroll";
                description = "";
                amount = 1;
                value = 1;
                icon = "Scroll/Scroll1";
                mesh = "Scroll/Scroll1";
                type = ItemTypes.Scroll;
                break;
            #endregion
            #region Misc 800-899
            case 800:
                name = "Old Book";
                description = "";
                amount = 1;
                value = 5;
                icon = "Misc/OldBook";
                mesh = "Misc/OldBook";
                type = ItemTypes.Misc;
                break;
            #endregion
            default:
                itemID = 303;
                name = "Cabbage";
                description = "";
                amount = 1;
                value = 100;
                heal = 2;
                icon = "Food/Cabbage";
                mesh = "Food/Cabbage";
                type = ItemTypes.Food;
                break;
        }
        Item temp = new Item
        {
            ID = itemID,
            Name = name,
            Description = description,
            Value = value,
            Amount = amount,
            Damage = damage,
            Armour = armour,
            Heal = heal,
            ItemType = type,
            IconName = Resources.Load("Icons/" + icon) as Sprite,
            MeshName = Resources.Load("Mesh/" + mesh) as GameObject,
        };
        return temp;
    }
}
