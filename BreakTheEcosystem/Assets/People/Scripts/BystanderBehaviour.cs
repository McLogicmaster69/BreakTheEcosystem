using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.People
{
    public class BystanderBehaviour : PersonBehaviour
    {
        public float WanderTime = 15f;

        private float wanderTimer = 0f;

        public BystanderBehaviour() : base(PersonType.Bystanders) { }

        protected override void BeforeStart()
        {
            Agent.speed += Random.Range(-3, 4) / 2f;
        }
        protected override void RunBehaviour()
        {
            WanderBuilding();
        }
        private void WanderBuilding()
        {
            if (Agent.remainingDistance <= 1f)
                wanderTimer = 0f;
            if(wanderTimer <= 0f)
            {
                wanderTimer = Random.Range(WanderTime - 5, WanderTime + 5);
                Wander();
            }
            wanderTimer -= Time.deltaTime;
        }
    }
}