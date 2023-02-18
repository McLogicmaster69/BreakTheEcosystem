using BTE.Player;
using BTE.Trees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTE.Animals
{
    public class GigaMooseBehaviour : AnimalBehaviour
    {
        [Header("Audio")]
        public AudioSource Audio;
        public AudioClip MoanSound;
        public AudioClip SummonSound;

        [Header("Stats")]
        public float ChargeTimer = 2f;
        public float ChargeWait = 2f;
        public float SummonWait = 30f;
        public float ShootWait = 10f;

        [Header("Objects")]
        public GameObject MooseMinion1;
        public GameObject MooseMinion2;
        public GameObject MooseProjectile;

        private bool wandering = true;
        private bool charging = false;

        private float timeSinceCharge = 0f;
        private float timeSinceStartCharge = 0f;
        private float timeSinceSummon = 0f;
        private float timeSinceShoot = 0f;

        private int prevStage = 0;
        private int currentLocation = 0;

        private Slider HealthBar;

        public GigaMooseBehaviour() : base(AnimalType.GigaMoose) { }

        protected override void runAliveBehaviour()
        {
            HealthBar.value = Health;
            if (Alive)
            {
                runUpdateStats();
                if (State == AnimalState.Wander)
                    runWander();
                else
                    runAttackProgram();
            }
        }

        private void runAttackProgram()
        {
            if (Health >= MaxHealth * 0.7f)
                runPhase1();
            else if(Health >= MaxHealth * 0.35f)
                runPhase2();
            else
                runPhase3();
        }

        private void runPhase1()
        {
            if(prevStage == 0)
                prevStage = 1;

            AttackObject.SetActive(true);
            if (charging)
            {
                timeSinceCharge = ChargeWait;
            }
            else if(timeSinceCharge <= 0f)
            {
                timeSinceCharge = ChargeWait;
                StartCoroutine(ChargeAttack());
            }

            timeSinceCharge -= Time.deltaTime;
            timeSinceStartCharge += Time.deltaTime;
        }

        private void runPhase2()
        {
            if(prevStage == 1)
            {
                Audio.clip = MoanSound;
                Audio.Play();
                prevStage = 2;
            }

            if(timeSinceSummon >= SummonWait)
            {
                Audio.clip = SummonSound;
                Audio.Play();

                timeSinceSummon = 0f;
                GameObject moose1 = Instantiate(MooseMinion1);
                moose1.transform.position = new Vector3(20, 1, 20);
                GameObject moose2 = Instantiate(MooseMinion2);
                moose2.transform.position = new Vector3(-20, 1, -20);
            }
            else if(timeSinceSummon >= 2f)
            {
                if(prevStage == 2)
                    runPhase1();
            }

            timeSinceSummon += Time.deltaTime;
        }

        private void runPhase3()
        {
            if (prevStage == 2)
            {
                Audio.clip = MoanSound;
                Audio.Play();
                prevStage = 3;
                Agent.SetDestination(new Vector3(25, 0, 25));
            }

            if(Agent.remainingDistance <= 3f)
            {
                switch (currentLocation)
                {
                    case 0:
                        Agent.SetDestination(new Vector3(-25, 0, 25));
                        break;
                    case 1:
                        Agent.SetDestination(new Vector3(-25, 0, -25));
                        break;
                    case 2:
                        Agent.SetDestination(new Vector3(25, 0, -25));
                        break;
                    case 3:
                        Agent.SetDestination(new Vector3(25, 0, 25));
                        currentLocation = -1;
                        break;
                }
                currentLocation++;
            }

            if(timeSinceShoot >= ShootWait)
            {
                timeSinceShoot = 0f;

                Vector3 PlayerPosition = PlayerMovement.main.transform.position;
                Vector3 translation = PlayerPosition - transform.position;
                translation.y = 0;
                Quaternion rotation = Quaternion.LookRotation(translation);

                GameObject pro1 = Instantiate(MooseProjectile);
                pro1.transform.position = transform.position;
                pro1.transform.rotation = rotation;

                GameObject pro2 = Instantiate(MooseProjectile);
                pro2.transform.position = transform.position;
                pro2.transform.rotation = Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y - 12f, 0));

                GameObject pro3 = Instantiate(MooseProjectile);
                pro3.transform.position = transform.position;
                pro3.transform.rotation = Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y + 12f, 0));

                GameObject pro4 = Instantiate(MooseProjectile);
                pro4.transform.position = transform.position;
                pro4.transform.rotation = Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y - 24f, 0));

                GameObject pro5 = Instantiate(MooseProjectile);
                pro5.transform.position = transform.position;
                pro5.transform.rotation = Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y + 24f, 0));
            }

            runPhase2();
            timeSinceSummon += Time.deltaTime * 0.4f;
            timeSinceShoot += Time.deltaTime;
        }

        protected override void OnDamage(int damage)
        {
            if (wandering)
            {
                wandering = false;
                Audio.clip = MoanSound;
                Audio.Play();
            }
        }

        protected override void OnDeath()
        {
        }

        protected override void AfterStart()
        {
            HealthBar = Generation.main.GetBossSlider();
            HealthBar.maxValue = MaxHealth;
            timeSinceSummon = SummonWait - 5f;
        }

        private IEnumerator ChargeAttack()
        {
            charging = true;
            Agent.SetDestination(PlayerMovement.main.transform.position);
            timeSinceStartCharge = 0f;
            yield return new WaitUntil(() => Agent.remainingDistance <= 0.1f || timeSinceStartCharge >= ChargeTimer);
            charging = false;
        }
    }
}