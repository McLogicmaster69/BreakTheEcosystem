using BTE.Managers;
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
            if (retreatTimer <= 0f)
            {
                Agent.speed = BaseSpeed;

                if (timeSinceTracked >= TimeToTrackPlayer)
                {
                    Agent.SetDestination(PlayerMovement.main.transform.position);
                }
                timeSinceTracked += Time.deltaTime;

                AttackObject.SetActive(true);
            }
            retreatTimer -= Time.deltaTime;
        }

        public override void runAttackSuccess()
        {
            retreatTimer = RetreatTimer;
            float wanderX = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
            float wanderZ = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
            Agent.SetDestination(new Vector3(wanderX, 1f, wanderZ));
        }

        protected override void OnDamage(int damage)
        {
        }

        protected override void OnDeath()
        {
        }
    }
}