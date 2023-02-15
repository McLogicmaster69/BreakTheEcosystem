using BTE.Contracts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    private Contract[] contracts = new Contract[5];
    public void OnPlayButtonClick()
    {
        for (int i = 0; i < contracts.Length; i++)
        {
            contracts[i] = Contract.GenerateRandomContract();
        }

    }

    public void OnContractButtonClick(int contractNum)
    {

    }
}
