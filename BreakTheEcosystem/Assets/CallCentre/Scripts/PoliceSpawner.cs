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

        public int MaxPolice = 25;
        public float SpawnTime = 20f;
        public GameObject Police;

        private int PoliceAlive = 0;
        private float spawnTimer = 45f;

        private void Update()
        {
            if(spawnTimer <= 0f && PoliceAlive < MaxPolice)
            {
                spawnTimer = SpawnTime;
                SpawnPolice(new Vector3(2f, 1f, -5f));
                SpawnPolice(new Vector3(-2f, 1f, -5f));
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