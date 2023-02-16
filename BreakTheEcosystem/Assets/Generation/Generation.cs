using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Trees
{
    public class Generation : MonoBehaviour
    {
        [SerializeField] private GameObject[] Trees;

        [SerializeField] private GameObject Fox;
        [SerializeField] private GameObject Moose;
        [SerializeField] private GameObject Bear;

        [SerializeField] [Range(0, 1)] private float TreeSpawnProbability = 0.02f;
        [SerializeField] [Range(0, 1)] private float FoxSpawnProbability = 0.005f;
        [SerializeField] [Range(0, 1)] private float BearSpawnProbability = 0.004f;

        private bool[,] OpenSpot;

        private void Start()
        {
            Generate(100, 15, 8);
        }
        private void Generate(int treeMin, int foxMin, int bearMin)
        {
            OpenSpot = new bool[(int)AnimalManager.main.MaxWanderRange * 2 + 1, (int)AnimalManager.main.MaxWanderRange * 2 + 1];
            for (int x = 0; x < (int)AnimalManager.main.MaxWanderRange * 2 + 1; x++)
            {
                for (int z = 0; z < (int)AnimalManager.main.MaxWanderRange * 2 + 1; z++)
                {
                    OpenSpot[x, z] = true;
                }
            }
            OpenSpot[(int)AnimalManager.main.MaxWanderRange, (int)AnimalManager.main.MaxWanderRange] = false;

            int treeCount = 0;
            do
            {
                for (int x = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x++)
                {
                    for (int z = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z++)
                    {
                        if (OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange])
                            if (RollSpawn(x, z, TreeSpawnProbability, Trees[Random.Range(0, Trees.Length)]))
                            {
                                OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange] = false;
                                treeCount++;
                            }
                    }
                }
            }
            while (treeCount < treeMin);

            int foxCount = 0;
            do
            {
                for (int x = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x++)
                {
                    for (int z = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z++)
                    {
                        if (OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange])
                            if(RollSpawn(x, z, FoxSpawnProbability, Fox))
                            {
                                OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange] = false;
                                foxCount++;
                            }
                    }
                }
            } while (foxCount < foxMin);

            int bearCount = 0;
            do
            {
                for (int x = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x++)
                {
                    for (int z = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z++)
                    {
                        if (OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange])
                            if(RollSpawn(x, z, BearSpawnProbability, Bear))
                            {
                                OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange] = false;
                                bearCount++;
                            }
                    }
                }
            } while (bearCount < bearMin);

            bool mooseBreak = false;
            while (true)
            {
                for (int x = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x++)
                {
                    for (int z = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z++)
                    {
                        if (OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange])
                        {
                            OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange] = false;
                            SpawnObject(x, z, Moose);
                            mooseBreak = true;
                            break;
                        }
                    }
                    if (mooseBreak)
                        break;
                }
                if (mooseBreak)
                    break;
            }
        }
        private bool RollSpawn(int x, int z, float prob, GameObject obj)
        {
            int spawnSeed = Random.Range(0, 10000);
            if (spawnSeed < prob * 10000)
            {
                SpawnObject(x, z, obj);
                return true;
            }
            return false;
        }
        private void SpawnObject(int x, int z, GameObject obj)
        {
            GameObject o = Instantiate(obj, this.gameObject.transform);
            o.transform.position = new Vector3(x, 1, z);
        }
    }
}