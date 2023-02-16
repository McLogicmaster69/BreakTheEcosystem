using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Contracts
{
    public class SlaughterObjective : Objective
    {
        public int Number { get; private set; }
        public SlaughterObjective(int number) : base(ObjectiveType.Slaughter, number * 10)
        {
            Number = number;
        }
    }
}