using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class EnemyBullet : MonoBehaviour
    {
        public int Damage = 3;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth.main.TakeDamage(Damage);
            }
        }
    }
}