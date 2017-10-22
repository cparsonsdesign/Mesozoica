// Author: Chris Parsons
// Purpose: To maintain a lis of the nodes and alow the target node to be assigned.
// Created: 10/17/2017
// Last Updated: 10/20/2017 CP

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANSManager: MonoBehaviour
{

    [SerializeField]
    public List<Transform> nodePositions = new List<Transform>();
    int nodeCount;
    //float tooFar = 20;

    public delegate void NewNode();
    public event NewNode NodePlaced;

    public delegate void LostNode();
    public event LostNode NodeRemoved;


    public Transform GetNodes(Vector3 from)
    {
        //Debug.Log(from. + " made it here");
        Transform optimalNode = null;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < nodePositions.Count; i++)
        {
            // Use this if you want to have the visitors ignore nodes that are far away

            //float distanceFrom = Vector3.Distance(from, nodePositions[i].position);
            //if (distanceFrom > tooFar)
            //{
            //    continue;
            //}

            float tempDis = Vector3.Distance(from, nodePositions[i].position);
            if (tempDis < minDistance)
            {
                minDistance = tempDis;
                optimalNode = nodePositions[i];
            }
        }
        return optimalNode;
    }

    public void PlacedNode()
    {
        NodePlaced();
        nodeCount = nodePositions.Count;
    }

    public void RemovedNode()
    {
        nodeCount = nodePositions.Count;
        NodeRemoved();
    }
    
}
