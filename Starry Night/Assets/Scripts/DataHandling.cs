using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class DataHandling
{
    // Path to savefile
    private static string pathName = "";

    // This method will be called at certain points to save all game data
    public static void SaveData(PlayerData data){
        FileStream fs = File.Create(pathName);
        // Write all playerdata to the file
    }

    // This method will be called everytime the game is launched
    public static void LoadData(PlayerData data){
        string loadData = File.ReadAllText(pathName);
        // Read all data from file to playerdata
    }
}
