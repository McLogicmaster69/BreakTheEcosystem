using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Trees
{
    public class TreeHealth : MonoBehaviour
    {
        public int Health = 100;
        public ParticleSystem[] Particles;
        private bool onFire = false;
        private float timer = 0f;
        private void Awake()
        {
            foreach(ParticleSystem system in Particles)
            {
                system.Stop();
            }
        }
        private void Update()
        {
            if (onFire)
            {
                timer += Time.deltaTime;
            }
            if(timer >= 0.75f)
            {
                timer -= 0.75f;
                Health--;
            }
            if (Health <= 0)
            {
                MainGameManager.TreeBurnt();
                Destroy(transform.parent.gameObject);
            }
        }
        private void OnParticleCollision(GameObject other)
        {
            onFire = true;
            Health -= 1;
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