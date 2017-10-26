// Author: Chris Parsons
// Purpose: to facilitate usage of the MA through a GUI
// Created: 10/25/2017
// Last Updated: 10/25/2017 CP

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
    float localMaPoints;
    [SerializeField]
    float nationalMaPoints;
    [SerializeField]
    float internationalMaPoints;
    float maPoints;

    float totalMaPoints;
    float playerWallet;


    // There's no real reason that cost and maPoints have to be here. It's just how I did it. If you want I can just go off of the prices and maPoints from the different levels.
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

    public void PurchaseMa()
    {
        if (playerWallet >= cost)
        {
            playerWallet -= cost;
            totalMaPoints += maPoints;
        }
        else
        {
            // Display message to say that funds are insufficient
        }
    }
}


