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
        public static int BulletDamage { get; private set; } = 2;
        public static int PoliceSpawnGroups { get; private set; } = 2;
        public static int MaxPolice { get; private set; } = 20;
        public static float PoliceSpawnTimes { get; private set; } = 25f;
        public static float PoliceShootRange { get; private set; } = 20f;
        public static float PoliceShotgunCooldown { get; private set; } = 4.25f;
        public static float PoliceQuickShotTime { get; private set; } = 2f;
        public static float PoliceFreeTime { get; private set; } = 60f;

        private static float EasyMultiplier { get; } = 1f;
        private static float HardMultiplier { get; } = 1.75f;
        private static float ExpertMultiplier { get; } = 3.25f;

        private static float EasyAttack { get; } = 0.75f;
        private static float HardAttack { get; } = 1.25f;
        private static float ExpertAttack { get; } = 1.7f;

        private static float EasyHealth { get; } = 0.8f;
        private static float HardHealth { get; } = 1.4f;
        private static float ExpertHealth { get; } = 1.6f;

        private static float EasySpeed { get; } = 0.85f;
        private static float HardSpeed { get; } = 1f;
        private static float ExpertSpeed { get; } = 1.1f;

        private static float EasyRegenTime { get; } = 2f;
        private static float HardRegenTime { get; } = 3f;
        private static float ExpertRegenTime { get; } = 4.5f;

        private static float EasyTimePerHealth { get; } = 0.25f;
        private static float HardTimePerHealth { get; } = 0.4f;
        private static float ExpertTimePerHealth { get; } = 0.45f;

        private static int EasyBulletDamage { get; } = 2;
        private static int HardBulletDamage { get; } = 3;
        private static int ExpertBulletDamage { get; } = 5;

        private static int EasyPoliceSpawnGroups { get; } = 2;
        private static int HardPoliceSpawnGroups { get; } = 3;
        private static int ExpertPoliceSpawnGroups { get; } = 4;

        private static int EasyMaxPolice { get; } = 20;
        private static int HardMaxPolice { get; } = 25;
        private static int ExpertMaxPolice { get; } = 30;

        private static float EasyPoliceSpawnTimes { get; } = 40f;
        private static float HardPoliceSpawnTimes { get; } = 30f;
        private static float ExpertPoliceSpawnTimes { get; } = 35f;

        private static float EasyPoliceShootingRange { get; } = 20f;
        private static float HardPoliceShootingRange { get; } = 17f;
        private static float ExpertPoliceShootingRange { get; } = 15f;

        private static float EasyPoliceShotgunCooldown { get; } = 5f;
        private static float HardPoliceShotgunCooldown { get; } = 4.75f;
        private static float ExpertPoliceShotgunCooldown { get; } = 3.75f;

        private static float EasyPoliceQuickShotTime { get; } = 2f;
        private static float HardPoliceQuickShotTime { get; } = 1.75f;
        private static float ExpertPoliceQuickShotTime { get; } = 1.5f;

        private static float EasyPoliceFreeTime { get; } = 60f;
        private static float HardPoliceFreeTime { get; } = 55f;
        private static float ExpertPoliceFreeTime { get; } = 47.5f;

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
                    BulletDamage = EasyBulletDamage;
                    PoliceSpawnGroups = EasyPoliceSpawnGroups;
                    MaxPolice = EasyMaxPolice;
                    PoliceSpawnTimes = EasyPoliceSpawnTimes;
                    PoliceShootRange = EasyPoliceShootingRange;
                    PoliceShotgunCooldown = EasyPoliceShotgunCooldown;
                    PoliceQuickShotTime = EasyPoliceQuickShotTime;
                    PoliceFreeTime = EasyPoliceFreeTime;
                    break;
                case Difficulty.Hard:
                    MoneyMultiplier = HardMultiplier;
                    AttackMultiplier = HardAttack;
                    HealthMultiplier = HardHealth;
                    SpeedMultiplier = HardSpeed;
                    RegenTime = HardRegenTime;
                    TimePerHealth = HardTimePerHealth;
                    BulletDamage = HardBulletDamage;
                    PoliceSpawnGroups = HardPoliceSpawnGroups;
                    MaxPolice = HardMaxPolice;
                    PoliceSpawnTimes = HardPoliceSpawnTimes;
                    PoliceShootRange = HardPoliceShootingRange;
                    PoliceShotgunCooldown = HardPoliceShotgunCooldown;
                    PoliceQuickShotTime = HardPoliceQuickShotTime;
                    PoliceFreeTime = HardPoliceFreeTime;
                    break;
                case Difficulty.Expert:
                    MoneyMultiplier = ExpertMultiplier;
                    AttackMultiplier = ExpertAttack;
                    HealthMultiplier = ExpertHealth;
                    SpeedMultiplier = ExpertSpeed;
                    RegenTime = ExpertRegenTime;
                    TimePerHealth = ExpertTimePerHealth;
                    BulletDamage = ExpertBulletDamage;
                    PoliceSpawnGroups = ExpertPoliceSpawnGroups;
                    MaxPolice = ExpertMaxPolice;
                    PoliceSpawnTimes = ExpertPoliceSpawnTimes;
                    PoliceShootRange = ExpertPoliceShootingRange;
                    PoliceShotgunCooldown = ExpertPoliceShotgunCooldown;
                    PoliceQuickShotTime = ExpertPoliceQuickShotTime;
                    PoliceFreeTime = ExpertPoliceFreeTime;
                    break;
            }
        }
    }
}