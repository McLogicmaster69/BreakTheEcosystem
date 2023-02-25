using BTE.BDLC.Missions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Managers
{
    public static class BDLCGameManager
    {
        public static int KillsRemaining { get; private set; } = 0;
        public static int C4Remaining { get; private set; } = 0;
        public static int MoneyRemaining { get; private set; } = 0;
        public static int AnimalsRemaining { get; private set; } = 0;
        public static bool BossRemaining { get; private set; } = false;
        public static int Reward { get; private set; } = 0;
        public static float TimeLimit { get; private set; } = 0f;
        public static bool AwaitingEnd { get; private set; } = false;

        public static void PlayGame(Mission mission)
        {
            ResetAll();
            Reward = mission.Reward;
            TimeLimit = mission.TimeLimit;
            foreach(Goal goal in mission.Goals)
            {
                switch (goal.Type)
                {
                    case GoalType.Kill:
                        KillsRemaining = ((KillGoal)goal).Amount;
                        break;
                    case GoalType.Plant:
                        C4Remaining = 3;
                        break;
                    case GoalType.Boss:
                        BossRemaining = true;
                        break;
                    case GoalType.Money:
                        MoneyRemaining = 3;
                        break;
                    case GoalType.Animals:
                        AnimalsRemaining = 3;
                        break;
                }
            }
            TransitionManager.main.TransitionToScene(2);
        }
        public static void ResetAll()
        {
            KillsRemaining = 0;
            C4Remaining = 0;
            MoneyRemaining = 0;
            AnimalsRemaining = 0;
            BossRemaining = false;
            AwaitingEnd = false;
        }
        public static void EndGame()
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerManager.Stats.BryceBucks += Mathf.FloorToInt(Reward * DifficultyManager.MoneyMultiplier);
            PlayerManager.SaveStats();
            TransitionManager.main.TransitionToScene(3);
        }
        public static void CheckCompletion()
        {
            if (KillsRemaining <= 0
                && C4Remaining <= 0
                && MoneyRemaining <= 0
                && AnimalsRemaining <= 0
                && BossRemaining == false)
                AwaitingEnd = true;
        }
        public static void KillPerson()
        {
            KillsRemaining--;
            CheckCompletion();
        }
        public static void PlantC4()
        {
            C4Remaining--;
            CheckCompletion();
        }
        public static void StealMoney()
        {
            MoneyRemaining--;
            CheckCompletion();
        }
        public static void FreeAnimal()
        {
            AnimalsRemaining--;
            CheckCompletion();
        }
        public static void KillBoss()
        {
            BossRemaining = false;
            CheckCompletion();
        }
    }
}