using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BTE.Weapons
{
    public class ShotgunBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private GameObject Bullet;
        [SerializeField] private TMP_Text Text;
        [SerializeField] private int Spread = 15;
        [SerializeField] private int AmountOfBullets = 8;

        [SerializeField] private AudioSource SoundSource;
        [SerializeField] private AudioClip ReloadSound;
        [SerializeField] private AudioClip ShotSound;

        public bool Reloading { get; private set; } = false;

        private int Ammo = 2;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R) && Ammo < 2 && Reloading == false)
            {
                StartCoroutine(ReloadGun());
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo > 0 && Reloading == false)
            {
                Shoot();
            }
            Text.text = $"{Ammo} / 2";
        }

        private IEnumerator ReloadGun()
        {
            Reloading = true;
            Animator.SetTrigger("Reload");
            yield return new WaitForSeconds(1f);
            SoundSource.clip = ReloadSound;
            SoundSource.Play();
            Ammo = 2;
            yield return new WaitForSeconds(1f);
            Reloading = false;
        }

        private void Shoot()
        {
            SoundSource.clip = ShotSound;
            SoundSource.Play();
            Ammo--;
            for (int i = 0; i < AmountOfBullets; i++)
            {
                GameObject bullet = Instantiate(Bullet, this.transform);
                bullet.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-Spread, Spread + 1), Random.Range(-Spread, Spread + 1), 0));
                bullet.transform.parent = null;
            }
        }
    }
}