using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Grass
{
    public class GrassGeneration : MonoBehaviour
    {
        public GameObject Grass;
        public int AmountOfGrass = 300;
        private void Start()
        {
            Generate();
        }
        private void Generate()
        {
            for (int i = 0; i < 300; i++)
            {
                GameObject o = Instantiate(Grass, transform);
                o.transform.position = new Vector3(Random.Range((int)-AnimalManager.main.MaxWanderRange * 10, (int)AnimalManager.main.MaxWanderRange * 10) / 10f, 1f, Random.Range((int)-AnimalManager.main.MaxWanderRange * 10, (int)AnimalManager.main.MaxWanderRange * 10) / 10f);
            }
        }
    }
}