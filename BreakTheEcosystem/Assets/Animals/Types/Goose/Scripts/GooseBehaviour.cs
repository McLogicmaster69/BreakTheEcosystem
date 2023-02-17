using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class GooseBehaviour : AnimalBehaviour
    {
        public GooseBehaviour() : base(AnimalType.Geese) { }

        protected override void OnDamage(int damage)
        {
        }

        protected override void OnDeath()
        {
        }
    }
}