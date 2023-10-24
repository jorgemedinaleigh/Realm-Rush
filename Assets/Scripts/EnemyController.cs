using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 10f)] float speed = 1f;
    [SerializeField] int maxHealth = 5;
    
    int currentHealth;

    void Start()
    {        
        currentHealth = maxHealth;

        FindPath();
        GoToStart();
        StartCoroutine(FollowPath());
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
            Destroy(gameObject);
        }
    }

    void FindPath()
    {
        path.Clear();

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void GoToStart()
    {
        transform.position = path[0].transform.position;
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

        Destroy(gameObject);
    }
}
