using BTE.BDLC.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BTE.BDLC.People
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class PersonBehaviour : MonoBehaviour
    {
        protected NavMeshAgent Agent;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            Agent.enabled = true;
        }
        private void Update()
        {
            RunBehaviour();
        }

        protected virtual void Wander()
        {
            float randX = Random.Range((int)CallCentreManager.main.LowerBoundX, (int)CallCentreManager.main.UpperBoundX);
            float randZ = Random.Range((int)CallCentreManager.main.LowerBoundZ, (int)CallCentreManager.main.UpperBoundZ);
            Agent.SetDestination(new Vector3(randX, 1f, randZ));
        }

        protected abstract void RunBehaviour();
    }
}