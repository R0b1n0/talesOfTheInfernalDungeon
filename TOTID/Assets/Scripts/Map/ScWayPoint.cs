using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScWayPoint
{
    public Vector3 wayPointId;
    private List<ScWayPoint> neighbors = new List<ScWayPoint>();

    public ScWayPoint(Vector3 id)
    {
        wayPointId = id;
    }
    public void AddNewNeighbor(ScWayPoint neighbor)
    {
        if (neighbors.Count < 5)
            neighbors.Add(neighbor);
        else
            Debug.Log("trop de voisins ");
    }
    public void AddMultipleNeighbors(List<ScWayPoint> neighborList)
    {
        neighbors = neighborList;
    }

    public List<ScWayPoint> GetAllNeighbors()
    {
        return neighbors;
    }
}
