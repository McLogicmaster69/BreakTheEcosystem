using BTE.BDLC.CallCentre;
using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.People
{
    public class PoliceBehaviour : PersonBehaviour
    {
        public PoliceBehaviour() : base(PersonType.Police) { }

        [Header("Tracking")]
        [SerializeField] private LayerMask Walls;
        [Header("Audio")]
        [SerializeField] private AudioSource ShotgunAudio;
        [Header("Objects")]
        [SerializeField] private GameObject Bullet;
        [SerializeField] private GameObject Shotgun;
        [SerializeField] private GameObject BulletSpawn;
        [SerializeField] private GameObject AttackObject;
        [Header("Shotgun")]
        [SerializeField] private float ShotgunCooldown = 5f;
        [SerializeField] private float AimTime = 0.7f;
        [SerializeField] private float ShootTime = 0.2f;
        [SerializeField] private int Spread = 1;
        [SerializeField] private int Bullets = 8;

        private float timer = 0f;

        private int shotgunState = 0;
        private float timeSinceShot = 0f;
        private float timeSinceAim = 0f;
        private float timeSinceWait = 0f;
        private float timeSinceActualShot = 0f;

        protected override void BeforeStart()
        {
            Agent.speed += Random.Range(-5, 6) / 2f;
        }
        protected override void RunBehaviour()
        {
            if (CanSeePlayer() || shotgunState != 0)
            {
                if(Vector3.Distance(transform.position, PlayerMovement.main.transform.position) <= 2.5f && shotgunState == 0)
                {
                    if(timer <= 0f)
                    {
                        timer = 5f;
                        Agent.isStopped = false;
                        Wander();
                    }
                }
                else
                {
                    timer = 0f;
                    Agent.isStopped = true;
                    ShootShotgun(ShotgunCooldown, ShootTime, Spread);
                }
            }
            else
                TrackPlayer();
        }
        private void TrackPlayer()
        {
            Agent.isStopped = false;
            Agent.SetDestination(PlayerMovement.main.transform.position);
        }
        private bool CanSeePlayer()
        {
            Vector3 directionToPlayer = (PlayerMovement.main.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, PlayerMovement.main.transform.position);
            return !Physics.Raycast(transform.position, directionToPlayer, distance, Walls);
        }
        private void FacePlayer()
        {
            Vector3 PlayerPosition = PlayerMovement.main.transform.position;
            Vector3 translation = PlayerPosition - transform.position;
            translation.y = 0;
            Quaternion rotation = Quaternion.LookRotation(translation);
            transform.rotation = rotation;
        }
        protected override void Die()
        {
            base.Die();
            Shotgun.SetActive(false);
            AttackObject.SetActive(false);
            PoliceSpawner.main.KillPolice();
        }

        private void ShootShotgun(float cooldown, float shotTime, int spread)
        {
            switch (shotgunState)
            {
                case 0:
                    FacePlayer();
                    timeSinceShot += Time.deltaTime;
                    if (timeSinceShot >= cooldown)
                    {
                        shotgunState = 1;
                        timeSinceAim = 0f;
                    }
                    break;
                case 1:
                    if(CanSeePlayer())
                        FacePlayer();
                    timeSinceAim += Time.deltaTime;
                    if (timeSinceAim >= AimTime)
                    {
                        shotgunState = 2;
                        timeSinceWait = 0f;
                    }
                    break;
                case 2:
                    timeSinceWait += Time.deltaTime;
                    if (timeSinceWait >= shotTime)
                    {
                        Shoot(spread);
                        shotgunState = 3;
                        timeSinceActualShot = 0f;
                    }
                    break;
                case 3:
                    FacePlayer();
                    timeSinceActualShot += Time.deltaTime;
                    if (timeSinceActualShot >= ShootTime)
                    {
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
                bullet.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-spread, spread + 1) / 2f, Random.Range(-spread, spread + 1) / 2f, 0));
            }
        }
        protected override void UpdateStats()
        {
        }
    }
}