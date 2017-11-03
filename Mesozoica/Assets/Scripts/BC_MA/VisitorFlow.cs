// Author: Chris Parsons
// Purpose: To manage the flow of visitors to the park.
// Created: 10/30/2017
// Last Updated: 11/2/2017 CP

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorFlow : MonoBehaviour 
{
    public enum Months { JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC }
    Months curMonth;


    float Ma = 20;
    float Bc = 10;
    float Sc;
    [SerializeField]
    float visitorFLowDivider = 100;

    float maxVisitorFlow = 60;
    float visitorSpawnTimer;
    float visitorSpawnTimerLimit;
    float maxTimerLimit;

    private void Start()
    {
        VisitorFlowFormula();
        StartCoroutine(CountupTimer());
    }

    public void SetMonth(Months month)
    {
        if(month != curMonth)
        {
            curMonth = month;
        }
       
        switch (month)
        {
            case Months.JAN:
                Sc = 1.5f;
                break;
            case Months.FEB:
                Sc = 1.0f;
                break;
            case Months.MAR:
                Sc = 0.5f;
                break;
            case Months.APR:
                Sc = 0.5f;
                break;
            case Months.MAY:
                Sc = 1.0f;
                break;
            case Months.JUN:
                Sc = 1.2f;
                break;
            case Months.JUL:
                Sc = 1.4f;
                break;
            case Months.AUG:
                Sc = 1.6f;
                break;
            case Months.SEP:
                Sc = 0.5f;
                break;
            case Months.OCT:
                Sc = 1.0f;
                break;
            case Months.NOV:
                Sc = 1.3f;
                break;
            case Months.DEC:
                Sc = 1.7f;
                break;
            default:
                break;
        }
    }

    // This will formula will allow for visitors to spawn quicker as the player's BC and/or MA rises by lowering the minimum and maximum wait times.
    // The spawn timer is then randomized between these 2 values.
    // The higher the player's BC and/or MA the more of an impact the SC is going to have. This will naturally give the players a much
    // nicer learning curve at the beginning of the game, while the magic of math will gradually scale the effects of the SC to the player's skill (aka BC+MA)
    // The script this is placed on will need to have a timer and should spawn the visitors when the timer hits the visitorSpawnTimerLimit
    // The maxTimerLimit is set to 60 to represent 1 minute, but this can be adjusted as needed.
    void VisitorFlowFormula()
    {
       maxVisitorFlow = Mathf.Clamp(60 - (((Ma + Bc) / visitorFLowDivider) * Sc), 5, 59);
        visitorSpawnTimerLimit = UnityEngine.Random.Range(60 - ((((Ma + Bc) / visitorFLowDivider) * Sc) * 2), maxVisitorFlow);

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
