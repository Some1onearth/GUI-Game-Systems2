using UnityEngine;

public class CustomisationGet : MonoBehaviour
{
    public Renderer character, helm;
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Load()
    {
        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        SetTexture("Helm", PlayerPrefs.GetInt("HelmIndex"));

    }
    void SetTexture(string type, int index  )
    {
        Texture2D texture = null;
        int matIndex = 0;
        Renderer rend = new Renderer();
        switch (type)
        {
            case "Skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;
                rend = character;
            break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 2;
                rend = character;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 3;
                rend = character;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 4;
                rend = character;
                break;
            case "Clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                matIndex = 5;
                rend = character;
                break;
            case "Armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                matIndex = 6;
                rend = character;
                break;
            case "Helm":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                matIndex = 1;
                rend = helm;
                break;
        }
        Material[] mats = rend.materials;
        mats[matIndex].mainTexture = texture;
        rend.materials = mats;
    }
}
