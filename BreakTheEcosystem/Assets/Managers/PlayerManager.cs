using BTE.Player;
using BTE.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Managers
{
    public static class PlayerManager
    {
        public static PlayerStats Stats = new PlayerStats();
        public static void SaveStats()
        {
            SaveSystem.Save<PlayerStats>(Stats, "player", ".stats");
        }
        public static void LoadStats()
        {
            if(SaveSystem.Exists("player", ".stats"))
                Stats = SaveSystem.Load<PlayerStats>("player", ".stats");
            else
                SaveSystem.Save<PlayerStats>(Stats, "player", ".stats");
        }
    }
}