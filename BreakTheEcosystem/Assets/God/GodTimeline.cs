using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTE.BDLC.God
{
    public class GodTimeline : MonoBehaviour
    {
        public AudioSource Audio;
        private void Start()
        {
            StartCoroutine(Action());
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(3);
            }
        }
        private IEnumerator Action()
        {
            yield return new WaitForSeconds(1f);
            Audio.Play();
            yield return new WaitForSeconds(17f);
            SceneManager.LoadScene(3);
        }
    }
}