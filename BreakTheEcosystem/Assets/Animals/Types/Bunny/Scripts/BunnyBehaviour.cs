using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class BunnyBehaviour : AnimalBehaviour
    {
        public BunnyBehaviour() : base(AnimalType.Rabbits) { }

        protected override void Attack()
        {
        }

        protected override void OnDamage(int damage)
        {
        }

        protected override void OnDeath()
        {
        }
    }
}