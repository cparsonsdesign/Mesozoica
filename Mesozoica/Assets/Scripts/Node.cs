// Author: Chris Parsons
// Purpose: To handle the node's functionality and interaction wih the visitors
// Created: 10/18/2017
// Last Updated: 10/20/2017 CP

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour 
{
    [SerializeField]
    [Tooltip("Place the ANS Manager here")]
    ANSManager ans;
  
    // This node's lists of creatures and visitors to calculate RoR and to heal visitors in the list when they are in range
    public List<IHappinessIncreaseable> visitors = new List<IHappinessIncreaseable>();
    public List<ICreature> creatures = new List<ICreature>();

    [Tooltip("Do not edit this value, it is visible in the inspector for debugging only. The script will update this appropriately. This is the value that will be used to sort the nodes in the GUI")]
    public float ror = 3;
    #region ( Ror multipliers )
    // Multiplers for RoR Calculation
    [SerializeField]
    [Tooltip("This is the base ror of any node, set it to whatever you like")]
    float baseRorAmount = 3;
    float creatureRorMultiplier = 0.75f;
    float rarityMultiplier;
    float visitorRorMultiplier = 0.015f;
    #endregion

    #region(Variables that may trigger a RoR update)
    int currentVisitorCount;
    int currentCreatureCount;
    float checkTimer;
    float checkTimeLimit;
    [SerializeField]
    [Tooltip("The number of visitors/creatures that need to be added/removed to trigger the RoR to update")]
    int maxCountChange;
    #endregion

    float NodeAoe = 35;
    [SerializeField]
    [Tooltip ("How long between each heal to the visitors' happiness")]
    float increaseWaitTimer = 1;
    



    // Use this for initialization
    void Start () 
	{
       
        StartCoroutine(IncreaseHappiness());
        ans.nodeList.Add(this);
        GrabCreaturesAndVisitors(this.transform.position);
        UpdateROR();
    }

    void GrabCreaturesAndVisitors(Vector3 center)
    {
        Collider[] visitorColliders = Physics.OverlapSphere(center, NodeAoe);

        foreach (Collider hit in visitorColliders)
        {
            if (hit.GetComponent(typeof(IHappinessIncreaseable)))
            {
                visitors.Add((IHappinessIncreaseable)hit.GetComponent(typeof(IHappinessIncreaseable)));
            }
            else if (hit.GetComponent(typeof(ICreature)))
            {
                creatures.Add((ICreature)hit.GetComponent(typeof(ICreature)));
            }
        }
    }

    public void UpdateROR()
    {
        rarityMultiplier = 0;

        for (int i = 0; i < creatures.Count; i++)
        {
            rarityMultiplier = rarityMultiplier + creatures[i].GetDinoRarity();
        }

        float finalDinoMultiplier = creatureRorMultiplier * rarityMultiplier;

        if (visitors.Count > 5)
        {
            // Checks to see if the current count of creatures or visitors has shifted up or down by 5.
            // This check is to help not run calculations when they aren't 100% needed.
            if (visitors.Count - currentVisitorCount >= Mathf.Abs(maxCountChange) || creatures.Count - currentCreatureCount == Mathf.Abs(maxCountChange))
            {
                currentVisitorCount = visitors.Count;
                currentCreatureCount = creatures.Count;
                

                // The fancy equation :)
                ror = Mathf.Clamp((baseRorAmount + (creatures.Count * finalDinoMultiplier)) - visitors.Count * Mathf.Exp(visitorRorMultiplier), baseRorAmount, 20);
            }
        }
        else
        {
            currentVisitorCount = visitors.Count;
            currentCreatureCount = creatures.Count;

            // The fancy equation :)
            ror = Mathf.Clamp((baseRorAmount + (creatures.Count * finalDinoMultiplier)) - visitors.Count * Mathf.Exp(visitorRorMultiplier), baseRorAmount, 20);
        }
    }


   IEnumerator IncreaseHappiness()
    {
        while (true)
        {
            if (visitors.Count > 0)
            {
                do
                {
                    for (int i = 0; i < visitors.Count; i++)
                    {
                        visitors[i].IncreaseHappiness(ror);
                    }
                    yield return new WaitForSecondsRealtime(increaseWaitTimer);
                    UpdateROR();
                }
                while (visitors.Count > 0);
            }
            else
            {
                yield return new WaitForSeconds(increaseWaitTimer);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        // add the appropriate type here
        if (other.GetComponent(typeof(IHappinessIncreaseable)))
        {
            visitors.Add((IHappinessIncreaseable)other.GetComponent(typeof(IHappinessIncreaseable)));
        }
        if (other.GetComponent(typeof(ICreature)))
        {
            creatures.Add((ICreature)other.GetComponent(typeof(ICreature)));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // add the appropriate type here
        if (other.GetComponent(typeof(IHappinessIncreaseable)))
        {
            visitors.Remove((IHappinessIncreaseable)other.GetComponent(typeof(IHappinessIncreaseable)));
        }
        if (other.GetComponent(typeof(ICreature)))
        {
            creatures.Remove((ICreature)other.GetComponent(typeof(ICreature)));
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, NodeAoe);
    }


}
