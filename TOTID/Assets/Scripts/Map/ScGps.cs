using System.Collections.Generic;
using UnityEngine;

public class ScGps 
{
    private Dictionary<Vector3, Node> open = new Dictionary<Vector3, Node>();
    private List<Vector3> openKeyList = new List<Vector3>();
    private Dictionary<Vector3, Node> closed = new Dictionary<Vector3, Node>();

    private Node nodeToEvaluate = null;
    private Node destinationNode = null;
    private Node startNode = null;

    private List<ScWayPoint> path = new List<ScWayPoint>();

    public List<ScWayPoint> FindPath(ScWayPoint start, ScWayPoint destination)
    {
        open.Clear();
        openKeyList.Clear();
        closed.Clear();

        float firstNodeWeight = GetNodePotential(start, destination);
        startNode = new Node(start, null, firstNodeWeight, firstNodeWeight);

        destinationNode = new Node(destination, null, firstNodeWeight, 0);
        open.Add(startNode.nodeId.wayPointId, startNode);
        openKeyList.Add(startNode.nodeId.wayPointId);

        nodeToEvaluate = startNode;
        EvaluateNode();

        path.Clear();
        CreatPath(closed[destination.wayPointId]);

        return path;
    }
    private void EvaluateNode()
    {

        if (nodeToEvaluate.nodeId.wayPointId == destinationNode.nodeId.wayPointId)
        {
            closed.Add(nodeToEvaluate.nodeId.wayPointId, nodeToEvaluate);
        }
        else
        {
            closed.Add(nodeToEvaluate.nodeId.wayPointId, nodeToEvaluate);

            open.Remove(nodeToEvaluate.nodeId.wayPointId);
            openKeyList.Remove(nodeToEvaluate.nodeId.wayPointId);

            foreach (ScWayPoint neighbors in nodeToEvaluate.nodeId.GetAllNeighbors())
            {
                if (neighbors != null)
                {
                    float nodePotential = GetNodePotential(neighbors, destinationNode.nodeId);
                    float nodeWeight = (nodeToEvaluate.weight - nodeToEvaluate.potential) + nodePotential;

                    if (open.ContainsKey(neighbors.wayPointId))// re-evaluate the node weight in the open List
                    {
                        if (nodeWeight < open[neighbors.wayPointId].weight && open[neighbors.wayPointId] != startNode)
                        {
                            open[neighbors.wayPointId].weight = nodeWeight;
                            open[neighbors.wayPointId].parentNode = nodeToEvaluate.nodeId;
                        }
                    }

                    if (closed.ContainsKey(neighbors.wayPointId)) //re-evaluate the node fromthe closed List and place it back in the openList
                    {
                        // check if the wheight is lower than the previous, and make sure you ain't re-evaluating the start node
                        if (nodeWeight < closed[neighbors.wayPointId].weight && closed[neighbors.wayPointId] != startNode)
                        {
                            closed[neighbors.wayPointId].weight = nodeWeight;
                            closed[neighbors.wayPointId].parentNode = nodeToEvaluate.nodeId;

                            open.Add(neighbors.wayPointId, closed[neighbors.wayPointId]);
                            openKeyList.Add(neighbors.wayPointId);
                            closed.Remove(neighbors.wayPointId);
                        }
                    }

                    if (!open.ContainsKey(neighbors.wayPointId) && !closed.ContainsKey(neighbors.wayPointId))  //the node hasn't been created yet 
                    {
                        Node newNode = new Node(neighbors, nodeToEvaluate.nodeId, nodePotential, nodeWeight);
                        open.Add(neighbors.wayPointId, newNode);
                        openKeyList.Add(neighbors.wayPointId);
                    }
                }
            }
            FindSmallestWeight();
            EvaluateNode();
        }
    }
    private void FindSmallestWeight()
    {

        float minWeight = minWeight = open[openKeyList[0]].weight;

        foreach (KeyValuePair<Vector3, Node> node in open)
        {
            if (node.Value.weight <= minWeight)
            {
                minWeight = node.Value.weight;
                nodeToEvaluate = node.Value;
            }
        }
    }
    private float GetNodePotential(ScWayPoint nodeToEvaluate, ScWayPoint destination)
    {
        return Vector3.Distance(destination.wayPointId, nodeToEvaluate.wayPointId);
    }

    private void CreatPath(Node nodeToAdd)
    {
        path.Add(nodeToAdd.nodeId);

        ScWayPoint parent = closed[path[path.Count - 1].wayPointId].parentNode;

        if (parent != null)
        {
            CreatPath(closed[parent.wayPointId]);
        }
    }
}


public class Node
{
    public ScWayPoint nodeId;
    public ScWayPoint parentNode;
    public float potential;
    public float weight;
    public Node(ScWayPoint id, ScWayPoint node, float myPotential, float myWeight)
    {
        nodeId = id;
        parentNode = node;
        potential = myPotential;
        weight = myWeight;
    }
}