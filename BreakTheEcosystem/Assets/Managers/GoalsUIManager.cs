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
                output += $"Kill {BDLCGameManager.KillsRemaining} people";
            if (BDLCGameManager.C4Remaining > 0)
                output += "Plant C4";
            if (BDLCGameManager.BossRemaining)
                output += "Kill the boss";
            if (BDLCGameManager.MoneyRemaining > 0)
                output += "Steal the money";
            if (BDLCGameManager.AnimalsRemaining > 0)
                output += "Free the animals";

            if (output.Length > 0)
                Text.text = output.Substring(1);
        }
    }
}