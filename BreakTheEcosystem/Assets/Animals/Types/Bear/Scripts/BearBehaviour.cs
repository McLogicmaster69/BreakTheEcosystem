using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class BearBehaviour : AnimalBehaviour
    {
        public BearBehaviour() : base(AnimalType.Bears) { }

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