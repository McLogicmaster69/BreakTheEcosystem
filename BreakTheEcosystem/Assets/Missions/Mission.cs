using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.Missions
{
    public class Mission
    {
        public List<Goal> Goals { get; protected set; } = new List<Goal>();
        public int TimeLimit { get; protected set; }
        public int Reward { get; protected set; }
        public void UpdateReward()
        {
            Reward = GetTotalReward();
        }
        protected int GetTotalReward()
        {
            int reward = 0;
            foreach (Goal o in Goals)
            {
                reward += o.Reward;
            }
            return Mathf.FloorToInt(reward * DifficultyManager.MoneyMultiplier * (TimeLimit == 0 ? 1f : 1.5f));
        }
        public static Mission GenerateRandomMission()
        {
            Mission contract = new Mission();

            if (Random.Range(0, 2) == 1)
                contract.TimeLimit = 0;
            else
                contract.TimeLimit = 300;

            List<Goal> pool = new List<Goal>();
            pool.Add(new KillGoal(Random.Range(15, 30)));
            pool.Add(new PlantGoal());
            pool.Add(new BossGoal());
            pool.Add(new MoneyGoal());
            pool.Add(new AnimalGoal());

            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                int selected = Random.Range(0, pool.Count);
                contract.Goals.Add(pool[selected]);
                pool.RemoveAt(selected);
            }

            contract.Reward = contract.GetTotalReward();
            return contract;
        }
    }
}