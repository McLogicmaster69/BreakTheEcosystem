using BTE.Animals;
using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Contracts
{
    public class Contract
    {
        public List<Objective> Objectives { get; protected set; } = new List<Objective>();
        public int TimeLimit { get; protected set; }
        public int Reward { get; protected set; }
        protected int GetTotalReward()
        {
            int reward = 0;
            foreach(Objective o in Objectives)
            {
                reward += o.Reward;
            }
            return Mathf.FloorToInt(reward * DifficultyManager.MoneyMultiplier * (TimeLimit == 0 ? 1 : 2));
        }
        public static Contract GenerateRandomContract()
        {
            Contract contract = new Contract();

            if(Random.Range(0, 2) == 1)
                contract.TimeLimit = 0;
            else
                contract.TimeLimit = 300;

            if (Random.Range(0, 4) == 0)
            {
                switch(Random.Range(0, 2))
                {
                    case 0:
                        contract.Objectives.Add(new GigaMooseObjective());
                        break;
                    case 1:
                        contract.Objectives.Add(new BryceObjective());
                        break;
                }
            }
            else
            {
                List<Objective> pool = new List<Objective>();
                pool.Add(new TreeObjective(Random.Range(2, 6) * 10));
                pool.Add(new SlaughterObjective(Random.Range(15, 30)));
                pool.Add(new MooseObjective());
                switch (Random.Range(0, 4))
                {
                    case 0:
                        pool.Add(new TargetObjective(AnimalType.Rabbits, Random.Range(5, 10)));
                        break;
                    case 1:
                        pool.Add(new TargetObjective(AnimalType.Bears, Random.Range(3, 5)));
                        break;
                    case 2:
                        pool.Add(new TargetObjective(AnimalType.Geese, Random.Range(5, 10)));
                        break;
                    case 3:
                        pool.Add(new TargetObjective(AnimalType.Foxes, Random.Range(3, 10)));
                        break;
                }

                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    int selected = Random.Range(0, pool.Count);
                    contract.Objectives.Add(pool[selected]);
                    pool.RemoveAt(selected);
                }

                contract.Reward = contract.GetTotalReward();
            }

            return contract;
        }
    }
}