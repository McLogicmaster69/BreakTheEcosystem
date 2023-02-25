using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("No rigidbody?");
        if (collision.CompareTag("TwinTowers(BombingSite)"))
        {
            CanPlace= true;
            TMPPlace.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("TwinTowers(BombingSite)"))
        {
            CanPlace = false;
            TMPPlace.SetActive(false);
        }
    }

}
