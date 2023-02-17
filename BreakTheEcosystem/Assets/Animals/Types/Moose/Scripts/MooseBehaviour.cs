using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class MooseBehaviour : AnimalBehaviour
    {
        public MooseBehaviour() : base(AnimalType.Moose) { }

        protected override void Chase()
        {
            Agent.speed = BaseSpeed;
            Agent.SetDestination(PlayerMovement.main.transform.position);
            AttackObject.SetActive(true);
        }

        protected override void OnDamage(int damage)
        {
        }

        protected override void OnDeath()
        {
        }
    }
}