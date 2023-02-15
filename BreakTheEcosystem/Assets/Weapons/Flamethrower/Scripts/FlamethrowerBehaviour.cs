using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class FlamethrowerBehaviour : MonoBehaviour
    {
        public ParticleSystem[] Particles;
        private void OnEnable()
        {
            foreach (ParticleSystem system in Particles)
            {
                system.Stop();
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                foreach(ParticleSystem system in Particles)
                {
                    if (system.isEmitting)
                        break;
                    system.Play();
                }
            }
            else
            {
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