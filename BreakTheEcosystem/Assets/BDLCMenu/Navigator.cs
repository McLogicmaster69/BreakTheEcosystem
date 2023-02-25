using BTE.BDLC.Missions;
using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BTE.BDLC.Menu
{
    public class Navigator : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject PlayMenu;

        public TMP_Text BryceBucks;
        public TMP_Text Objectives;
        public TMP_Text Rewards;
        public TMP_Text Time;

        private Mission[] missions = new Mission[5];
        private int SelectedMission = 0;

        private void Start()
        {
            PlayerManager.LoadStats();
            DifficultyManager.SetDifficulty(Difficulty.Easy);
            OnMainMenuClicked();
        }

        public void OnPlayClicked()
        {
            BryceBucks.text = $"Bryce Bucks: {PlayerManager.Stats.BryceBucks}";
            for (int i = 0; i < missions.Length; i++)
            {
                missions[i] = Mission.GenerateRandomMission();
            }
            MainMenu.SetActive(false);
            PlayMenu.SetActive(true);
            UpdateContractUI();
        }
        public void OnMainMenuClicked()
        {
            MainMenu.SetActive(true);
            PlayMenu.SetActive(false);
        }
        public void OnBackClicked()
        {
            TransitionManager.main.TransitionToScene(0);
        }
        public void OnMissionClicked(int missionNum)
        {
            SelectedMission = missionNum;
            UpdateContractUI();
        }
        public void OnStartClicked()
        {
            BDLCGameManager.PlayGame(missions[SelectedMission]);
        }

        public void UpdateContractUI()
        {

            // set bryce bucks and time

            Mission currentMission = missions[SelectedMission];
            currentMission.UpdateReward();

            Rewards.text = "+" + currentMission.Reward.ToString();
            if (currentMission.TimeLimit == 0)
                Time.text = "-";
            else
                Time.text = (currentMission.TimeLimit / 60f).ToString() + " mins";

            string output = "";

            foreach (Goal goal in currentMission.Goals)
            {
                switch (goal.Type)
                {
                    case GoalType.Kill:
                        output += $"Kill {((KillGoal)goal).Amount} people\n";
                        break;
                    case GoalType.Plant:
                        output += $"Blow up the place\n";
                        break;
                    case GoalType.Boss:
                        output += "Kill the boss\n";
                        break;
                    case GoalType.Money:
                        output += "Steal the money\n";
                        break;
                    case GoalType.Animals:
                        output += "Free the animals\n";
                        break;
                }
            }
            Objectives.text = output;
        }
    }
}