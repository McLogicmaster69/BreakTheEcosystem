using BTE.Animals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Contracts
{
    public class TargetObjective : Objective
    {
        public AnimalType Animal { get; private set; }
        public int Number { get; private set; }
        public TargetObjective(AnimalType animal, int number) : base(ObjectiveType.Target)
        {
            Animal = animal;
            Number = number;
        }
    }
}