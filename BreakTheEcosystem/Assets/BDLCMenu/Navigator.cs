using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.Menu
{
    public class Navigator : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject PlayMenu;

        public void OnPlayClicked()
        {
            MainMenu.SetActive(false);
            PlayMenu.SetActive(true);
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
    }
}