using BTE.Animals;
using BTE.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTE.Managers
{
    public static class MainGameManager
    {
        public static AnimalType Target { get; private set; } = AnimalType.None;
        public static int TreesRemaining { get; private set; } = 0;
        public static int TargetsRemaining { get; private set; } = 0;
        public static int SlaughterRemaining { get; private set; } = 0;
        public static bool MooseRemaining { get; private set; } = false;
        public static bool GigaMooseRemaining { get; private set; } = false;
        public static bool BryceRemaining { get; private set; } = false;
        public static int Reward { get; private set; } = 0;
        public static float TimeLimit { get; private set; } = 0f;
        public static float Sensitivity { get; set; } = 100f;
        public static void PlayGame(Contract contract)
        {
            ResetAll();
            Target = AnimalType.None;
            Reward = contract.Reward;
            TimeLimit = contract.TimeLimit;
            foreach (Objective objective in contract.Objectives)
            {
                switch (objective.Type)
                {
                    case ObjectiveType.Tree:
                        TreesRemaining = ((TreeObjective)objective).Trees;
                        break;
                    case ObjectiveType.Target:
                        Target = ((TargetObjective)objective).Animal;
                        TargetsRemaining = ((TargetObjective)objective).Number;
                        break;
                    case ObjectiveType.Slaughter:
                        SlaughterRemaining = ((SlaughterObjective)objective).Number;
                        break;
                    case ObjectiveType.Moose:
                        MooseRemaining = true;
                        break;
                    case ObjectiveType.GigaMoose:
                        GigaMooseRemaining = true;
                        break;
                    case ObjectiveType.Bryce:
                        BryceRemaining = true;
                        break;
                }
            }
            TransitionManager.main.TransitionToScene(1);
        }
        private static void ResetAll()
        {
            Target = AnimalType.None;
            TreesRemaining = 0;
            TargetsRemaining = 0;
            SlaughterRemaining = 0;
            MooseRemaining = false;
            GigaMooseRemaining = false;
            BryceRemaining = false;
            Reward = 0;
        }
        public static void EndGame()
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerManager.Stats.BryceBucks += Mathf.FloorToInt(Reward * DifficultyManager.MoneyMultiplier);
            PlayerManager.SaveStats();
            TransitionManager.main.TransitionToScene(0);
        }
        public static void CheckCompletion()
        {
            if (TreesRemaining <= 0
                && TargetsRemaining <= 0
                && SlaughterRemaining <= 0
                && MooseRemaining == false
                && GigaMooseRemaining == false
                && BryceRemaining == false)
                EndGame();
        }

        public static void TreeBurnt()
        {
            TreesRemaining--;
            CheckCompletion();
        }
        public static void AnimalKilled(AnimalType type)
        {
            SlaughterRemaining--;
            if (type == Target)
                TargetsRemaining--;
            if(type == AnimalType.Moose)
                MooseRemaining = false;
            if(type == AnimalType.GigaMoose)
                GigaMooseRemaining = false;
            if(type == AnimalType.Bryce)
                BryceRemaining = false;
            CheckCompletion();
        }
    }
}