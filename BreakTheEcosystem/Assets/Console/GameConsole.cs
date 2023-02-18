using BTE.Contracts;
using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Console
{
    public class GameConsole : MonoBehaviour
    {
        public GameObject ConsoleUI;
        public bool Active = false;
        private void Start()
        {
            ConsoleUI.SetActive(false);
        }
        private void Update()
        {
            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Backspace))
            {
                Active = !Active;
                ConsoleUI.SetActive(Active);
                Cursor.lockState = Active ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        public void LoadBasic()
        {
            Contract c = new Contract();
            MainGameManager.PlayGame(c);
        }
        public void LoadMoose()
        {
            MainGameManager.PlayGame(Contract.GetMooseContract());
        }
        public void LoadBryce()
        {
            MainGameManager.PlayGame(Contract.GetBryceContract());
        }
    }
}