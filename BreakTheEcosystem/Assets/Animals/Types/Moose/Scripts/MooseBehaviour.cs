using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals.Fox
{
    public class MooseBehaviour : AnimalBehaviour
    {
        public MooseBehaviour() : base(AnimalType.Moose) { }

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