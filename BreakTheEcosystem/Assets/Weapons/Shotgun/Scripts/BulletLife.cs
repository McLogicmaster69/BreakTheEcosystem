using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class BulletLife : MonoBehaviour
    {
        [SerializeField] private float Lifetime = 0.2f;

        private void Start()
        {
            StartCoroutine(RunLife());
        }

        private IEnumerator RunLife()
        {
            yield return new WaitForSeconds(Lifetime);
            Destroy(this.gameObject);
        }
    }
}