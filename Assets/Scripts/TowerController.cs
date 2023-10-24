using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] int cost = 75;

    public bool CreateTower(TowerController tower, Vector3 position)
    {
        BankController bank = FindAnyObjectByType<BankController>();

        if(bank == null)
        {
            return false;
        }

        if(bank.GetCurrentBalance() >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }    

        return false;    
    }
}
