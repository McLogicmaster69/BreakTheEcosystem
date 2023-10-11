using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Music
{
    [System.Serializable]
    public class AdaptiveSource
    {
        public AudioSource Source;
        public float BaseVolume = 0f;
        public float Change = 1f;
    }
}