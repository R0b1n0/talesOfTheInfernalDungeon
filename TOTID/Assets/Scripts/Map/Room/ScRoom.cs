using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ScRoom : MonoBehaviour
{
    /* the X and Z scale value of the rooms shall always be a multiple of the cell size */


    [SerializeField] float cellSize; //always set a whole number value, this was set as a float to avoid float casting
    [SerializeField] float yOffset;
    [SerializeField] GameObject eedededed;


    private List<List<ScWayPoint>> myGraph = new List<List<ScWayPoint>>();
    private Transform myTrans;
    private ScWayPoint wayPointDestination;
    private List<ScWayPoint> path;

    void Start()
    {
        myTrans = transform;
        MapEnvironement();

    }

    public List<ScWayPoint> FindPathBetween(Vector3 destination, ScWayPoint origin)
    {
        
        wayPointDestination = FindClossestCell(destination);
        path = ScMapManagor.Instance.FindPath(origin, wayPointDestination);
        return path;
    }

    public ScWayPoint FindClossestCell(Vector3 destination)
    {
        ScWayPoint resultWayPoint = null;
        foreach (List<ScWayPoint> wayPointList in myGraph)
        {
            foreach (ScWayPoint wayPoint in wayPointList)
            {
                if (wayPoint != null)
                {
                    if (destination.x < wayPoint.wayPointId.x + (cellSize / 2) && destination.x > wayPoint.wayPointId.x - (cellSize / 2) && destination.z < wayPoint.wayPointId.z + (cellSize / 2) && destination.z > wayPoint.wayPointId.z - (cellSize / 2))
                    {
                        resultWayPoint = wayPoint;
                        break;
                    }
                }
            }
            if (resultWayPoint != null)
                break;
        }
        return resultWayPoint;
    }

    #region mapping
    private void MapEnvironement()
    {
        float zOffset = (myTrans.position.z - (myTrans.localScale.z / 2)) + (cellSize / 2);

        
        for (int i = 0; i < myTrans.localScale.x/cellSize; i++)
        {
            float xOffset = (myTrans.position.x - (myTrans.localScale.x / 2) + (cellSize / 2)) + (cellSize * i);
            myGraph.Add(new List<ScWayPoint>());

            for (int j = 0; j < myTrans.localScale.z / cellSize; j++)
            {
                Vector3 underGround = new Vector3(xOffset, myTrans.position.y + (myTrans.localScale.y / 2) - yOffset  , zOffset + cellSize*j);
                Ray ray = new Ray(underGround, Vector3.up);

                if (!Physics.Raycast(ray))
                {
                    Vector3 newWayPointPos = new Vector3(xOffset, myTrans.position.y + (myTrans.localScale.y / 2), zOffset + cellSize * j);
                    
                    ScWayPoint newWayPoint = new ScWayPoint(newWayPointPos);
                    myGraph[myGraph.Count - 1].Add(newWayPoint);

                    Instantiate(eedededed, newWayPointPos, Quaternion.identity);
                }
                else
                {
                    myGraph[myGraph.Count - 1].Add(null);
                }
            }
        }
        CreateGraph();
    }
    private void CreateGraph()
    {

        for (int raw = 0; raw < myGraph.Count; raw++)
        {
            for (int column = 0; column < myGraph[raw].Count; column++)
            {
                if (myGraph[raw][column] != null)
                    FindWayPointNeighbors(raw,column);
            }
        }//give each wayPoint their neighbors

    }
    private void FindWayPointNeighbors(int raw, int column)
    {
        if (raw + 1 < myGraph.Count && myGraph[raw + 1][column] != null) //check under
        {
            myGraph[raw][column].AddNewNeighbor(myGraph[raw + 1][column]);
            myGraph[raw + 1][column].AddNewNeighbor(myGraph[raw][column]);
        }

        if (column + 1 < myGraph[0].Count && myGraph[raw][column + 1] != null)
        {
            myGraph[raw][column].AddNewNeighbor(myGraph[raw][column + 1]);
            myGraph[raw][column + 1].AddNewNeighbor(myGraph[raw][column]);
        }
    }
    #endregion
}
