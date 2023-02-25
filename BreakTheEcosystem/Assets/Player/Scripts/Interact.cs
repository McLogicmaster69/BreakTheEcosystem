using BTE.BDLC.Cage;
using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Player
{
    public class Interact : MonoBehaviour
    {
        public GameObject PickupTxt;
        public GameObject FreeTxt;
        public float distance = 3;
        public LayerMask Interactable;
        void Start()
        {
            PickupTxt.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance, Interactable))
            {
                if (hit.collider.tag == "C4")
                {
                    PickupTxt.SetActive(!Inventory.main.C4);
                    if (Input.GetKeyDown(KeyCode.E) && Inventory.main.C4 == false)
                    {
                        Inventory.main.IncrementFromInventory(InventoryItems.C4);
                        Destroy(hit.collider.gameObject);
                    }
                }
                else if (hit.collider.tag == "Money")
                {
                    PickupTxt.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        BDLCGameManager.StealMoney();
                        Destroy(hit.collider.gameObject);
                    }
                }
                else
                    PickupTxt.SetActive(false);

                if (hit.collider.tag == "Cage")
                {
                    CageBehaviour cb = hit.collider.gameObject.GetComponent<CageBehaviour>();
                    FreeTxt.SetActive(cb.Up);
                    if (Input.GetKeyDown(KeyCode.E))
                        cb.Interact();
                }
                else
                    FreeTxt.SetActive(false);
            }
            else
            {
                PickupTxt.SetActive(false);
                FreeTxt.SetActive(false);
            }
        }
    }
}