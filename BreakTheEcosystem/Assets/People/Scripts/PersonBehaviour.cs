using BTE.BDLC.Managers;
using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BTE.BDLC.People
{
    public enum PersonType
    {
        Bystanders,
        Police,
        Boss
    }
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class PersonBehaviour : MonoBehaviour
    {
        [Header("Stats")]
        public float Health = 10;

        [Header("Animator")]
        public Animator Animator;

        [Header("Audio")]
        public AudioSource DeathSound;

        protected NavMeshAgent Agent;

        protected PersonType Type { get; }
        protected bool Alive = true;

        public PersonBehaviour(PersonType type)
        {
            Type = type;
        }

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            BeforeStart();
            Agent.enabled = true;
            Agent.speed *= DifficultyManager.SpeedMultiplier;
            Health *= DifficultyManager.HealthMultiplier;
        }
        private void Update()
        {
            if(Alive)
                RunBehaviour();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Bullet") && Alive)
            {
                TakeDamage();
            }
        }

        protected virtual void Wander()
        {
            float randX = Random.Range((int)CallCentreManager.main.LowerBoundX, (int)CallCentreManager.main.UpperBoundX);
            float randZ = Random.Range((int)CallCentreManager.main.LowerBoundZ, (int)CallCentreManager.main.UpperBoundZ);
            Agent.SetDestination(new Vector3(randX, 1f, randZ));
        }
        protected virtual void TakeDamage()
        {
            Health--;
            if (Health <= 0)
                Die();
        }
        protected virtual void Die()
        {
            Alive = false;
            Animator.SetBool("Alive", false);
            Agent.enabled = false;
            DeathSound.Play();
            UpdateStats();
        }
        protected virtual void UpdateStats()
        {
            BDLCGameManager.KillPerson();
        }
        protected virtual void BeforeStart()
        {
        }

        protected abstract void RunBehaviour();
    }
}