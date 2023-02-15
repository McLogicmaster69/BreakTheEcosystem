using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class Toolbar : MonoBehaviour
    {
        public GameObject Flamethrower;
        public GameObject Shotgun;
        public GameObject Boomerang;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Flamethrower.SetActive(true);
                Shotgun.SetActive(false);
                Boomerang.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Flamethrower.SetActive(false);
                Shotgun.SetActive(true);
                Boomerang.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Flamethrower.SetActive(false);
                Shotgun.SetActive(false);
                Boomerang.SetActive(true);
            }
        }
    }
}