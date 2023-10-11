using BTE.Music;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class FlamethrowerBehaviour : MonoBehaviour
    {
        [SerializeField] private AudioSource Sound;
        [SerializeField] private ParticleSystem[] Particles;
        [SerializeField] private AdaptiveMusicManager musicManager;

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
                musicManager.Adapt = true;
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
                musicManager.Adapt = false;
            }
        }
    }
}