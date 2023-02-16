using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BTE.Menu
{
    public class DifficultySlider : MonoBehaviour
    {
        public TMP_Text text;
        public Navigator Nav;

        public void OnDifficultyChange(float difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    text.text = "EASY";
                    DifficultyManager.SetDifficulty(Difficulty.Easy);
                    break;
                case 1:
                    text.text = "HARD";
                    DifficultyManager.SetDifficulty(Difficulty.Hard);
                    break;
                case 2:
                    text.text = "EXPERT";
                    DifficultyManager.SetDifficulty(Difficulty.Expert);
                    break;
            }
            Nav.UpdateContractUI();
        }
    }
}