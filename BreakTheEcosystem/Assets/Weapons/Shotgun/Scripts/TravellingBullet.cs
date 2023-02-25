using BTE.Managers;
using BTE.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class TravellingBullet : MonoBehaviour
    {
        [SerializeField] private float Lifetime = 2f;
        [SerializeField] private float Speed = 30f;
        [SerializeField] private int Damage = 3;
        [SerializeField] private LayerMask PlayerLayer;
        [SerializeField] private LayerMask WallLayer;

        private Vector3 PrevPosition;

        private void Start()
        {
            Damage = DifficultyManager.BulletDamage;
            PrevPosition = transform.position;
            StartCoroutine(RunLife());
        }
        private void Update()
        {
            PrevPosition = transform.position;
            transform.position += Speed * Time.deltaTime * transform.forward;
            CheckIfHitPlayer();
        }
        private void CheckIfHitPlayer()
        {
            Vector3 directionToBullet = (PrevPosition - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, PrevPosition);

            bool hitPlayer = Physics.Raycast(transform.position, directionToBullet, out RaycastHit playerHit, distance, PlayerLayer);
            bool hitWall = Physics.Raycast(transform.position, directionToBullet, out RaycastHit wallHit, distance, WallLayer);

            if (hitPlayer && hitWall)
            {
                if(playerHit.distance < wallHit.distance)
                    PlayerHealth.main.TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if(hitPlayer)
            {
                PlayerHealth.main.TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if(hitWall)
                Destroy(gameObject);
        }

        private IEnumerator RunLife()
        {
            yield return new WaitForSeconds(Lifetime);
            Destroy(this.gameObject);
        }
    }
}