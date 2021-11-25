using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/*
Serialization is the automatic process of transforming data structures or objects stats into a format that unity can store and reconstruct later.

Serialization is the process of converting an object into a stream of bytes to store the object or transmit it to memory, database of file its main file.
Its main purpose is to save the state of an object to be able to recreate it when needed.

The reverse of Serialization is called??? Deserialization.
The Serialization system is written in C++

PlayerData or your main Data script is the bridge between your binary save and game, it stores all the info on the data you want to save
 */
public class PlayerData
{
    //Data from game we want to save
    public string playerName;
    public int level;
    public float maxHealth, maxMana, maxStamina;
    public float currentHealth, currentMana, currentStamina;
    public float pX, pY, pZ, rX, rY, rZ, rW;
    public PlayerData (PlayerHandler player)
    {
        playerName = player.name;
        maxHealth = player.attributes[0].maxValue;
        maxMana = player.attributes[1].maxValue;
        maxStamina = player.attributes[2].maxValue;
        currentHealth = player.attributes[0].maxValue;
        currentMana = player.attributes[1].maxValue;
        currentStamina = player.attributes[2].maxValue;

        pX = player.transform.position.x;
        pX = player.transform.position.y;
        pX = player.transform.position.z;
        rX = player.transform.rotation.x;
        rY = player.transform.rotation.y;
        rZ = player.transform.rotation.z;
        rW = player.transform.rotation.w;
    }
}
