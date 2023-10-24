using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BankController : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    int currentBalance;

    void Awake()
    {
        currentBalance = startingBalance;
    }

    public int GetCurrentBalance()
    {
        return currentBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance = currentBalance + Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        currentBalance = currentBalance - Mathf.Abs(amount);

        if(currentBalance <= 0)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
