using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.Missions
{
    public enum GoalType
    {
        Kill,
        Plant,
        Boss,
        Money,
        Animals
    }
    public class Goal
    {
        public GoalType Type { get; private set; }
        public int Reward { get; private set; }

        public Goal(GoalType type, int reward)
        {
            Type = type;
            Reward = reward;
        }
    }
}