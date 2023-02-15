using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BTE.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public float speed = 12f;

        public static PlayerMovement main;
        private void Awake()
        {
            main = this;
        }

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);
        }
    }
}