using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.People
{
    public class BystanderBehaviour : PersonBehaviour
    {
        public float WanderTime = 15f;

        private float wanderTimer = 0f;

        protected override void RunBehaviour()
        {
            WanderBuilding();
        }
        private void WanderBuilding()
        {
            if(wanderTimer <= 0f)
            {
                wanderTimer = WanderTime;
                Wander();
            }
            wanderTimer -= Time.deltaTime;
        }
    }
}