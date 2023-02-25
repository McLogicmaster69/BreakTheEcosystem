using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.End
{
    public class EndTrigger : MonoBehaviour
    {
        private bool HasC4 = false;
        private AudioSource Audio;
        private void Start()
        {
            Audio = GetComponent<AudioSource>();
        }
        private void Update()
        {
            if (BDLCGameManager.C4Remaining > 0)
                HasC4 = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(BDLCGameManager.AwaitingEnd && other.CompareTag("Player"))
            {
                if (HasC4)
                    Audio.Play();
                BDLCGameManager.EndGame();
            }
        }
    }
}