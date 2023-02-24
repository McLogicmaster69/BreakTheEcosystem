using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.Managers
{
    public class CallCentreManager : MonoBehaviour
    {
        public static CallCentreManager main { get; private set; }
        private void Awake()
        {
            main = this;
        }

        [Header("Size")]
        public float LowerBoundX = -10f;
        public float UpperBoundX = 10f;
        public float LowerBoundZ = -10f;
        public float UpperBoundZ = 10f;
    }
}