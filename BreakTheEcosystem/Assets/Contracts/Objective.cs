using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Contracts
{
    public enum ObjectiveType
    {
        Tree,
        Target,
        Slaughter,
        Moose,
        GigaMoose,
        Bryce
    }
    public abstract class Objective
    {
        public ObjectiveType Type { get; private set; }
        public int Reward { get; private set; }

        public Objective(ObjectiveType type, int reward)
        {
            Type = type;
            Reward = reward;
        }
    }
}