using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class TravellingBullet : MonoBehaviour
    {
        [SerializeField] private float Lifetime = 2f;
        [SerializeField] private float Speed = 30f;

        private void Start()
        {
            StartCoroutine(RunLife());
        }
        private void Update()
        {
            transform.position += Speed * Time.deltaTime * transform.forward;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                Destroy(gameObject);
        }

        private IEnumerator RunLife()
        {
            yield return new WaitForSeconds(Lifetime);
            Destroy(this.gameObject);
        }
    }
}