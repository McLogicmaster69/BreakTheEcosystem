using BTE.Managers;
using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class DogBehaviour : AnimalBehaviour
    {
        [HideInInspector] public int AttackState = 0;

        [Header("Stats")]
        [Header("Charge")]
        public float TimeToPrepareCharge = 2f;
        public float TimeSpentCharging = 2f;
        public float ChargeSpeed = 10f;
        [Header("Charge")]
        public float TimeToPrepareTrackCharge = 2f;
        public float TimeSpentTrackCharging = 2f;
        public float TrackChargeSpeed = 10f;
        public float RotationSpeed = 5f;
        [Header("Chase")]
        public float TimeToTrackPlayer = 0.25f;
        [Header("Audio")]
        public AudioSource Woof;

        private float timePreparingCharge = 0f;
        private float timeCharging = 0f;
        private float timeSinceTracked = 999f;
        private bool charging = false;

        public DogBehaviour() : base(AnimalType.Dogs) { }

        protected override void runAliveBehaviour()
        {
            AttackObject.SetActive(true);
            switch (AttackState)
            {
                case 0:
                    retreatTimer = 0f;
                    Agent.isStopped = true;
                    ChargeAttack();
                    break;
                case 1:
                    Agent.isStopped = false;
                    charging = false;
                    timePreparingCharge = 0f;
                    Chase();
                    break;
                case 2:
                    retreatTimer = 0f;
                    Agent.isStopped = true;
                    TrackChargeAttack();
                    break;
            }
        }
        public override void runAttackSuccess()
        {
            retreatTimer = RetreatTimer;
            float wanderX = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
            float wanderZ = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
            Agent.SetDestination(new Vector3(wanderX, 1f, wanderZ));
        }

        private void ChargeAttack()
        {
            if (charging)
            {
                transform.position += ChargeSpeed * transform.forward * Time.deltaTime;
                timeCharging += Time.deltaTime;
                if(timeCharging >= TimeSpentCharging)
                {
                    charging = false;
                    timePreparingCharge = 0f;
                }
            }
            else
            {
                Vector3 PlayerPosition = PlayerMovement.main.transform.position;
                Vector3 translation = PlayerPosition - transform.position;
                translation.y = 0;
                Quaternion rotation = Quaternion.LookRotation(translation);
                transform.rotation = rotation;

                timePreparingCharge += Time.deltaTime;
                if(timePreparingCharge >= TimeToPrepareCharge)
                {
                    charging = true;
                    timeCharging = 0f;
                }
            }
        }

        private void TrackChargeAttack()
        {
            if (charging)
            {
                Vector3 PlayerPosition = PlayerMovement.main.transform.position;
                Vector3 translation = PlayerPosition - transform.position;
                translation.y = 0;
                Vector3 reqRotation = Quaternion.LookRotation(translation).eulerAngles;
                Vector3 curRotation = transform.rotation.eulerAngles;

                if (curRotation.y != reqRotation.y)
                {
                    if (Mathf.Abs(curRotation.y - reqRotation.y) < Mathf.Abs(curRotation.y - (reqRotation.y + 360)))
                    {
                        if (curRotation.y < reqRotation.y)
                            curRotation.y += RotationSpeed * Time.deltaTime;
                        else if (curRotation.y > reqRotation.y)
                            curRotation.y -= RotationSpeed * Time.deltaTime;
                    }
                    else if (Mathf.Abs(curRotation.y - reqRotation.y) > Mathf.Abs(curRotation.y - (reqRotation.y + 360)))
                    {
                        curRotation.y += RotationSpeed * Time.deltaTime;
                    }
                }

                transform.rotation = Quaternion.Euler(curRotation);
                transform.position += TrackChargeSpeed * transform.forward * Time.deltaTime;
                timeCharging += Time.deltaTime;
                if (timeCharging >= TimeSpentTrackCharging)
                {
                    charging = false;
                    timePreparingCharge = 0f;
                }
            }
            else
            {
                Vector3 PlayerPosition = PlayerMovement.main.transform.position;
                Vector3 translation = PlayerPosition - transform.position;
                translation.y = 0;
                Quaternion rotation = Quaternion.LookRotation(translation);
                transform.rotation = rotation;

                timePreparingCharge += Time.deltaTime;
                if (timePreparingCharge >= TimeToPrepareTrackCharge)
                {
                    charging = true;
                    timeCharging = 0f;
                }
            }
        }
        protected override void Chase()
        {
            if (retreatTimer <= 0f)
            {
                SetAgentSpeed(BaseSpeed);

                if (timeSinceTracked >= TimeToTrackPlayer)
                {
                    Agent.SetDestination(PlayerMovement.main.transform.position);
                }
                timeSinceTracked += Time.deltaTime;
            }
            retreatTimer -= Time.deltaTime;
        }

        protected override void OnDamage(int damage)
        {
            Health += damage;
        }

        protected override void OnDeath()
        {
        }

        protected override void AfterStart()
        {
            StartCoroutine(Woofing());
        }

        private IEnumerator Woofing()
        {
            while (true)
            {
                Woof.Play();
                yield return new WaitForSeconds(Random.Range(3, 7));
            }
        }
    }
}