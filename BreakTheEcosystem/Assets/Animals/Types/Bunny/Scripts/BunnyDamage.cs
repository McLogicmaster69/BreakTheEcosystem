using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class BunnyDamage : MonoBehaviour
    {
        public BunnyBehaviour Behaviour;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Behaviour.TakeDamage(1);
            }
        }
    }
}