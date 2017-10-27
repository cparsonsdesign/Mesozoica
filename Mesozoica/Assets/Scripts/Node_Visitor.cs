﻿// Author: Chris Parsons
// Purpose: Small script hooking the visitor up to the ANS Sysytem
// Created: 10/18/2017
// Last Updated: 10/20/2017 CP

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Visitor : MonoBehaviour, IHappinessIncreaseable 
{
    public float happiness = 50;


    public void IncreaseHappiness(float ror)
    {
        happiness = happiness + ror;
    }
}

