using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class DataHandling
{
    // Path to savefile
    private static string pathName = "";

    /// <summary>
    /// Saves game data to file at the given path
    /// </summary>
    /// <param name="data">The player data to be saved</param>
    public static void SaveData(PlayerData data){
        FileStream fs = File.Create(pathName);
        // Write all playerdata to the file
    }

    /// <summary>
    /// Loads game data from fiile at the given path. This will be called whenever the game is opened to return the user to the last save
    /// </summary>
    /// <param name="data">The PlayerData object that will contain the loaded game data</param>
    public static void LoadData(PlayerData data){
        string loadData = File.ReadAllText(pathName);
        // Read all data from file to playerdata
    }
}
