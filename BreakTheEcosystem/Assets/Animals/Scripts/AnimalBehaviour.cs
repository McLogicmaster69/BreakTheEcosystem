using DTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DTE.Animals
{
    public enum AnimalState
    {
        Wander,
        Attack,
        Flee
    }
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AnimalBehaviour : MonoBehaviour
    {
        public int Health;
        public int Damage;
        public float BaseSpeed;
        public float RunSpeed;
        public float WanderTime;
        public AnimalState State;
        public bool Alive = true;

        protected NavMeshAgent Agent;

        private float wanderTimer;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {

            // If the animal is alive, it will run its behaviours

            if (Alive)
            {
                runMovement();
                runAttack();
                runFlee();
            }
        }

        // Base behaviour for each action

        private void runMovement()
        {
            if (State == AnimalState.Wander)
                runWander();
        }
        private void runAttack()
        {
            if (State == AnimalState.Attack)
                Attack();
        }
        private void runFlee()
        {
            if (State == AnimalState.Flee)
                Flee();
        }
        private void runDamage(int damage)
        {
            OnDamage(damage);
        }
        private void runDeath()
        {
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

        // Methods for subclasses to inherit

        protected abstract void Attack();
        protected abstract void Flee();
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