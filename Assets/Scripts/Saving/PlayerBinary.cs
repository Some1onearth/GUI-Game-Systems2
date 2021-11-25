using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PlayerBinary
{
    public static void SaveData(PlayerHandler player)
    {
        //reference our binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        //location to save
        string path = Application.persistentDataPath + "/" + "Kitten" + ".jpeg";
        //create file at file path
        FileStream stream = new FileStream(path, FileMode.Create);
        //what data to write to the file
        PlayerData data = new PlayerData(player);
        //write it and convert to bytes
        formatter.Serialize(stream, data);
        //and we are done with the action
        stream.Close();
    }
    public static PlayerData LoadData(PlayerHandler player)
    {
        //location to load
        string path = Application.persistentDataPath + "/" + "Kitten" + ".jpeg";
        //if we have a file at that path
        if (File.Exists(path))
        {
            //get our binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //read the data from the path
            FileStream stream = new FileStream(path, FileMode.Open);
            //set the data from what it is back into its usable C# form
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            //we are done with the action
            stream.Close();
            //send usable data back to the PlayerData script
            return data;
        }
        return null;
    }
}
