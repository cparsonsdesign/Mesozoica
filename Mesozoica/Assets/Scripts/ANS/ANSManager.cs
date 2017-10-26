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
    int nodeCount;

    public delegate void NewNode();
    public event NewNode NodePlaced;

    public delegate void LostNode();
    public event LostNode NodeRemoved;

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
        NodePlaced();
        nodeCount = nodeList.Count;
    }

    public void RemovedNode()
    {
        nodeCount = nodeList.Count;
        NodeRemoved();
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
