using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTE.Trees
{
    public class Generation : MonoBehaviour
    {
        public static Generation main;
        private void Awake()
        {
            main = this;
        }
        public Slider GetBossSlider()
        {
            return BossHealthBar.GetComponent<Slider>();
        }

        [SerializeField] private GameObject[] Trees;

        [SerializeField] private GameObject Fox;
        [SerializeField] private GameObject Moose;
        [SerializeField] private GameObject Bear;
        [SerializeField] private GameObject Goose;
        [SerializeField] private GameObject[] Rabbits;

        [SerializeField] private GameObject GigaMoose;
        [SerializeField] private GameObject Bryce;
        [SerializeField] private GameObject Dog;

        [SerializeField] private GameObject BossHealthBar;

        [SerializeField] [Range(0, 1)] private float TreeSpawnProbability = 0.02f;
        [SerializeField] [Range(0, 1)] private float FoxSpawnProbability = 0.005f;
        [SerializeField] [Range(0, 1)] private float BearSpawnProbability = 0.004f;
        [SerializeField] [Range(0, 1)] private float RabbitSpawnProbability = 0.004f;
        [SerializeField] [Range(0, 1)] private float GooseSpawnProbability = 0.004f;

        private bool[,] OpenSpot;

        private void Start()
        {
            Generate(300, 15, 8, 15, 15);
        }
        private void Generate(int treeMin, int foxMin, int bearMin, int rabbitMin, int gooseMin)
        {
            OpenSpot = new bool[(int)AnimalManager.main.MaxWanderRange * 2 + 1, (int)AnimalManager.main.MaxWanderRange * 2 + 1];
            for (int x = 0; x < (int)AnimalManager.main.MaxWanderRange * 2 + 1; x++)
            {
                for (int z = 0; z < (int)AnimalManager.main.MaxWanderRange * 2 + 1; z++)
                {
                    OpenSpot[x, z] = true;
                }
            }

            for (int x = -1; x < 2; x++)
            {
                for (int z = -1; z < 2; z++)
                {
                    OpenSpot[(int)AnimalManager.main.MaxWanderRange + x, (int)AnimalManager.main.MaxWanderRange + z] = false;
                }
            }

            if (MainGameManager.GigaMooseRemaining)
            {
                GenerateGigaMoose();
                OpenSpot[(int)AnimalManager.main.MaxWanderRange, (int)AnimalManager.main.MaxWanderRange - 30] = false;
            }
            else if (MainGameManager.BryceRemaining)
            {
                GenerateBryce();
                OpenSpot[(int)AnimalManager.main.MaxWanderRange, (int)AnimalManager.main.MaxWanderRange + 30] = false;
                OpenSpot[(int)AnimalManager.main.MaxWanderRange, (int)AnimalManager.main.MaxWanderRange + 27] = false;
            }

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

            if (MainGameManager.GigaMooseRemaining || MainGameManager.BryceRemaining)
            {
                BossHealthBar.SetActive(true);
                return;
            }
            BossHealthBar.SetActive(false);

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

            int rabbitCount = 0;
            do
            {
                for (int x = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x++)
                {
                    for (int z = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z++)
                    {
                        if (OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange])
                            if(RollSpawn(x, z, RabbitSpawnProbability, Rabbits[Random.Range(0, 2)]))
                            {
                                OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange] = false;
                                rabbitCount++;
                            }
                    }
                }
            } while (rabbitCount < rabbitMin);

            int gooseCount = 0;
            do
            {
                for (int x = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x++)
                {
                    for (int z = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z++)
                    {
                        if (OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange])
                            if(RollSpawn(x, z, GooseSpawnProbability, Goose))
                            {
                                OpenSpot[x + (int)AnimalManager.main.MaxWanderRange, z + (int)AnimalManager.main.MaxWanderRange] = false;
                                gooseCount++;
                            }
                    }
                }
            } while (gooseCount < gooseMin);

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

        private void GenerateBryce()
        {
            SpawnObject(0, 30, Bryce);
            SpawnObject(0, 27, Dog);
        }

        private void GenerateGigaMoose()
        {
            SpawnObject(0, -30, GigaMoose);
        }
    }
}