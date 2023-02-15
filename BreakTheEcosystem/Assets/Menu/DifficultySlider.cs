using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BTE.Menu
{
    public class DifficultySlider : MonoBehaviour
    {
        public TMP_Text text;
        public void OnDifficultyChange(float difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    text.text = "EASY";
                    break;
                case 1:
                    text.text = "HARD";
                    break;
                case 2:
                    text.text = "EXPERT";
                    break;
            }
        }
    }
}