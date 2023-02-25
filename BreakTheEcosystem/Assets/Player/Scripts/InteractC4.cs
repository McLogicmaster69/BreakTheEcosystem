using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractC4 : MonoBehaviour   
{
    public GameObject TMPPickUp;
    public bool canPickUp;
    public float distance = 3;
    public LayerMask bomb;
    void Start()
    {
        TMPPickUp.SetActive(false); 
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, bomb))
        {
            if (hit.collider.tag == "C4")
            {
                TMPPickUp.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && Inventory.main.C4 == false)
                {
                    Inventory.main.IncrementFromInventory(InventoryItems.C4);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
                TMPPickUp.SetActive(false);
        }
        else
            TMPPickUp.SetActive(false);
    }
}
