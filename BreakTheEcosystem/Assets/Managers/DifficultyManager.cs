using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Managers
{
    public enum Difficulty
    {
        Easy,
        Hard,
        Expert
    }
    public static class DifficultyManager
    {
        public static Difficulty Difficulty { get; private set; }
        public static float MoneyMultiplier { get; private set; } = 1f;
        public static float AttackMultiplier { get; private set; } = 1f;
        public static float HealthMultiplier { get; private set; } = 1f;

        private static float EasyMultiplier { get; } = 1f;
        private static float HardMultiplier { get; } = 1.5f;
        private static float ExpertMultiplier { get; } = 2.5f;

        private static float EasyAttack { get; } = 1f;
        private static float HardAttack { get; } = 1.25f;
        private static float ExpertAttack { get; } = 1.8f;

        private static float EasyHealth { get; } = 1f;
        private static float HardHealth { get; } = 1.5f;
        private static float ExpertHealth { get; } = 1.9f;

        public static void SetDifficulty(Difficulty difficulty)
        {
            Difficulty = difficulty;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    MoneyMultiplier = EasyMultiplier;
                    AttackMultiplier = EasyAttack;
                    HealthMultiplier = EasyHealth;
                    break;
                case Difficulty.Hard:
                    MoneyMultiplier = HardMultiplier;
                    AttackMultiplier = HardAttack;
                    HealthMultiplier = HardHealth;
                    break;
                case Difficulty.Expert:
                    MoneyMultiplier = ExpertMultiplier;
                    AttackMultiplier = ExpertAttack;
                    HealthMultiplier = ExpertHealth;
                    break;
            }
        }
    }
}