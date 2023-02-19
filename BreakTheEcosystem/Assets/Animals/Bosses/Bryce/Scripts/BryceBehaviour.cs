using BTE.Player;
using BTE.Trees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTE.Animals
{
    public class BryceBehaviour : AnimalBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioSource PainSound;
        [SerializeField] private AudioClip[] PainSounds;
        [SerializeField] private AudioSource VoicelinesAudio;
        [SerializeField] private AudioClip[] StartSounds;
        [SerializeField] private AudioClip[] BoscoSounds;
        [SerializeField] private AudioClip[] ShotgunSounds;
        [SerializeField] private AudioSource ShotgunAudio;
        [Header("Stats")]
        [Header("Pursue")]
        public float PursueStoppingDistance = 5f;
        public float PursueStartingDistance = 12f;
        [Header("Chase")]
        public float TimeToChase = 8f;
        public float TimeChasing = 6f;
        [Header("Shotgun")]
        public float ShotgunCooldown = 7f;
        public float AimTime = 1.5f;
        public float ShootTime = 1f;
        public int Spread = 1;
        public int Bullets = 8;
        public GameObject Bullet;
        public GameObject Shotgun;
        public GameObject BulletSpawn;

        private float timeChasing = 0f;
        private float timeSinceChasing = -5f;
        private bool chasing = false;
        private bool pursueing = false;

        private int shotgunState = 0;
        private float timeSinceShot = 0f;
        private float timeSinceAim = 0f;
        private float timeSinceWait = 0f;
        private float timeSinceActualShot = 0f;

        private DogBehaviour DogBehaviour;
        private Slider HealthBar;

        public BryceBehaviour() : base(AnimalType.Bryce) { }

        protected override void runAliveBehaviour()
        {
            AttackObject.SetActive(true);
            HealthBar.value = Health;
            if (Alive)
            {
                runUpdateStats();
                runAttackProgram();
            }
        }

        private void runAttackProgram()
        {
            if (Health >= MaxHealth * 0.75f)
                runPhase1();
            else if (Health >= MaxHealth * 0.45f)
                runPhase2();
            else
                runPhase3();
        }

        private void runPhase1()
        {
            PursuePlayer();
            DogChase(0);
        }

        private void runPhase2()
        {
            if (shotgunState == 0)
                PursuePlayer();
            DogChase(2);
            ShootShotgun(ShotgunCooldown, ShootTime, Spread);
        }

        private void runPhase3()
        {
            if(shotgunState == 0)
                PursuePlayer();
            DogChase(2);
            ShootShotgun(ShotgunCooldown / 2, ShootTime / 1.5f, Spread * 3);
        }

        private void PursuePlayer()
        {
            if(Vector3.Distance(transform.position, PlayerMovement.main.transform.position) >= PursueStartingDistance)
            {
                Agent.isStopped = false;
                Agent.SetDestination(PlayerMovement.main.transform.position);
                pursueing = true;
            }
            else if (pursueing)
            {
                Agent.SetDestination(PlayerMovement.main.transform.position);
                if (Agent.remainingDistance <= PursueStoppingDistance)
                {
                    Agent.isStopped = true;
                    pursueing = false;
                }
            }
            else
            {
                TrackRotation();
            }
        }

        private void TrackRotation()
        {
            Vector3 PlayerPosition = PlayerMovement.main.transform.position;
            Vector3 translation = PlayerPosition - transform.position;
            translation.y = 0;
            Quaternion rotation = Quaternion.LookRotation(translation);
            transform.rotation = rotation;
        }

        private void DogChase(int nextState)
        {
            if (chasing)
            {
                timeChasing += Time.deltaTime;
                if(timeChasing >= TimeChasing)
                {
                    chasing = false;
                    timeSinceChasing = 0f;
                    DogBehaviour.AttackState = nextState;
                }
            }
            else
            {
                timeSinceChasing += Time.deltaTime;
                if (timeSinceChasing >= TimeToChase) 
                {
                    VoicelinesAudio.clip = BoscoSounds[Random.Range(0, BoscoSounds.Length)];
                    VoicelinesAudio.Play();

                    chasing = true;
                    timeChasing = 0f;
                    DogBehaviour.AttackState = 1;
                }
            }
        }

        private void ShootShotgun(float cooldown, float shotTime, int spread)
        {
            switch (shotgunState)
            {
                case 0:
                    timeSinceShot += Time.deltaTime;
                    if(timeSinceShot >= cooldown)
                    {
                        shotgunState = 1;
                        timeSinceAim = 0f;
                        Shotgun.SetActive(true);

                        VoicelinesAudio.clip = ShotgunSounds[Random.Range(0, ShotgunSounds.Length)];
                        VoicelinesAudio.Play();
                    }
                    break;
                case 1:
                    TrackRotation();
                    timeSinceAim += Time.deltaTime;
                    if(timeSinceAim >= AimTime)
                    {
                        shotgunState = 2;
                        timeSinceWait = 0f;
                    }
                    break;
                case 2:
                    timeSinceWait += Time.deltaTime;
                    if(timeSinceWait >= shotTime)
                    {
                        Shoot(spread);
                        shotgunState = 3;
                        timeSinceActualShot = 0f;
                    }
                    break;
                case 3:
                    timeSinceActualShot += Time.deltaTime;
                    if(timeSinceActualShot >= ShootTime)
                    {
                        Shotgun.SetActive(false);
                        timeSinceShot = 0;
                        shotgunState = 0;
                    }
                    break;
            }
        }

        private void Shoot(int spread)
        {
            ShotgunAudio.Play();
            for (int i = 0; i < Bullets; i++)
            {
                GameObject bullet = Instantiate(Bullet, BulletSpawn.transform);
                bullet.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-spread, spread + 1), Random.Range(-spread, spread + 1), 0));
            }
        }

        protected override void OnDamage(int damage)
        {
            if (!PainSound.isPlaying)
            {
                PainSound.clip = PainSounds[Random.Range(0, PainSounds.Length)];
                PainSound.Play();
            }
        }

        protected override void OnDeath()
        {
        }

        protected override void AfterStart()
        {
            HealthBar = Generation.main.GetBossSlider();
            HealthBar.maxValue = MaxHealth;
            DogBehaviour = FindAnyObjectByType<DogBehaviour>();

            VoicelinesAudio.clip = StartSounds[Random.Range(0, StartSounds.Length)];
            VoicelinesAudio.Play();

            Shotgun.SetActive(false);
        }
    }
}