using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BTE.BDLC.Timer
{
    public class BDLCGameTimer : MonoBehaviour
    {
        [SerializeField] private GameObject Timer;
        [SerializeField] private TMP_Text Text;
        private bool hasTimer = false;
        private float TimeRemaining;
        private void Start()
        {
            if (BDLCGameManager.TimeLimit == 0f)
                hasTimer = false;
            else
            {
                TimeRemaining = BDLCGameManager.TimeLimit;
                hasTimer = true;
            }
            Timer.SetActive(hasTimer);
        }
        private void Update()
        {
            if (hasTimer)
            {
                TimeRemaining -= Time.deltaTime;
                if (TimeRemaining <= 0f)
                {
                    Cursor.lockState = CursorLockMode.None;
                    TransitionManager.main.TransitionToScene(3);
                }
                Text.text = Mathf.Floor(TimeRemaining).ToString();
            }
        }
    }
}