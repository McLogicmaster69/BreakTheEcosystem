using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTE.Timer
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private GameObject Timer;
        [SerializeField] private TMP_Text Text;
        private bool hasTimer = false;
        private float TimeRemaining;
        private void Start()
        {
            if (MainGameManager.TimeLimit == 0f)
                hasTimer = false;
            else
            {
                TimeRemaining = MainGameManager.TimeLimit;
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
                    SceneManager.LoadScene(0);
                }
                Text.text = Mathf.Floor(TimeRemaining).ToString();
            }
        }
    }
}