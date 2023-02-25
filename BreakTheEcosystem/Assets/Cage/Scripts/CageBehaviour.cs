using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.Cage
{
    public class CageBehaviour : MonoBehaviour
    {
        private bool Up = true;
        public GameObject CageObject;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Bullet") && Up)
            {
                Destroy(CageObject);
                Up = false;
                BDLCGameManager.FreeAnimal();
            }
        }
    }
}