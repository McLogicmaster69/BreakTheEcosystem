using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class Toolbar : MonoBehaviour
    {
        public ShotgunBehaviour ShotgunBehaviour;
        public GameObject Flamethrower;
        public GameObject Shotgun;

        private void Update()
        {
            if (!ShotgunBehaviour.Reloading)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Flamethrower.SetActive(true);
                    Shotgun.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Flamethrower.SetActive(false);
                    Shotgun.SetActive(true);
                }
            }
        }
    }
}