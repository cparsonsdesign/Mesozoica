// Author: Chris Parsons
// Purpose:
// Created:
// Last Updated: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCreature : MonoBehaviour 
{
    Node node;

    private void Start()
    {
        node = GetComponentInParent<Node>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // add the appropriate type here
        if (other.GetComponent<Node_Creature>())
        {
            node.creatures.Add(other.GetComponent<Node_Creature>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // add the appropriate type here
        if (other.GetComponent<Node_Creature>())
        {
            node.creatures.Remove(other.GetComponent<Node_Creature>());
        }
    }
}
