using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BankController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] int startingBalance = 150;
    int currentBalance;

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public int GetCurrentBalance()
    {
        return currentBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance = currentBalance + Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance = currentBalance - Mathf.Abs(amount);
        UpdateDisplay();

        if(currentBalance <= 0)
        {
            ReloadScene();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
