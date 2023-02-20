using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BTE.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth main;
        private void Awake()
        {
            main = this;
        }

        [SerializeField] private Slider HealthSlider;

        public int Health = 100;
        private float TimeSinceHit = 100f;
        private float TimeSinceLastHealth = 100f;

        private void Update()
        {
            if(Health < 100)
            {
                if(TimeSinceHit >= DifficultyManager.RegenTime)
                {
                    if(TimeSinceLastHealth >= DifficultyManager.TimePerHealth)
                    {
                        TimeSinceLastHealth = 0f;
                        Health++;
                    }
                }
            }
            TimeSinceHit += Time.deltaTime;
            TimeSinceLastHealth += Time.deltaTime;
            HealthSlider.value = Health;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            TimeSinceHit = 0f;
            if (Health <= 0)
                Death();
        }

        private void Death()
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }
    }
}