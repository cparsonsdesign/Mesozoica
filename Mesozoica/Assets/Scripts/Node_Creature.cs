// Author: Chris Parsons
// Purpose: To handle the creatures (minimal) interaction with the ANS nodes
// Created: 10/18/2017
// Last Updated: 10/20/2017 CP

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Creature : MonoBehaviour, ICreature 
{
    [SerializeField]
    float rarity = 3;

    public float GetDinoRarity()
    {
        return rarity;
    }

}
