using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Trees
{
    public class TreeGeneration : MonoBehaviour
    {
        public GameObject Tree;
        [Range(0, 1)] public float SpawnProbability = 0.02f;
        private void Start()
        {
            Generate(100);
        }
        private void Generate(int minimum)
        {
            int treeCount = 0;
            do
            {
                for (int x = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); x++)
                {
                    for (int z = -Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z < Mathf.FloorToInt(AnimalManager.main.MaxWanderRange); z++)
                    {
                        if (RollSpawn(x, z))
                            treeCount++;
                    }
                }
            }
            while (treeCount < minimum);
        }
        private bool RollSpawn(int x, int z)
        {
            int spawnSeed = Random.Range(0, 100);
            if (spawnSeed < SpawnProbability * 100)
            {
                SpawnTree(x, z);
                return true;
            }
            return false;
        }
        private void SpawnTree(int x, int z)
        {
            GameObject o = Instantiate(Tree, this.gameObject.transform);
            Tree.transform.position = new Vector3(x, 1, z);
        }
    }
}