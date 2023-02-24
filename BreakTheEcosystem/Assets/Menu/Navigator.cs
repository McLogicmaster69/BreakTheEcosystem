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

        [Header("Sections")]
        public GameObject MainMenu;
        public GameObject OptionsMenu;
        public GameObject PlayMenu;
        public GameObject CreditsMenu;
        public GameObject MusicMenu;

        [Header("Audio")]

        public AudioSource Music;
        public AudioClip[] Pieces;

        public AudioSource ByeBye;

        private void Start()
        {
            PlayerManager.LoadStats();
            DifficultyManager.SetDifficulty(Difficulty.Easy);
            OnMainMenuButtonClick();
        }

        // For each button, have a public GameObject reference for each section
        // If you, for exmaple, hit the Options button
        // MainMenu.SetActive(false);
        // Options.SetActive(true);
        // Brackeys has a video on settings if you want to do anything with that to further it
        
        public void OnPlayButtonClick()
        {
            BryceBucks.text = $"Bryce Bucks: {PlayerManager.Stats.BryceBucks}";
            for (int i = 0; i < contracts.Length; i++)
            {
                contracts[i] = Contract.GenerateRandomContract();
            }
            MainMenu.SetActive(false);
            PlayMenu.SetActive(true);
            UpdateContractUI();
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
            CreditsMenu.SetActive(false);
            MusicMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        public void OnCreditsButtonClick()
        {
            CreditsMenu.SetActive(true);
            MainMenu.SetActive(false);
            MusicMenu.SetActive(false);
        }
        public void OnMusicButtonClick()
        {
            MusicMenu.SetActive(true);
            CreditsMenu.SetActive(false);
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
            StartCoroutine(QuitGame());
        }

        public void OnStartButtonClick() //starts game
        {
            MainGameManager.PlayGame(contracts[SelectedContract]);
        }

        public void ChangeMusic(int id)
        {
            Music.clip = Pieces[id];
            Music.Play();
        }

        private IEnumerator QuitGame()
        {
            ByeBye.Play();
            yield return new WaitUntil(() => ByeBye.isPlaying == false);
            Application.Quit();
        }
    }
}