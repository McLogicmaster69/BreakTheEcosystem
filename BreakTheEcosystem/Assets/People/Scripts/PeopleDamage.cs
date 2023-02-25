using BTE.Managers;
using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.People
{
    public class PeopleDamage : MonoBehaviour
    {
        public float Damage = 3;
        private void Start()
        {
            Damage *= DifficultyManager.AttackMultiplier;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth.main.TakeDamage(Mathf.FloorToInt(Damage));
            }
        }
    }
}