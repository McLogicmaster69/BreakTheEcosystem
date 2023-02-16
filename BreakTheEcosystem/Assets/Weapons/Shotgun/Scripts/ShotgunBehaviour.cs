using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Weapons
{
    public class ShotgunBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private GameObject Bullet;
        [SerializeField] private int Spread = 15;
        [SerializeField] private int AmountOfBullets = 8;

        private int Ammo = 2;
        private bool Reloading = false;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R) && Ammo < 2)
            {
                StartCoroutine(ReloadGun());
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo > 0)
            {
                Shoot();
            }
        }

        private IEnumerator ReloadGun()
        {
            Reloading = true;
            Animator.SetTrigger("Reload");
            yield return new WaitForSeconds(1f);
            Ammo = 2;
            yield return new WaitForSeconds(1f);
            Reloading = false;
        }

        private void Shoot()
        {
            Ammo--;
            for (int i = 0; i < AmountOfBullets; i++)
            {
                GameObject bullet = Instantiate(Bullet, this.transform);
                bullet.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), 0));
                bullet.transform.parent = null;
            }
        }
    }
}