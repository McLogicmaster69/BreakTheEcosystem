using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.CallCentre
{
    public class PoliceSpawner : MonoBehaviour
    {
        public static PoliceSpawner main;
        private void Awake()
        {
            main = this;
        }

        public GameObject Police;

        private int PoliceAlive = 0;
        private float spawnTimer = 45f;

        private void Update()
        {
            if(spawnTimer <= 0f && PoliceAlive < DifficultyManager.MaxPolice)
            {
                spawnTimer = DifficultyManager.PoliceSpawnTimes;
                switch (DifficultyManager.PoliceSpawnGroups)
                {
                    case 2:
                        SpawnPolice(new Vector3(2f, 1f, -5f));
                        SpawnPolice(new Vector3(-2f, 1f, -5f));
                        break;
                    case 3:
                        SpawnPolice(new Vector3(4f, 1f, -5f));
                        SpawnPolice(new Vector3(0f, 1f, -5f));
                        SpawnPolice(new Vector3(-4f, 1f, -5f));
                        break;
                    case 4:
                        SpawnPolice(new Vector3(2f, 1f, -5f));
                        SpawnPolice(new Vector3(6f, 1f, -5f));
                        SpawnPolice(new Vector3(-2f, 1f, -5f));
                        SpawnPolice(new Vector3(-6f, 1f, -5f));
                        break;
                }
            }
            spawnTimer -= Time.deltaTime;
        }
        private void SpawnPolice(Vector3 position)
        {
            GameObject o = Instantiate(Police, transform);
            o.transform.position = position;
            PoliceAlive++;
        }
        public void KillPolice()
        {
            PoliceAlive--;
        }
    }
}