using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//you will need to change scenes

public class CustomisationSet : Stats
{
    #region Variables
    [Header("Texture List")]
    //Texture2D List for skin,hair, mouth, eyes
    public List<Texture2D> skin = new List<Texture2D>();//1
    public List<Texture2D> mouth = new List<Texture2D>();//2
    public List<Texture2D> eyes = new List<Texture2D>();//3
    public List<Texture2D> hair = new List<Texture2D>();//4
    public List<Texture2D> clothes = new List<Texture2D>();//5
    public List<Texture2D> armour = new List<Texture2D>();//6
    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int mouthIndex, eyesIndex, hairIndex, clothesIndex, armourIndex, helmIndex;
    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    //public MeshRenderer characterMesh; //works the same as renderer because it's under the same library
    public Renderer character;
    public Renderer helm;
    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int mouthMax, eyesMax, hairMax, clothesMax, armourMax;
    [Header("Character Name")]
    //name of our character that the user is making
    public string characterName;
    [Header("Other")]
    public bool raceDrop;
    public string raceDropDisplay = "Select Race";
    public bool classDrop;
    public string classDropDisplay = "Select Class";
    public Vector2 scrollPosRace, scrollPosClass;
    public int bonusStats = 6;
    [Header("Stats Display")]
    public Text pointsText, strengthText, dexterityText, constitutionText, intelligenceText, wisdomText, charismaText;
    [System.Serializable]
    public struct PointUI
    {
        public Stats.StatBlock stat;
        public Text nameDisplay;
        public GameObject plusButton;
        public GameObject minusButton;
    }
    public PointUI[] pointSystem;


    private string[] materialNames = new string[7] { "Skin", "Mouth", "Eyes", "Hair", "Clothes", "Armour", "Helm" };
    private string[] attributeName = new string[3] { "Health", "Stamina", "Mana" };
    private string[] statName = new string[6] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
    void TextUpdate()
    {
        pointsText.text = "Points: " + bonusStats;
    }
    public void SetName(string charName)
    {
        characterName = charName;
    }
    #endregion
    #region Start
    //in start we need to set up the following
    private void Start()
    {
        for (int i = 0; i < attributeName.Length; i++)
        {
            attributes[i].name = attributeName[i];
        }
        for (int i = 0; i < statName.Length; i++)
        {
            pointSystem[i].stat.name = statName[i];
        }
        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            skin.Add(temp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            mouth.Add(temp);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            eyes.Add(temp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            hair.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            clothes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            armour.Add(temp);
        }
        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<Renderer>();
        helm = GameObject.Find("cap").GetComponent<Renderer>();
        //character = GameObject.Find("axe").GetComponent<Renderer>();
        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Hair", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        SetTexture("Helm", 0);
        #endregion
        #region Class Dropdown
        ChooseClass(0);
        var classDropDown = GameObject.Find("Dropdown - Class").GetComponent<Dropdown>(); //grab that dropdown
        classDropDown.options.Clear(); //clear options just in case

        CharacterClass[] classes = (CharacterClass[])System.Enum.GetValues(typeof(CharacterClass)); //put the class into array

        foreach (var item in classes)
        {
            //put the items in the array into the dropdown options
            classDropDown.options.Add(new Dropdown.OptionData() { text = item.ToString() });
        }
        #endregion
        #region Race Dropdown
        ChooseRace(0);
        var raceDropDown = GameObject.Find("Dropdown - Race").GetComponent<Dropdown>(); //grab that dropdown
        raceDropDown.options.Clear(); //clear options just in case

        CharacterRace[] races = (CharacterRace[])System.Enum.GetValues(typeof(CharacterRace)); //put the class into array

        foreach (var item in races)
        {
            //put the items in the array into the dropdown options
            raceDropDown.options.Add(new Dropdown.OptionData() { text = item.ToString() });
        }
        #endregion
        TextUpdate();
        ChooseClass(0);
        ChooseRace(0);
    }
    #endregion

    #region SetTexture
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    public void SetTexture(string type, int dir)
    {

        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        Renderer curRend = new Renderer();
        //inside a switch statement that is swapped by the string name of our material
        #region Switch Material
        switch (type)
        {
            #region Skin
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;
                //max is the same as our skin max
                max = skinMax;
                //textures is our skin list .ToArray()
                textures = skin.ToArray();
                //material index element number is 1
                matIndex = 1;
                //break
                curRend = character;
                break;
            #endregion Skin
            #region Mouth
            case "Mouth":
                //index is the same as our type index
                index = mouthIndex;
                //max is the same as our type max
                max = mouthMax;
                //textures is our type list .ToArray()
                textures = mouth.ToArray();
                //material index element number is i
                matIndex = 2;
                //break
                curRend = character;
                break;
            #endregion
            #region Mouth
            case "Eyes":
                //index is the same as our type index
                index = eyesIndex;
                //max is the same as our type max
                max = eyesMax;
                //textures is our type list .ToArray()
                textures = eyes.ToArray();
                //material index element number is i
                matIndex = 3;
                //break
                curRend = character;
                break;
            #endregion
            #region Hair
            case "Hair":
                //index is the same as our hair index
                index = hairIndex;
                //max is the same as our hair max
                max = hairMax;
                //textures is our hair list .ToArray()
                textures = hair.ToArray();
                //material index element number is 3
                matIndex = 4;
                //break
                curRend = character;
                break;
            #endregion
            #region Clothes
            case "Clothes":
                //index is the same as our type index
                index = clothesIndex;
                //max is the same as our type max
                max = clothesMax;
                //textures is our type list .ToArray()
                textures = clothes.ToArray();
                //material index element number is i
                matIndex = 5;
                //break
                curRend = character;
                break;
            #endregion
            #region Armour
            case "Armour":
                //index is the same as our type index
                index = armourIndex;
                //max is the same as our type max
                max = armourMax;
                //textures is our type list .ToArray()
                textures = armour.ToArray();
                //material index element number is i
                matIndex = 6;
                //break
                curRend = character;
                break;
            #endregion
            #region helm
            case "Helm":
                //index is the same as our type index
                index = helmIndex;
                //max is the same as our type max
                max = armourMax;
                //textures is our type list .ToArray()
                textures = armour.ToArray();
                //material index element number is i
                matIndex = 1;
                curRend = helm;
                //break
                break;
                #endregion
        }
        #endregion
        //outside our switch statement
        #region Assign Direction
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] mat = curRend.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        curRend.materials = mat;

        #endregion
        //create another switch that is goverened by the same string name of our material
        #region Set Material Switch
        switch (type)
        {
            //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
            case "Helm":
                helmIndex = index;
                break;
        }
        #endregion
    }
    #endregion
    #region Class and Stats
    //create a switch statement that holds the base class
    public void ChooseClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                pointSystem[0].stat.value = 15;//str
                pointSystem[1].stat.value = 12;//dex
                pointSystem[2].stat.value = 14;//con
                pointSystem[3].stat.value = 12;//int
                pointSystem[4].stat.value = 8;//wis
                pointSystem[5].stat.value = 10;//char
                characterClass = CharacterClass.Barbarian;

                break;
            case 1:
                pointSystem[0].stat.value = 8;//str
                pointSystem[1].stat.value = 10;//dex
                pointSystem[2].stat.value = 13;//con
                pointSystem[3].stat.value = 12;//int
                pointSystem[4].stat.value = 14;//wis
                pointSystem[5].stat.value = 15;//char
                characterClass = CharacterClass.Bard;
                break;
            case 2:
                pointSystem[0].stat.value = 8;//str
                pointSystem[1].stat.value = 12;//dex
                pointSystem[2].stat.value = 12;//con
                pointSystem[3].stat.value = 14;//int
                pointSystem[4].stat.value = 15;//wis
                pointSystem[5].stat.value = 11;//char
                
                characterClass = CharacterClass.Cleric;
                break;
            case 3:
                pointSystem[0].stat.value = 8;//str
                pointSystem[1].stat.value = 11;//dex
                pointSystem[2].stat.value = 13;//con
                pointSystem[3].stat.value = 13;//int
                pointSystem[4].stat.value = 14;//wis
                pointSystem[5].stat.value = 12;//char
                characterClass = CharacterClass.Druid;
                break;
            case 4:
                pointSystem[0].stat.value = 14;//str
                pointSystem[1].stat.value = 14;//dex
                pointSystem[2].stat.value = 15;//con
                pointSystem[3].stat.value = 11;//int
                pointSystem[4].stat.value = 13;//wis
                pointSystem[5].stat.value = 8;//char
                characterClass = CharacterClass.Fighter;
                break;
            case 5:
                pointSystem[0].stat.value = 14;//str
                pointSystem[1].stat.value = 15;//dex
                pointSystem[2].stat.value = 15;//con
                pointSystem[3].stat.value = 8;//int
                pointSystem[4].stat.value = 13;//wis
                pointSystem[5].stat.value = 13;//char
                characterClass = CharacterClass.JackieChan;
                break;
            case 6:
                pointSystem[0].stat.value = 14;//str
                pointSystem[1].stat.value = 12;//dex
                pointSystem[2].stat.value = 13;//con
                pointSystem[3].stat.value = 15;//int
                pointSystem[4].stat.value = 13;//wis
                pointSystem[5].stat.value = 12;//char
                characterClass = CharacterClass.Monk;
                break;
            case 7:
                pointSystem[0].stat.value = 13;//str
                pointSystem[1].stat.value = 8;//dex
                pointSystem[2].stat.value = 14;//con
                pointSystem[3].stat.value = 13;//int
                pointSystem[4].stat.value = 12;//wis
                pointSystem[5].stat.value = 15;//char
                characterClass = CharacterClass.Paladin;
                break;
            case 8:
                pointSystem[0].stat.value = 12;//str
                pointSystem[1].stat.value = 15;//dex
                pointSystem[2].stat.value = 13;//con
                pointSystem[3].stat.value = 14;//int
                pointSystem[4].stat.value = 12;//wis
                pointSystem[5].stat.value = 11;//char
                characterClass = CharacterClass.Ranger;
                break;
            case 9:
                pointSystem[0].stat.value = 12;//str
                pointSystem[1].stat.value = 15;//dex
                pointSystem[2].stat.value = 12;//con
                pointSystem[3].stat.value = 13;//int
                pointSystem[4].stat.value = 8;//wis
                pointSystem[5].stat.value = 14;//char
                characterClass = CharacterClass.Rogue;
                break;
            case 10:
                pointSystem[0].stat.value = 8;//str
                pointSystem[1].stat.value = 12;//dex
                pointSystem[2].stat.value = 15;//con
                pointSystem[3].stat.value = 13;//int
                pointSystem[4].stat.value = 14;//wis
                pointSystem[5].stat.value = 11;//char
                characterClass = CharacterClass.Sorcerer;
                break;
            case 11:
                pointSystem[0].stat.value = 8;//str
                pointSystem[1].stat.value = 12;//dex
                pointSystem[2].stat.value = 13;//con
                pointSystem[3].stat.value = 14;//int
                pointSystem[4].stat.value = 15;//wis
                pointSystem[5].stat.value = 11;//char
                characterClass = CharacterClass.Warlock;
                break;
            case 12:
                pointSystem[0].stat.value = 8;//str
                pointSystem[1].stat.value = 9;//dex
                pointSystem[2].stat.value = 11;//con
                pointSystem[3].stat.value = 15;//int
                pointSystem[4].stat.value = 15;//wis
                pointSystem[5].stat.value = 13;//char
                characterClass = CharacterClass.Wizard;
                break;
        }
        for (int i = 0; i < pointSystem.Length; i++)
        {
            //reset points
            bonusStats = 6;
            //display changes
            pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.raceValue);
            //reset buttons
            pointSystem[i].minusButton.SetActive(false);
            pointSystem[i].plusButton.SetActive(true);
        }
    }
    #endregion
    #region Race and Bonus
    public void ChooseRace(int raceIndex)
    {
        // =6
        switch (raceIndex)
        {
            case 0:
                pointSystem[0].stat.raceValue = 4;//str
                pointSystem[1].stat.raceValue = 4;//dex
                pointSystem[2].stat.raceValue = 6;//con
                pointSystem[3].stat.raceValue = 0;//int
                pointSystem[4].stat.raceValue = 0;//wis
                pointSystem[5].stat.raceValue = 1;//char
                characterRace = CharacterRace.ActionHero;
                break;
                

            case 1:
                pointSystem[0].stat.raceValue = 3;//str
                pointSystem[1].stat.raceValue = 0;//dex
                pointSystem[2].stat.raceValue = 0;//con
                pointSystem[3].stat.raceValue = 0;//int
                pointSystem[4].stat.raceValue = 0;//wis
                pointSystem[5].stat.raceValue = 3;//char
                characterRace = CharacterRace.Dragonborn;                
                break;
            case 2:
                pointSystem[0].stat.raceValue = 2;//str
                pointSystem[1].stat.raceValue = 0;//dex
                pointSystem[2].stat.raceValue = 4;//con
                pointSystem[3].stat.raceValue = 0;//int
                pointSystem[4].stat.raceValue = 0;//wis
                pointSystem[5].stat.raceValue = 0;//char
                characterRace = CharacterRace.Dwarf;                
                break;
            case 3:
                pointSystem[0].stat.raceValue = 0;//str
                pointSystem[1].stat.raceValue = 2;//dex
                pointSystem[2].stat.raceValue = 0;//con
                pointSystem[3].stat.raceValue = 2;//int
                pointSystem[4].stat.raceValue = 2;//wis
                pointSystem[5].stat.raceValue = 0;//char
                characterRace = CharacterRace.Elf;                
                break;
            case 4:
                pointSystem[0].stat.raceValue = 0;//str
                pointSystem[1].stat.raceValue = 2;//dex
                pointSystem[2].stat.raceValue = 0;//con
                pointSystem[3].stat.raceValue = 3;//int
                pointSystem[4].stat.raceValue = 1;//wis
                pointSystem[5].stat.raceValue = 0;//char
                characterRace = CharacterRace.Gnome;                
                break;
            case 5:
                pointSystem[0].stat.raceValue = 2;//str
                pointSystem[1].stat.raceValue = 2;//dex
                pointSystem[2].stat.raceValue = 0;//con
                pointSystem[3].stat.raceValue = 1;//int
                pointSystem[4].stat.raceValue = 1;//wis
                pointSystem[5].stat.raceValue = 0;//char
                characterRace = CharacterRace.HalfElf;
                break;
            case 6:
                pointSystem[0].stat.raceValue = 0;//str
                pointSystem[1].stat.raceValue = 3;//dex
                pointSystem[2].stat.raceValue = 2;//con
                pointSystem[3].stat.raceValue = 1;//int
                pointSystem[4].stat.raceValue = 0;//wis
                pointSystem[5].stat.raceValue = 0;//char
                characterRace = CharacterRace.Halfling;
                break;
            case 7:
                pointSystem[0].stat.raceValue = 3;//str
                pointSystem[1].stat.raceValue = 0;//dex
                pointSystem[2].stat.raceValue = 3;//con
                pointSystem[3].stat.raceValue = 0;//int
                pointSystem[4].stat.raceValue = 0;//wis
                pointSystem[5].stat.raceValue = 0;//char
                characterRace = CharacterRace.HalfOrc;
                break;
            case 8:
                pointSystem[0].stat.raceValue = 1;//str
                pointSystem[1].stat.raceValue = 1;//dex
                pointSystem[2].stat.raceValue = 1;//con
                pointSystem[3].stat.raceValue = 1;//int
                pointSystem[4].stat.raceValue = 1;//wis
                pointSystem[5].stat.raceValue = 1;//char
                characterRace = CharacterRace.Human;
                break;
            case 9:
                pointSystem[0].stat.raceValue = 2;//str
                pointSystem[1].stat.raceValue = 2;//dex
                pointSystem[2].stat.raceValue = 0;//con
                pointSystem[3].stat.raceValue = 1;//int
                pointSystem[4].stat.raceValue = 1;//wis
                pointSystem[5].stat.raceValue = 0;//char
                characterRace = CharacterRace.Tiefling;
                break;
        }
        for (int i = 0; i < pointSystem.Length; i++)
        {
            //display changes
            pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.raceValue);
            //reset buttons
            pointSystem[i].minusButton.SetActive(false);
            pointSystem[i].plusButton.SetActive(true);
        }
    }
    #endregion
    #region Save
    //Function called Save this will allow us to save our indexes to PlayerPrefs
    public void SaveCharacter()
    {
        //SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        PlayerPrefs.SetInt("HelmIndex", helmIndex);
        //SetString CharacterName, class, race
        PlayerPrefs.SetString("CharacterName", characterName);
        PlayerPrefs.SetString("CharacterClass", characterClass.ToString());
        PlayerPrefs.SetString("CharacterRace", characterRace.ToString());
        //int loop stats
        for (int i = 0; i < pointSystem.Length; i++)
        {
            PlayerPrefs.SetInt(pointSystem[i].stat.name, (pointSystem[i].stat.value + pointSystem[i].stat.raceValue + pointSystem[i].stat.levelTemptValue));
        }
    }
    #endregion
    #region Canvas
    #region Random and Reset button
    public void RandomiseButton()
    {
        skinIndex = Random.Range(0, skinMax);
        mouthIndex = Random.Range(0, mouthMax);
        eyesIndex = Random.Range(0, eyesMax);
        hairIndex = Random.Range(0, hairMax);
        clothesIndex = Random.Range(0, clothesMax);
        armourIndex = Random.Range(0, armourMax);
        helmIndex = Random.Range(0, armourMax);
        {
            SetTexture("Skin", 0);
            SetTexture("Mouth", 0);
            SetTexture("Eyes", 0);
            SetTexture("Hair", 0);
            SetTexture("Clothes", 0);
            SetTexture("Armour", 0);
            SetTexture("Helm", 0);
        }
    }
    public void ResetButton()
    {
        SetTexture("Skin", skinIndex = 0);
        SetTexture("Mouth", mouthIndex = 0);
        SetTexture("Eyes", eyesIndex = 0);
        SetTexture("Hair", hairIndex = 0);
        SetTexture("Clothes", clothesIndex = 0);
        SetTexture("Armour", armourIndex = 0);
        SetTexture("Helm", helmIndex = 0);
    }
    #endregion
    #region Set Texture buttons
    public void SetTexttureTypeRight(string type) //allows the data (e.g. Skin) to be set in the inspecter
    {
        SetTexture(type, 1);
    }
    public void SetTexttureTypeLeft(string type)
    {
        SetTexture(type, -1);
    }
    #endregion
    #region Set Bonus
    public void SetBonusPos(int i)
    {
        //change the values
        bonusStats--;
        pointSystem[i].stat.tempValue++;
        //if we have no points hide the pos button
        if (bonusStats <= 0)
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(false);
            }
        }
        //if we haven't shown the minus button then activate
        if (pointSystem[i].minusButton.activeSelf == false)
        {
            pointSystem[i].minusButton.SetActive(true);
        }
        //update text
        pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.raceValue + pointSystem[i].stat.tempValue);
        TextUpdate();
    }
    public void SetBonusNeg(int i)
    {
        //change the values
        bonusStats++;
        pointSystem[i].stat.tempValue--;
        //if we have no temp points hide the neg button
        if (pointSystem[i].stat.tempValue <= 0)
        {            
            pointSystem[i].minusButton.SetActive(false);
        }        
        //if we haven't shown the plus button then activate
        if (pointSystem[i].plusButton.activeSelf == false )
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(true);
            }
        }
        pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.raceValue + pointSystem[i].stat.tempValue);
        TextUpdate();
    }
    #endregion
    #endregion
    //#region OnGUI
    //private void OnGUI()//Function for our GUI elements
    //{
    //    //create the floats scrW and scrH that govern our 16:9 ratio
    //    Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);
    //    //create an int that will help with shuffling your GUI elements under eachother
    //    #region Buttons and Display for Custom
    //    for (int i = 0; i < materialNames.Length; i++)
    //    {
    //        //GUI button on the left of the screen with the contence <
    //        if (GUI.Button(new Rect(0.25f * scr.x, 2.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "<"))
    //        {
    //            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
    //            SetTexture(materialNames[i], -1);
    //        }
    //        //GUI Box or Lable on the left of the screen with the contence
    //        GUI.Box(new Rect(0.75f * scr.x, 2.5f * scr.y + (i * 0.5f * scr.y), 1.5f * scr.x, 0.5f * scr.y), materialNames[i]);

    //        //GUI button on the left of the screen with the contence >
    //        if (GUI.Button(new Rect(2.25f * scr.x, 2.5f * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), ">"))
    //        {
    //            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  1
    //            SetTexture(materialNames[i], 1);
    //        }
    //        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
    //    }
    //    #endregion
    //    #region Random Reset
    //    if (GUI.Button(new Rect(0.25f * scr.x, 2.5f * scr.y + (materialNames.Length * 0.5f * scr.y), 1.25f * scr.x, 0.5f * scr.y), "Randomise"))
    //    {
    //        skinIndex = Random.Range(0, skinMax);
    //        mouthIndex = Random.Range(0, mouthMax);
    //        eyesIndex = Random.Range(0, eyesMax);
    //        hairIndex = Random.Range(0, hairMax);
    //        clothesIndex = Random.Range(0, clothesMax);
    //        armourIndex = Random.Range(0, armourMax);
    //        helmIndex = Random.Range(0, armourMax);
    //        {
    //            SetTexture("Skin", 0);
    //            SetTexture("Mouth", 0);
    //            SetTexture("Eyes", 0);
    //            SetTexture("Hair", 0);
    //            SetTexture("Clothes", 0);
    //            SetTexture("Armour", 0);
    //            SetTexture("Helm", 0);
    //        }
    //    }
    //    if (GUI.Button(new Rect(1.5f * scr.x, 2.5f * scr.y + (materialNames.Length * 0.5f * scr.y), 1.25f * scr.x, 0.5f * scr.y), "Reset"))
    //    {
    //        SetTexture("Skin", skinIndex = 0);
    //        SetTexture("Mouth", mouthIndex = 0);
    //        SetTexture("Eyes", eyesIndex = 0);
    //        SetTexture("Hair", hairIndex = 0);
    //        SetTexture("Clothes", clothesIndex = 0);
    //        SetTexture("Armour", armourIndex = 0);
    //        SetTexture("Helm", helmIndex = 0);
    //    }
    //    //create 2 buttons one Random and one Reset
    //    //Random will feed a random amount to the direction 
    //    //reset will set all to 0 both use SetTexture
    //    //move down the screen with the int using ++ each grouping of GUI elements are moved using this
    //    #endregion
    //    #region Character Name
    //    //name of our character equals a GUI TextField that holds our character name and limit of characters
    //    //move down the screen with the int using ++ each grouping of GUI elements are moved using this
    //    characterName = GUI.TextField(new Rect(0.25f * scr.x, 2.5f * scr.y + (materialNames.Length + 1) * (0.5f * scr.y), 2.5f * scr.x, 0.5f * scr.y), characterName, 32);
    //    //GUI Button called Save and Play
    //    //this button will run the save function and also load into the game level
    //    #endregion
    //    #region Select Class
    //    //button for toggling dropdown
    //    if (GUI.Button(new Rect(12.75f * scr.x, 2.5f * scr.y, 2 * scr.x, 0.5f * scr.y), classDropDisplay))
    //    {
    //        classDrop = !classDrop;
    //    }
    //    //if dropdown - scroll view that displays our classes as selectable buttons
    //    if (classDrop)
    //    {
    //        float listSize = System.Enum.GetNames(typeof(CharacterClass)).Length;
    //        scrollPosClass = GUI.BeginScrollView(new Rect(12.75f * scr.x, 3f * scr.y, 2 * scr.x, 4f * scr.y), scrollPosClass, new Rect(0, 0, 0, listSize * 0.5f * scr.y));
    //        GUI.Box(new Rect(0, 0, 1.75f * scr.x, listSize * 0.5f * scr.y), "");
    //        for (int i = 0; i < listSize; i++)
    //        {
    //            if (GUI.Button(new Rect(0, 0.5f * scr.y * i, 1.75f * scr.x, 0.5f * scr.y), System.Enum.GetNames(typeof(CharacterClass))[i]))
    //            {
    //                ChooseClass(i);
    //                classDropDisplay = System.Enum.GetNames(typeof(CharacterClass))[i];
    //                classDrop = false;
    //            }
    //        }
    //        GUI.EndScrollView();
    //    }
    //    #endregion
    //    #region Select Race
    //    if (!classDrop)
    //    {
    //        if (GUI.Button(new Rect(12.75f * scr.x, 3f * scr.y, 2 * scr.x, 0.5f * scr.y), raceDropDisplay))
    //        {
    //            raceDrop = !raceDrop;
    //        }
    //        //if dropdown - scroll view that displays our classes as selectable buttons
    //        if (raceDrop)
    //        {
    //            float listSize = System.Enum.GetNames(typeof(CharacterRace)).Length;
    //            scrollPosRace = GUI.BeginScrollView(new Rect(12.75f * scr.x, 3.5f * scr.y, 2 * scr.x, 4f * scr.y), scrollPosRace, new Rect(0, 0, 0, listSize * 0.5f * scr.y));
    //            GUI.Box(new Rect(0, 0, 1.75f * scr.x, listSize * 0.5f * scr.y), "");
    //            for (int i = 0; i < listSize; i++)
    //            {
    //                if (GUI.Button(new Rect(0, 0.5f * scr.y * i, 1.75f * scr.x, 0.5f * scr.y), System.Enum.GetNames(typeof(CharacterRace))[i]))
    //                {
    //                    ChooseRace(i);
    //                    raceDropDisplay = System.Enum.GetNames(typeof(CharacterRace))[i];
    //                    raceDrop = false;
    //                }
    //            }
    //            GUI.EndScrollView();
    //        }
    //    }
    //    #endregion
    //    #region Add Points
    //    // stats - display stats
    //    if (!classDrop || !raceDrop)
    //    {
    //        //this is made to not overspend your points

    //        // Box for points to spend
    //        GUI.Box(new Rect(10.75f * scr.x, 3.5f * scr.y, 2 * scr.x, 0.5f * scr.y), "Points:" + bonusStats);
    //        // + and - buttons on either side of a box/label
    //        for (int i = 0; i < characterStats.Length; i++)
    //        {
    //            //-
    //            //if our points are below 6 && the temp value is above 0
    //            if (bonusStats < 6 && characterStats[i].levelTemptValue > 0)
    //            {
    //                if (GUI.Button(new Rect(10.25f * scr.x, 4 * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "-"))
    //                {
    //                    //remove points from level temp and add points to bonus stats
    //                    bonusStats++;
    //                    characterStats[i].levelTemptValue--;
    //                }
    //            }
    //            //type
    //            //display total stats and stat name
    //            GUI.Box(new Rect(10.75f * scr.x, 4 * scr.y + (i * 0.5f * scr.y), 2f * scr.x, 0.5f * scr.y), characterStats[i].name + ": " + (characterStats[i].value + characterStats[i].tempValue + characterStats[i].levelTemptValue));
    //            //+
    //            //if bonus stats are above 0
    //            if (bonusStats > 0)
    //            {
    //                if (GUI.Button(new Rect(12.75f * scr.x, 4 * scr.y + (i * 0.5f * scr.y), 0.5f * scr.x, 0.5f * scr.y), "+"))
    //                {
    //                    //remove points from bonus stats and add points to level temp
    //                    bonusStats--;
    //                    characterStats[i].levelTemptValue++;
    //                }
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Save and Play
    //    // display button of name/Class/Race/Points
    //    if (characterName != "" && classDropDisplay != "Select Class" && raceDropDisplay != "Select Race" && bonusStats == 0)
    //    {
    //        //GUI Button called Save and Play
    //        if (GUI.Button(new Rect(13 * scr.x, 8 * scr.y, 2 * scr.x, 0.5f * scr.y), "Save and Play"))
    //        {
    //            //this button will run the save function
    //            SaveCharacter();
    //            //and also load into the game level
    //            SceneManager.LoadScene(2);
    //        }
    //    }
    //    #endregion
    //}
    //#endregion
}