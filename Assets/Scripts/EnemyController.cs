using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 10f)] float speed = 1f;
    [SerializeField] int maxHealth = 5;
    [SerializeField] int difficultyRamp = 1;
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;
    
    int currentHealth;
    BankController bank;

    void OnEnable()
    {        
        currentHealth = maxHealth;

        FindPath();
        GoToStart();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        bank = FindFirstObjectByType<BankController>();
    }

    public void RewardGold()
    {
        if(bank == null)
        {
            return;
        }

        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if(bank == null)
        {
            return;
        }

        bank.Withdraw(goldPenalty);
    }

    private void OnParticleCollision(GameObject other) 
    {
        ProcessHit();    
    }

    void ProcessHit()
    {
        currentHealth--;

        if(currentHealth <= 0)
        {
            RewardGold();
            maxHealth = maxHealth + difficultyRamp;
            gameObject.SetActive(false);
        }
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(GameObject child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
            }            
        }
    }

    void GoToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent = travelPercent + Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }            
        }

        FinishPath();
    }
}
