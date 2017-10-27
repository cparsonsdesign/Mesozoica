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
    float visitorSpawnTimerLimit;
    float maxTimerLimit = 60;

    private void Start()
    {
        VisitorFlowFormula();
        StartCoroutine(CountupTimer());
    }

    // This will formula will allow for visitors to spawn quicker as the player's BC and/or MA rises.
    // The script this is placed on will need to have a timer and should spawn the visitors when the timer hits the visitorSpawnTimerLimit
    // The maxTimerLimit is set to 60 to represent 1 minute, but this can be adjusted as needed.

    void VisitorFlowFormula()
    {
        visitorSpawnTimerLimit = maxTimerLimit - ((Ma + Bc) / 100);
    }

    // Just a super simple timer coroutine. No need to actually use this one.
    IEnumerator CountupTimer()
    {
       while(true)
        {
            visitorSpawnTimer++;
            if(visitorSpawnTimer >= visitorSpawnTimerLimit)
            {
                //spawn visitor
                visitorSpawnTimer = 0;
                yield return new WaitForSecondsRealtime(1);
            }
            else
            {
                yield return new WaitForSecondsRealtime(1);
            }
        }
        
    }
}
