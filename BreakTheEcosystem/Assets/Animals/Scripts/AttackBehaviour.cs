using BTE.Managers;
using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class AttackBehaviour : MonoBehaviour
    {
        [SerializeField] private AnimalBehaviour Behaviour;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth.main.TakeDamage(Behaviour.Damage);
                Behaviour.runAttackSuccess();
            }
        }
    }
}