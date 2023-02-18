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
        [Header("Attack")]
        public GameObject AttackObject;
        public float AttackDistance = 5f;
        public float AttackCooldown = 1f;
        [Header("Other")]
        public Animator Animator;

        public bool Alive { get; private set; } = true;

        protected NavMeshAgent Agent;
        protected AnimalState State = AnimalState.Wander;
        protected float wanderTimer;
        protected float AttackTimer = 0f;
        protected float TimeAttacking = 0f;
        protected bool Attacking = false;

        private float MaxHealth;

        public AnimalBehaviour(AnimalType type)
        {
            Type = type;
        }

        private void Awake()
        {
            AttackObject.SetActive(false);
            Health = Mathf.FloorToInt(Health * DifficultyManager.HealthMultiplier);
            Damage = Mathf.FloorToInt(Damage * DifficultyManager.AttackMultiplier);
            MaxHealth = Health;
            Agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {

            // If the animal is alive, it will run its behaviours

            if (Alive && Attacking == false)
            {
                runUpdateStats();
                runMovement();
            }

            TimeAttacking += Time.deltaTime;
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
                Chase();
            if (State == AnimalState.Flee)
                runFlee();
        }
        private void runFlee()
        {
            if(Agent.remainingDistance < 1f)
            {
                float wanderX = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
                float wanderZ = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
                Agent.SetDestination(new Vector3(wanderX, 1f, wanderZ));
            }
        }
        private void runDamage(int damage)
        {
            if (Health <= MaxHealth * FleePercent)
                State = AnimalState.Flee;
            else
                State = AnimalState.Attack;
            OnDamage(damage);
        }
        private void runDeath()
        {
            if (Alive)
            {
                StopAllCoroutines();
                AttackObject.SetActive(false);
                Attacking = false;
                Animator.SetInteger("State", 2);
                Animator.SetTrigger("Transition");
                Agent.isStopped = true;
                Alive = false;
                MainGameManager.AnimalKilled(Type);
                OnDeath();
            }
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
        private void runUpdateStats()
        {
            if (State == AnimalState.Flee)
                Agent.speed = FleeSpeed;
            else if(State == AnimalState.Wander)
                Agent.speed = BaseSpeed;
        }

        // Methods for subclasses to inherit

        protected virtual void Attack()
        {
            Agent.speed = BaseSpeed * 2;
            StartCoroutine(AttackActive());
        }
        protected virtual void Chase()
        {
            if(Agent.remainingDistance <= AttackDistance && AttackTimer < 0f)
            {
                AttackTimer = AttackCooldown;
                Attack();
            }
            else
            {
                Agent.speed = BaseSpeed;
                Agent.SetDestination(PlayerMovement.main.transform.position);
            }
            AttackTimer -= Time.deltaTime;
        }

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

        protected IEnumerator AttackActive()
        {
            Attacking = true;
            AttackObject.SetActive(true);
            Animator.SetInteger("State", 1);
            Animator.SetTrigger("Transition");
            TimeAttacking = 0f;
            yield return new WaitUntil(() => Agent.remainingDistance < 0.3f || TimeAttacking >= 2f);
            AttackObject.SetActive(false);
            Attacking = false;
            Animator.SetInteger("State", 0);
            Animator.SetTrigger("Transition");
            float wanderX = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
            float wanderZ = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
            Agent.SetDestination(new Vector3(wanderX, 1f, wanderZ));
        }
    }
}