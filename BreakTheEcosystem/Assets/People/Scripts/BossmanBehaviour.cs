using BTE.Managers;
using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.People
{
    public class BossmanBehaviour : PersonBehaviour
    {
        public BossmanBehaviour() : base(PersonType.Boss) { }

        protected override void RunBehaviour()
        {
            Agent.enabled = false;
            FacePlayer();
        }
        private void FacePlayer()
        {
            Vector3 PlayerPosition = PlayerMovement.main.transform.position;
            Vector3 translation = PlayerPosition - transform.position;
            translation.y = 0;
            Quaternion rotation = Quaternion.LookRotation(translation);
            transform.rotation = rotation;
        }
        protected override void UpdateStats()
        {
            BDLCGameManager.KillBoss();
        }
    }
}