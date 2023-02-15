using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Contracts
{
    public class Contract
    {
        public List<Objective> Objectives { get; protected set; } = new List<Objective>();
        public int TimeLimit { get; protected set; }
        public int GetTotalReward()
        {
            int reward = 0;
            foreach(Objective o in Objectives)
            {
                reward += o.Reward;
            }
            return reward;
        }
        public static Contract GenerateRandomContract()
        {
            Contract contract = new Contract();

            if(Random.Range(0, 2) == 1)
            {
                contract.TimeLimit = 0;
            }
            else
            {
                contract.TimeLimit = 300;
            }

            int numOfObjectives = Random.Range(1, 4);

            return contract;
        }
    }
}