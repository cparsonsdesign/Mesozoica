// Author: Chris Parsons
// Purpose:
// Created:
// Last Updated: 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorFlow : MonoBehaviour 
{

    float Ma = 20;
    float Bc = 10;

    float visitorSpawnTimer;
    float maxTimerTime = 60;

    void ViisitorFlow()
    {
        visitorSpawnTimer = maxTimerTime - ((Ma + Bc) / 100);
    }


}
