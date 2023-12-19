using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScDrawPath : MonoBehaviour{

    LineRenderer linePath;
    public ScWayPoint pathStartWayPoint;
    public List<ScWayPoint> pathWayPoints = new List<ScWayPoint>();
    public List<Vector3> pathPositions = new List<Vector3>();
    
    void Start() {
        linePath = GetComponent<LineRenderer>();
    }

    public void ResetLine() {
        linePath.positionCount = 0;
        pathPositions.Clear();
        Debug.Log("Reset");
    }

    public void GetPath(List<ScWayPoint> path) {
        pathWayPoints = GameObject.Find("Player").GetComponent<ScMovement>().path;
        pathStartWayPoint = GameObject.Find("Player").GetComponent<ScMovement>().currentCell;
        for (int i = 0; i < pathWayPoints.Count; i++) {
            pathPositions.Add(pathWayPoints[i].wayPointId + new Vector3(0,0.4f,0));
        }
        pathPositions.Add(pathStartWayPoint.wayPointId + new Vector3(0, 0.4f, 0));
    }

    public void DrawLine () { 
        linePath.positionCount = pathPositions.Count;
        for (int u=0; u<pathPositions.Count; u++) { 
            linePath.SetPosition(u, pathPositions[u]);
        }
    }
}
