using BTE.Managers;
using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BTE.Animals
{
    public enum AnimalType
    {
        Rabbits,
        Bears,
        Geese,
        Foxes,

        Moose,
        GigaMoose,
        Dogs,
        Bryce,

        None
    }
    public enum AnimalState
    {
        Wander,
        Attack,
        Flee
    }
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AnimalBehaviour : MonoBehaviour
    {
        public AnimalType Type { get; private set; }

        [Header("Stats")]
        public int Health = 10;
        public int Damage = 2;
        public float FleePercent = 0.4f;
        [Header("Speed")]
        public float BaseSpeed = 4;
        public float FleeSpeed = 7;
        public float WanderTime = 10;
        public bool Alive { get; private set; } = true;

        protected NavMeshAgent Agent;
        protected AnimalState State = AnimalState.Wander;

        private float wanderTimer;

        public AnimalBehaviour(AnimalType type)
        {
            Type = type;
        }

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {

            // If the animal is alive, it will run its behaviours

            if (Alive)
            {
                runUpdateStats();
                runMovement();

                if (State == AnimalState.Attack)
                    runAttack();
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                TakeDamage(1);
            }
        }

        // Base behaviour for each action

        private void runMovement()
        {
            if (State == AnimalState.Wander)
                runWander();
            if (State == AnimalState.Attack)
                runChase();
            if (State == AnimalState.Flee)
                runFlee();
        }
        private void runAttack()
        {
            Attack();
        }
        private void runFlee()
        {
        }
        private void runDamage(int damage)
        {
            OnDamage(damage);
        }
        private void runDeath()
        {
            Alive = false;
            MainGameManager.AnimalKilled(Type);
            OnDeath();
        }
        private void runWander()
        {
            wanderTimer -= Time.deltaTime;
            if(wanderTimer <= 0f)
            {
                wanderTimer = WanderTime;
                float wanderX = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
                float wanderZ = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
                Agent.SetDestination(new Vector3(wanderX, 1f, wanderZ));
            }
        }
        private void runChase()
        {
            Agent.SetDestination(PlayerMovement.main.transform.position);
        }
        private void runUpdateStats()
        {
            if (State == AnimalState.Flee)
                Agent.speed = FleeSpeed;
            else
                Agent.speed = BaseSpeed;
        }

        // Methods for subclasses to inherit

        protected abstract void Attack();
        protected abstract void OnDamage(int damage);
        protected abstract void OnDeath();

        // Other

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                runDeath();
            else
                runDamage(damage);
        }
    }
}