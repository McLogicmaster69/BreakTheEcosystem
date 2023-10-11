using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Music
{
    public class AdaptiveMusicManager : MonoBehaviour
    {
        public bool Adapt { get; set; } = false;

        [SerializeField] private float TimeToAdjust = 1f;
        [SerializeField] private float InstantTimeToAdjust = 0.1f;
        [SerializeField] private AdaptiveSource[] sources;

        private float adaptedValue = 0f;
        private float valueToAdaptTo = 0f;

        private void Update()
        {
            if(adaptedValue < valueToAdaptTo)
            {
                adaptedValue += Time.deltaTime / InstantTimeToAdjust;
                if(adaptedValue >= valueToAdaptTo)
                {
                    adaptedValue = valueToAdaptTo;
                    valueToAdaptTo = 0f;
                }
            }
            else
            {
                if (Adapt)
                    adaptedValue += Time.deltaTime / TimeToAdjust;
                else
                    adaptedValue -= Time.deltaTime / TimeToAdjust;
            }

            adaptedValue = Mathf.Clamp(adaptedValue, 0f, 1f);
            foreach(AdaptiveSource source in sources)
            {
                source.Source.volume = source.BaseVolume + source.Change * adaptedValue;
            }
        }

        public void InstantChange(float value)
        {
            valueToAdaptTo = value;
            valueToAdaptTo = Mathf.Clamp(valueToAdaptTo, 0f, 1f);
        }

        public void InstantIncrease(float value)
        {
            valueToAdaptTo += value;
            valueToAdaptTo = Mathf.Clamp(valueToAdaptTo, 0f, 1f);
        }
    }
}