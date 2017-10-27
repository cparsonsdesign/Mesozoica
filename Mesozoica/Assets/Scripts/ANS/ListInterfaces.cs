// Author: Chris Parsons
// Purpose: Holds the intefaces for comunication with the ANS
// Created: 10/20/2017
// Last Updated: 10/25/2017 CP

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHappinessIncreaseable
{
    void IncreaseHappiness(float ror);
}

public interface ICreature
{
    float GetDinoRarity();
}

public interface IGetRor
{
    List<Node> SortNodeListForGui();
}

