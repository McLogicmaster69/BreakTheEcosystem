using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class MooseBehaviour : AnimalBehaviour
    {
        public float TimeToTrackPlayer = 0.25f;
        private float timeSinceTracked = 0f;

        public MooseBehaviour() : base(AnimalType.Moose) { }

        protected override void Chase()
        {
            Agent.speed = BaseSpeed;

            if(timeSinceTracked >= TimeToTrackPlayer)
            {
                Agent.SetDestination(PlayerMovement.main.transform.position);
            }
            timeSinceTracked += Time.deltaTime;

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