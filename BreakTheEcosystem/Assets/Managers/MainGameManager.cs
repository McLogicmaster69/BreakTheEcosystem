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
        private static AnimalType Target;
        private static int TreesRemaining = 0;
        private static int TargetsRemaining = 0;
        private static int SlaughterRemaining = 0;
        private static bool MooseRemaining = false;
        private static bool GigaMooseRemaining = false;
        private static bool BryceRemaining = false;
        public static void PlayGame(Contract contract)
        {
            Target = AnimalType.None;
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
            SceneManager.LoadScene(1);
        }
        public static void EndGame()
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
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