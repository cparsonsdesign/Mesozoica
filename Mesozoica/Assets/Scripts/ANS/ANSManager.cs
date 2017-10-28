// Author: Chris Parsons
// Purpose: To maintain a lis of the nodes and alow the target node to be assigned.
// Created: 10/17/2017
// Last Updated: 10/20/2017 CP

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANSManager: MonoBehaviour, IGetRor
{

    public List<Node> nodeList = new List<Node>();

    // There needs to be a check against the max node number before a node is placed
    int nodeCount;
    int nodeMax;

    public delegate void NewNode();
    public event NewNode NodePlaced;

    public delegate void LostNode();
    public event LostNode NodeRemoved;

    int thresholdReached = 0;
    // This is a temporary variable. This needs to be a reference to the player's actual visitor count.
    public int visitorCount;

    static int SortByRor(Node node1, Node node2)
    {
        return node1.ror.CompareTo(node2.ror);
    }

    public List<Node> SortNodeListForGui()
    {
        nodeList.Sort(SortByRor);
        nodeList.Reverse();
        return nodeList;
    }

 
    public void PlacedNode()
    {
        if (NodePlaced != null)
        {
            NodePlaced();
        }
        nodeCount = nodeList.Count;
    }

    public void RemovedNode()
    {
        nodeCount = nodeList.Count;
        NodeRemoved();
    }

    // This needs to be worked into the player's stats.
   
    public void ThresholdReached()
    {
        if(visitorCount >= thresholdReached + 100)
        {
            nodeMax += 3;
            thresholdReached = thresholdReached + 100;
        }
    }



    //public Transform GetNodes(Vector3 from)
    //{
    //    Transform optimalNode = null;
    //    float minDistance = Mathf.Infinity;

    //    for (int i = 0; i < nodeList.Count; i++)
    //    {
    //        float tempDis = Vector3.Distance(from, nodeList[i].position);
    //        if (tempDis < minDistance)
    //        {
    //            minDistance = tempDis;
    //            optimalNode = nodeList[i];
    //        }
    //    }
    //    return optimalNode;
    //}

}
