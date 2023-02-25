using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTE.Managers
{
    public class TransitionManager : MonoBehaviour
    {
        public static TransitionManager main;
        private void Awake()
        {
            main = this;
        }
        [SerializeField] private Animator TransitionAnimator;
        public void TransitionToScene(int scene)
        {
            StartCoroutine(Transition(scene));
        }
        private IEnumerator Transition(int scene)
        {
            TransitionAnimator.SetTrigger("Fade");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(scene);
        }
    }
}