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
        public static float AttackMultiplier { get; private set; } = 0.75f;
        public static float HealthMultiplier { get; private set; } = 0.8f;
        public static float SpeedMultiplier { get; private set; } = 0.85f;
        public static float RegenTime { get; private set; } = 6f;
        public static float TimePerHealth { get; private set; } = 0.1f;

        private static float EasyMultiplier { get; } = 1f;
        private static float HardMultiplier { get; } = 1.75f;
        private static float ExpertMultiplier { get; } = 3.25f;

        private static float EasyAttack { get; } = 0.75f;
        private static float HardAttack { get; } = 1.25f;
        private static float ExpertAttack { get; } = 1.75f;

        private static float EasyHealth { get; } = 0.8f;
        private static float HardHealth { get; } = 1.4f;
        private static float ExpertHealth { get; } = 1.65f;

        private static float EasySpeed { get; } = 0.85f;
        private static float HardSpeed { get; } = 1f;
        private static float ExpertSpeed { get; } = 1.1f;

        private static float EasyRegenTime { get; } = 2f;
        private static float HardRegenTime { get; } = 3f;
        private static float ExpertRegenTime { get; } = 4f;

        private static float EasyTimePerHealth { get; } = 0.22f;
        private static float HardTimePerHealth { get; } = 0.4f;
        private static float ExpertTimePerHealth { get; } = 0.47f;

        public static void SetDifficulty(Difficulty difficulty)
        {
            Difficulty = difficulty;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    MoneyMultiplier = EasyMultiplier;
                    AttackMultiplier = EasyAttack;
                    HealthMultiplier = EasyHealth;
                    SpeedMultiplier = EasySpeed;
                    RegenTime = EasyRegenTime;
                    TimePerHealth = EasyTimePerHealth;
                    break;
                case Difficulty.Hard:
                    MoneyMultiplier = HardMultiplier;
                    AttackMultiplier = HardAttack;
                    HealthMultiplier = HardHealth;
                    SpeedMultiplier = HardSpeed;
                    RegenTime = HardRegenTime;
                    TimePerHealth = HardTimePerHealth;
                    break;
                case Difficulty.Expert:
                    MoneyMultiplier = ExpertMultiplier;
                    AttackMultiplier = ExpertAttack;
                    HealthMultiplier = ExpertHealth;
                    SpeedMultiplier = ExpertSpeed;
                    RegenTime = ExpertRegenTime;
                    TimePerHealth = ExpertTimePerHealth;
                    break;
            }
        }
    }
}