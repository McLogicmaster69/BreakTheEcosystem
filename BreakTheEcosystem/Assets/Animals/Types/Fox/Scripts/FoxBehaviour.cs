using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals.Fox
{
    public class FoxBehaviour : AnimalBehaviour
    {
        public FoxBehaviour() : base(AnimalType.Foxes) { }

        protected override void Attack()
        {
        }

        protected override void Flee()
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