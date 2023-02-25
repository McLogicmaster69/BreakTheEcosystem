using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.Missions
{
    public class KillGoal : Goal
    {
        public int Amount { get; private set; }
        public KillGoal(int amount) : base(GoalType.Kill, amount * 10)
        {
            Amount = amount;
        }
    }
}