using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Music
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioSource Music;
        public AudioClip[] Pieces;
        private void Update()
        {
            if (!Music.isPlaying)
            {
                Music.clip = Pieces[Random.Range(0, Pieces.Length)];
                Music.Play();
            }
        }
    }
}