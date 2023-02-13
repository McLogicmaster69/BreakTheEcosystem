using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Managers
{
    public class AnimalManager : MonoBehaviour
    {
        public static AnimalManager main;
        private void Awake()
        {
            main = this;
        }

        [SerializeField] private float _MaxWanderRange = 50f;
        public float MaxWanderRange { get { return _MaxWanderRange; } }
    }
}