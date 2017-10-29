// Author: Chris Parsons
// Purpose: to facilitate usage of the MA through a GUI
// Created: 10/25/2017
// Last Updated: 10/29/2017 CP

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaMarketplaceManager: MonoBehaviour
{
    public MAItem[] maItems;

    // Temporary placeholder variables. These will need to be swapped out for references to the player's stats.
    float totalMaPoints;
    float playerWallet;

    // Variables for purchasing MaPoints
    float cost;
    float maPoints;

    // Variables for the MA Point degradation.
    float MaDegradeTimer;
    float MaDegradeTimeLimit = 120;
    bool crIsRunning;


    public void SetValues(int arrayPosition)
    {
        cost = maItems[arrayPosition].cost;
        maPoints = maItems[arrayPosition].maPoints;
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


