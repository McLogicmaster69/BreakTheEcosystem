using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory main;
    private void Awake()
    {
        main = this;
    }
    public void IncrementFromInventory(InventoryItems input)
    {
        switch (input)
        {
            case InventoryItems.C4:
            C4 = true;
                break;
        }
    }
    public void DecrementFromInventory(InventoryItems input) 
    {
        switch (input)
        {
            case InventoryItems.C4:
                C4 = false;
                break;
        }
    }

    public bool C4 { get; private set; } = false;
}
public enum InventoryItems
{
    C4,
}