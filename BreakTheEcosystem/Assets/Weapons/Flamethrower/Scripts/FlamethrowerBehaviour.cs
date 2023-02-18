using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class FlamethrowerBehaviour : MonoBehaviour
    {
        public AudioSource Sound;
        public ParticleSystem[] Particles;
        private void OnEnable()
        {
            Sound.Stop();
            foreach (ParticleSystem system in Particles)
            {
                system.Stop();
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (!Sound.isPlaying)
                    Sound.Play();
                foreach(ParticleSystem system in Particles)
                {
                    if (system.isEmitting)
                        break;
                    system.Play();
                }
            }
            else
            {
                if (Sound.isPlaying)
                    Sound.Stop();
                foreach (ParticleSystem system in Particles)
                {
                    if (!system.isEmitting)
                        break;
                    system.Stop();
                }
            }
        }
    }
}