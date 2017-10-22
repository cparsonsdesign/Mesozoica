// Author: Chris Parsons
// Purpose:
// Created:
// Last Updated: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public GameObject node;
    public Transform spawnSpot;
    public ANSManager ans;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(Input.GetKeyDown(KeyCode.N))
        {
            GameObject newNode = Instantiate(node, spawnSpot );
            ans.nodePositions.Add(newNode.transform);
            ans.PlacedNode();
        }
	}
}
