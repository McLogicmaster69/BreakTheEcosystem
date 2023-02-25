using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BTE.Managers
{
    public class GoalsUIManager : MonoBehaviour
    {
        private TMP_Text Text;

        private void Start()
        {
            Text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            string output = "";
            if (BDLCGameManager.KillsRemaining > 0)
                output += $"\nKill {BDLCGameManager.KillsRemaining} people";
            if (BDLCGameManager.C4Remaining > 0)
                output += "\nPlant C4";
            if (BDLCGameManager.BossRemaining)
                output += "\nKill the boss";
            if (BDLCGameManager.MoneyRemaining > 0)
                output += "\nSteal the money";
            if (BDLCGameManager.AnimalsRemaining > 0)
                output += "\nFree the animals";

            if (output.Length > 0)
                Text.text = output.Substring(1);
            else
                Text.text = "Escape";
        }
    }
}