using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Animals
{
    public class MooseProjectileBehaviour : AnimalBehaviour
    {
        public MooseProjectileBehaviour() : base(AnimalType.Moose) { }

        public float Timer = 10f;

        protected override void runAliveBehaviour()
        {
            AttackObject.SetActive(true);
            transform.position += BaseSpeed * transform.forward * Time.deltaTime;
        }

        protected override void AfterStart()
        {
            StartCoroutine(TimeAlive());
        }

        protected override void OnDamage(int damage)
        {
        }

        protected override void OnDeath()
        {
        }

        private IEnumerator TimeAlive()
        {
            yield return new WaitForSeconds(Timer);
            Destroy(this.gameObject);
        }
    }
}