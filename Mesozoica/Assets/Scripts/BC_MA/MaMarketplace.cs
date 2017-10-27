// Author: Chris Parsons
// Purpose: to facilitate usage of the MA through a GUI
// Created: 10/25/2017
// Last Updated: 10/26/2017 CP

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaMarketplace : MonoBehaviour, IPurchaseable
{
    [SerializeField]
    float localCost = 9750;
    [SerializeField]
    float nationalCost = 18230;
    [SerializeField]
    float internationalCost = 44732;
    float cost;

    [SerializeField]
    float localMaPoints = 100;
    [SerializeField]
    float nationalMaPoints = 1000;
    [SerializeField]
    float internationalMaPoints = 2500;
    float maPoints;

    // Temporary placeholder variables. These will need to be swapped out for references to the player's stats.
    float totalMaPoints;
    float playerWallet;

    // Variables for the MA Point degradation.
    float MaDegradeTimer;
    float MaDegradeTimeLimit = 120;

    bool crIsRunning;


    // Set the cost and MA Points to the appropriate level
    public void BuyLocal()
    {
        cost = localCost;
        maPoints = localMaPoints;
        PurchaseMa();
    }

    public void BuyNational()
    {
        cost = nationalCost;
        maPoints = nationalMaPoints;
        PurchaseMa();
    }

    public void BuyInternational()
    {
        cost = internationalCost;
        maPoints = internationalMaPoints;
        PurchaseMa();
    }

    // This interface will deduct the player's money in exchange for MA Points.
    // It will need to be properly wired to the player's stats. playerWallet and totalMaPoints are currently just placeholder variables
    public void PurchaseMa()
    {
        if (playerWallet >= cost)
        {
            playerWallet -= cost;
            totalMaPoints += maPoints;

            // Starting the coroutine here ensures that it doesn't run until MA points have been purchased.
            if (!crIsRunning)
            {
                StartCoroutine(MaDegradation());
            }
        }
        else
        {
            // Display message to say that funds are insufficient
        }
    }

    // Increases the MaDregradeTimer by 1 every second then checks to see if the time limit has been reached.
    // If the limit has been reached, decrease the player's MA Points by 1, and reset the timer. 
    // Time Limit is currently set to 2 minutes. This will likely need to be adjusted to feel right for the pace of the game.
    IEnumerator MaDegradation()
    {
        crIsRunning = true;
        while (true)
        {
            MaDegradeTimer++;
            if(MaDegradeTimer >= MaDegradeTimeLimit)
            {
                totalMaPoints -= 1;
                if (totalMaPoints <= 0)
                {
                    MaDegradeTimer = 0;
                    crIsRunning = false;
                    StopCoroutine(MaDegradation());
                }
                else
                {
                    MaDegradeTimer = 0;
                    yield return new WaitForSecondsRealtime(1);
                }
            }
            else
            {
                yield return new WaitForSecondsRealtime(1);
            }
        }
        
    }
}


