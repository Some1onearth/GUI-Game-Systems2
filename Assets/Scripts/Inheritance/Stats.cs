using UnityEngine;
using System;

public class Stats : Attributes
{
    //Str - crush a tomato - Brute force
    //Dex - dodge a tomato/shoot a tomato - accuracy and cat liek reflexes
    //Con - eat a bad tomato - health and resistence
    //Int - knowing that a tomato is a fruit - book smart
    //Wis - wisdom is knowing not to put tomato into a fruit salad - street smart
    //Char - the ability to sell a tomato based fruit salad
    #region Struct
    [Serializable]
    public struct StatBlock
    {
        //name of our stat
        public string name;
        //base value of our stat
        public int value;
        //race value of our stat
        public int raceValue;
        //buff or debuff value being applied
        public int tempValue;
        //temp value for leveling
        public int levelTemptValue;

    }
    #endregion
    #region Variables
    //all things you can kill, start with 3 attributes eg health, stamina and mana
    public StatBlock[] characterStats = new StatBlock[6];
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public CharacterRace characterRace = CharacterRace.Human;
    #endregion
}
public enum CharacterClass //placing this outside script allows it to be accessed anywhere
{
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Fighter,
    JackieChan,
    Monk,
    Paladin,
    Ranger,
    Rogue,
    Sorcerer,
    Warlock,
    Wizard
}
public enum CharacterRace //placing this outside script allows it to be accessed anywhere
{
    ActionHero,
    Dragonborn,
    Dwarf,
    Elf,
    Gnome,
    HalfElf,
    Halfling,
    HalfOrc,
    Human,
    Tiefling
}