using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.Cage
{
    public class CageBehaviour : MonoBehaviour
    {
        public bool Up = true;
        public GameObject CageObject;
        public void Interact()
        {
            if (Up)
            {
                Up = false;
                Destroy(CageObject);
                BDLCGameManager.FreeAnimal();
            }
        }
    }
}