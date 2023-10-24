using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] TowerController towerPrefab;
    [SerializeField] bool isPlaceable;

    private void OnMouseDown() 
    {
        if(isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }

    public bool GetIsPlaceable()
    {
        return isPlaceable;
    }
}
