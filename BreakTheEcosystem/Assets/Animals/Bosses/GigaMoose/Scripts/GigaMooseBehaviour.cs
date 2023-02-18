using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class GigaMooseBehaviour : AnimalBehaviour
    {
        public GigaMooseBehaviour() : base(AnimalType.GigaMoose) { }

        protected override void OnDamage(int damage)
        {
        }

        protected override void OnDeath()
        {
        }
    }
}