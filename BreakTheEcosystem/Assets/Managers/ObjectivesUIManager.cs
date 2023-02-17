using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BTE.Managers
{
    public class ObjectivesUIManager : MonoBehaviour
    {
        private TMP_Text Text;

        private void Start()
        {
            Text = GetComponent<TMP_Text>();
        }
        private void Update()
        {
            string output = "";
            if (MainGameManager.TreesRemaining > 0)
                output += $"\nBurn {MainGameManager.TreesRemaining} trees";
            if (MainGameManager.TargetsRemaining > 0)
                output += $"\nKill {MainGameManager.TargetsRemaining} {MainGameManager.Target}";
            if (MainGameManager.SlaughterRemaining > 0)
                output += $"\nKill {MainGameManager.SlaughterRemaining} animals";
            if (MainGameManager.MooseRemaining)
                output += $"\nKill the moose";
            if (MainGameManager.GigaMooseRemaining)
                output += $"\nKill the giga moose";
            if (MainGameManager.BryceRemaining)
                output += $"\nKill Bryce";

            if (output.Length > 0)
                Text.text = output.Substring(1);
        }
    }
}