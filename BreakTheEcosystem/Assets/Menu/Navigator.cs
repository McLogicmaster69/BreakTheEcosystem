using BTE.Contracts;
using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BTE.Menu
{
    public class Navigator : MonoBehaviour
    {
        public TMP_Text BryceBucks;
        public TMP_Text Objectives;
        public TMP_Text Rewards;
        public TMP_Text Time;

        private Contract[] contracts = new Contract[5];
        private int SelectedContract = 0;

        public GameObject MainMenu;
        public GameObject OptionsMenu;
        public GameObject PlayMenu;

        private void Start()
        {
            BryceBucks.text = $"Bryce Bucks: {PlayerManager.Stats.BryceBucks}";
            OnMainMenuButtonClick();
        }

        // For each button, have a public GameObject reference for each section
        // If you, for exmaple, hit the Options button
        // MainMenu.SetActive(false);
        // Options.SetActive(true);
        // Brackeys has a video on settings if you want to do anything with that to further it
        
        public void OnPlayButtonClick()
        {
            for (int i = 0; i < contracts.Length; i++)
            {
                contracts[i] = Contract.GenerateRandomContract();
            }
            MainMenu.SetActive(false);
            PlayMenu.SetActive(true);
        }
        public void OnOptionsButtonClick()
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(true);
        }
        public void OnMainMenuButtonClick()
        {
            OptionsMenu.SetActive(false);
            PlayMenu.SetActive(false);
            MainMenu.SetActive(true);
        }

        public void OnContractButtonClick(int contractNum)
        {
            SelectedContract = contractNum;
            UpdateContractUI();
        }
        public void UpdateContractUI()
        {

            // Update UI with contracts[SelectedContract]
            //
            // Run a foreach loop with contracts[SelectedContract].Objectives -> foreach(Objective obj in contracts[SelectedContract].Objectives)
            // For each one, switch the current objective obj.Type
            // If .Type is ObjectiveType.Tree, then add to the objective text, $"Destroy {(TreeObjective)obj.Trees} trees"
            // When you do (TreeObjectives)obj, it tells the compiler that obj is a TreeObjective type of Objective, meaning you can access certain things unique to that class
            // Similar things for each class, just check the Contracts/Objectives unity folder for all the classes

            // set bryce bucks and time

            Contract currentContract = contracts[SelectedContract];
            currentContract.UpdateReward();

            Rewards.text = "+" + currentContract.Reward.ToString();
            if (currentContract.TimeLimit == 0)
                Time.text = "-";
            else
                Time.text = (currentContract.TimeLimit / 60).ToString() + " mins";

            string output = "";

            foreach (var objective in currentContract.Objectives)
            {
                switch(objective.Type)
                {
                    case ObjectiveType.Tree:
                        output += $"Destroy {((TreeObjective)objective).Trees} trees\n";
                        break;
                    case ObjectiveType.Target:
                        output += $"Kill {((TargetObjective)objective).Number} {((TargetObjective)objective).Animal.ToString().ToLower()}\n";
                        break;
                    case ObjectiveType.Slaughter:
                        output += $"Kill {((SlaughterObjective)objective).Number} animals\n";
                        break;
                    case ObjectiveType.Moose:
                        output += $"Kill the moose\n";
                        break;
                    case ObjectiveType.GigaMoose:
                        output += $"Kill the giga-moose\n";
                        break;
                    case ObjectiveType.Bryce:
                        output += $"Defeat Bryce\n";
                        break;
                }
            }
            Objectives.text = output;
        }

        public void OnQuitButtonClick()
        {
            Application.Quit();
        }

        public void OnStartButtonClick() //starts game
        {
            MainGameManager.PlayGame(contracts[SelectedContract]);
        }
    }
}