using BTE.Contracts;
using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

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

        private void Start()
        {
            BryceBucks.text = $"Bryce Bucks: {PlayerManager.Stats.BryceBucks}";
            OnPlayButtonClick();
            UpdateContractUI();
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

        }
        public void OnContractButtonClick(int contractNum)
        {
            SelectedContract = contractNum;
            UpdateContractUI();
        }
        private void UpdateContractUI()
        {
            // Update UI with contracts[SelectedContract]
            //
            // Run a foreach loop with contracts[SelectedContract].Objectives -> foreach(Objective obj in contracts[SelectedContract].Objectives)
            // For each one, switch the current objective obj.Type
            // If .Type is ObjectiveType.Tree, then add to the objective text, $"Destroy {(TreeObjective)obj.Trees} trees"
            // When you do (TreeObjectives)obj, it tells the compiler that obj is a TreeObjective type of Objective, meaning you can access certain things unique to that class
            // Similar things for each class, just check the Contracts/Objectives unity folder for all the classes
        }
    }
}