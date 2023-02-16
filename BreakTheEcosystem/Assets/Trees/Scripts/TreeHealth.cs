using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Trees
{
    public class TreeHealth : MonoBehaviour
    {
        public int Health = 100;
        public ParticleSystem[] Particles;
        private void Awake()
        {
            foreach(ParticleSystem system in Particles)
            {
                system.Stop();
            }
        }
        private void OnParticleCollision(GameObject other)
        {
            Health -= 1;
            if(Health <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
            foreach (ParticleSystem system in Particles)
            {
                if (!system.isPlaying)
                    system.Play();
                else
                    break;
            }
        }
    }
}