using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager main;

        public PlayerStats Stats;

        private void Awake()
        {
            main = this;
        }
    }
}