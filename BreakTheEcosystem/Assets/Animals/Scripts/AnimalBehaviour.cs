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
        public float AttackSpeedMultiplier = 2.5f;
        public float RetreatTimer = 1.5f;
        [Header("Other")]
        public Animator Animator;
        public AnimalState State = AnimalState.Wander;
        [Header("Audio")]
        public AudioSource DeathSound;

        public bool Alive { get; private set; } = true;

        protected NavMeshAgent Agent;
        protected float wanderTimer;
        protected float AttackTimer = 0f;
        protected float TimeAttacking = 0f;
        protected bool Attacking = false;
        protected float MaxHealth;
        protected float retreatTimer = 0f;

        public AnimalBehaviour(AnimalType type)
        {
            Type = type;
        }

        private void Awake()
        {
            BeforeStart();
            AttackObject.SetActive(false);
            Health = Mathf.FloorToInt(Health * DifficultyManager.HealthMultiplier);
            Damage = Mathf.FloorToInt(Damage * DifficultyManager.AttackMultiplier);
            MaxHealth = Health;
            Agent = GetComponent<NavMeshAgent>();
            AfterStart();
        }
        private void Update()
        {
            // If the animal is alive, it will run its behaviours
            runAliveBehaviour();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                TakeDamage(1);
            }
        }

        // Base behaviour for each action
        // Methods for subclasses to inherit

        public virtual void runAttackSuccess()
        {
            retreatTimer = RetreatTimer;
            TimeAttacking = float.MaxValue;
        }
        protected virtual void runMovement()
        {
            if (State == AnimalState.Wander)
                runWander();
            if (State == AnimalState.Attack)
                Chase();
            if (State == AnimalState.Flee)
                runFlee();
        }
        protected virtual void runFlee()
        {
            if(Agent.remainingDistance < 1f)
            {
                float wanderX = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
                float wanderZ = Random.Range(-AnimalManager.main.MaxWanderRange, AnimalManager.main.MaxWanderRange);
                Agent.SetDestination(new Vector3(wanderX, 1f, wanderZ));
            }
        }
        protected virtual void runDamage(int damage)
        {
            if (Health <= MaxHealth * FleePercent)
                State = AnimalState.Flee;
            else
                State = AnimalState.Attack;
            OnDamage(damage);
        }
        protected virtual void runDeath()
        {
            if (Alive)
            {
                StopAllCoroutines();
                DeathSound.Play();
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
        protected virtual void runWander()
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
        protected virtual void runUpdateStats()
        {
            if (State == AnimalState.Flee)
                Agent.speed = FleeSpeed;
            else if(State == AnimalState.Wander)
                Agent.speed = BaseSpeed;
        }
        protected virtual void runAliveBehaviour()
        {
            if (Alive && Attacking == false)
            {
                runUpdateStats();
                runMovement();
            }
            else if (Attacking)
            {
                SetAttackDestination();
            }

            TimeAttacking += Time.deltaTime;
        }

        protected virtual void Attack()
        {
            Agent.speed = BaseSpeed * AttackSpeedMultiplier;
            StartCoroutine(AttackActive());
        }
        protected virtual void Chase()
        {
            if (retreatTimer <= 0f)
            {
                if (Agent.remainingDistance <= AttackDistance && AttackTimer < 0f)
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
            retreatTimer -= Time.deltaTime;
        }
        protected virtual void SetAttackDestination()
        {
            Agent.SetDestination(PlayerMovement.main.transform.position);
        }
        protected virtual void BeforeStart() { }
        protected virtual void AfterStart() { }

        // Abstract

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