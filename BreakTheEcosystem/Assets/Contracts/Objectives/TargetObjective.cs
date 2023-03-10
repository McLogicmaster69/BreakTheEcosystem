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
        public TargetObjective(AnimalType animal, int number) : base(ObjectiveType.Target, GetTargetValue(animal) * number)
        {
            Animal = animal;
            Number = number;
        }
        private static int GetTargetValue(AnimalType type)
        {
            switch (type)
            {
                case AnimalType.Rabbits:
                    return 15;
                case AnimalType.Bears:
                    return 40;
                case AnimalType.Geese:
                    return 25;
                case AnimalType.Foxes:
                    return 25;
            }
            return 0;
        }
    }
}