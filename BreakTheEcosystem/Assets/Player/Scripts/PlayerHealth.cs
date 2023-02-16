using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTE.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public int Health = 100;
        public float RegenTime = 10f;
        public float TimePerHealth = 0.1f;
        private float TimeSinceHit = 100f;
        private float TimeSinceLastHealth = 100f;

        private void Update()
        {
            if(Health < 100)
            {
                if(TimeSinceHit >= RegenTime)
                {
                    if(TimeSinceLastHealth >= TimePerHealth)
                    {
                        TimeSinceLastHealth = 0f;
                        Health++;
                    }
                }
            }
            TimeSinceHit += Time.deltaTime;
            TimeSinceLastHealth += Time.deltaTime;
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
            SceneManager.LoadScene(0);
        }
    }
}