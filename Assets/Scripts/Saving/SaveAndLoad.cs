using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public PlayerHandler player;
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey("Loaded"))
        {
            FirstLoad();
            PlayerPrefs.SetInt("Loaded", 0);
            Save();
        }
        else
        {
            Load();
        }
    }
    void FirstLoad()
    {
        player.attributes[0].maxValue = 100;
        player.attributes[1].maxValue = 100;
        player.attributes[2].maxValue = 100;
        player.attributes[0].currentValue = 75;
        player.attributes[1].currentValue = 75;
        player.attributes[2].currentValue = 75;
        player.transform.position = new Vector3(270, -6, 266);
    }
    public void Load()
    {
        PlayerData data = PlayerBinary.LoadData(player);
        player.name = data.playerName;
        player.attributes[0].maxValue = data.maxHealth;
        player.attributes[1].maxValue = data.maxMana;
        player.attributes[2].maxValue = data.maxStamina;
        player.attributes[0].currentValue = data.currentHealth;
        player.attributes[1].currentValue = data.currentMana;
        player.attributes[2].currentValue = data.currentStamina;
        player.transform.position = new Vector3(data.pX, data.pY, data.pZ);
        player.transform.rotation = new Quaternion(data.rX, data.rY, data.rZ, data.rW);
    }
    public void Save()
    {
        PlayerBinary.SaveData(player);
    }
}
