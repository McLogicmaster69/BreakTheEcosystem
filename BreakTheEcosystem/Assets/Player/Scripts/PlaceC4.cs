using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Player
{
    public class PlaceC4 : MonoBehaviour
    {
        public GameObject TMPPlace;
        public GameObject C4;

        bool CanPlace = false;

        // Start is called before the first frame update
        void Start()
        {
            TMPPlace.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            TMPPlace.SetActive(CanPlace && Inventory.main.C4);
            if (Input.GetKeyDown(KeyCode.E) && CanPlace && Inventory.main.C4)
            {
                Inventory.main.DecrementFromInventory(InventoryItems.C4);
                BDLCGameManager.PlantC4();
                GameObject oh = Instantiate(C4);
                oh.transform.position = transform.position;
            }
        }
        void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("TwinTowers(BombingSite)"))
            {
                CanPlace = true;
            }
        }

        void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("TwinTowers(BombingSite)"))
            {
                CanPlace = false;
            }
        }

    }
}