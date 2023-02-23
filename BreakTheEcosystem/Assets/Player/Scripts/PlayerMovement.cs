using BTE.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BTE.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public ShotgunBehaviour Shotgun;
        public float Speed = 12f;
        public float ShotgunSpeed = 8f;

        public static PlayerMovement main;
        private void Awake()
        {
            main = this;
        }

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x == 0 && z == 0)
                return;
            Vector3 move = (transform.right * x + transform.forward * z).normalized;

            // If aiming down sight, slow the player down
            if(Shotgun.DownSights)
                controller.Move(move * ShotgunSpeed * Time.deltaTime);
            else
                controller.Move(move * Speed * Time.deltaTime);

            // Correct the y-coordinate
            transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);
        }
    }
}