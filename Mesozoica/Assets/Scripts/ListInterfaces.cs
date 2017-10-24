// Author: Chris Parsons
// Purpose:
// Created:
// Last Updated: 

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
   void SortNodeListForGui();
}

