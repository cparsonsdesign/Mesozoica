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
    //ANSManager ans;
    public float ror = 3;

    // This node's lists of creatures and visitors to calculate RoR and to heal visitors in the list when they are in range
    // ***** Be sure to change the types here and on the OnTriggerEnter/Exit functions!!!!!! ****
    [SerializeField]
    public List<Node_Visitor> visitors = new List<Node_Visitor>();
    public List<Node_Creature> creatures = new List<Node_Creature>();
    
    #region ( ror multipliers )
    // Multiplers for RoR Calculation
    float baseRorAmount = 3;
    float creatureRorMultiplier = 0.75f;
    float visitorRorMultiplier = 0.015f;
    #endregion

    #region(variables that may trigger a RoR update)
    int currentVisitorCount;
    int currentCreatureCount;
    float checkTimer;
    float checkTimeLimit;
    [SerializeField]
    [Tooltip("The number of visitors/creatures that need to be added/removed to trigger the RoR to update")]
    int maxCountChange;
    #endregion

    float NodeVisitorAoe = 35;
    //float NodeCreatureAoi = 35;
    [SerializeField]
    float increaseWaitTimer = 3;
    bool crIsRunning;



    // Use this for initialization
    void Start () 
	{
        //ans = FindObjectOfType<ANSManager>();
        //// This is here to test things out. When implemented, if this isn't removed, every node will be added to the list
        //// twice. Once from the ANS Manager and once here
        //ans.nodePositions.Add(this.transform);

        // When the node is first placed, find all the visitors and creatures that are within range and add them to the apprpriate list. Then update the RoR.
       //GrabCreaturesAndVisitors(this.transform.position);
        UpdateROR();
	}

    //void GrabCreaturesAndVisitors(Vector3 center)
    //{
    //    Collider[] visitorColliders = Physics.OverlapSphere(center, NodeVisitorAoe);

    //    foreach (Collider hit in visitorColliders)
    //    {
    //        if (hit.GetComponent<Node_Visitor>())
    //        {
    //            visitors.Add(hit.GetComponent<Node_Visitor>());
    //        }
    //    }

    //    Collider[] creatureColliders = Physics.OverlapSphere(center, NodeCreatureAoi);

    //    foreach (Collider hit in creatureColliders)
    //    {
    //        if (hit.GetComponent<Node_Creature>())
    //        {
    //            creatures.Add(hit.GetComponent<Node_Creature>());
    //        }
    //    }
    //}
    public void UpdateROR()
    {

        if (visitors.Count > 5)
        {
            // Checks to see if the current count of creatures or visitors has shifted up or down by 5. (the commented out if() is the long way of writing everything out if the Mathf.Abs formula isn't working.)
            // This check is to help not run calculations when they aren't 100% needed.
            //if(visitors.Count > currentVisitorCount + 5 || visitors.Count < currentVisitorCount - 5 || creatures.Count > currentCreatureCount + 5 || creatures.Count < currentCreatureCount - 5 || checkTimer > checkTimeLimit)
            if (visitors.Count - currentVisitorCount == Mathf.Abs(maxCountChange) || creatures.Count - currentCreatureCount == Mathf.Abs(maxCountChange) || checkTimer >= checkTimeLimit)
            {
                currentVisitorCount = visitors.Count;
                currentCreatureCount = creatures.Count;

                // The fancy equation goes here :)
                ror = Mathf.Clamp((baseRorAmount + (creatures.Count * creatureRorMultiplier)) - visitors.Count * Mathf.Exp(visitorRorMultiplier), baseRorAmount, 20);
            }
        }
        else
        {
            currentVisitorCount = visitors.Count;
            currentCreatureCount = creatures.Count;

            // The fancy equation goes here :)
            ror = Mathf.Clamp((baseRorAmount + (creatures.Count * creatureRorMultiplier)) - visitors.Count * Mathf.Exp(visitorRorMultiplier), baseRorAmount, 20);
        }
    }



    // Update is called once per frame
    void Update () 
	{
        // You don't have to use the timer if you don't want to. But its here if you need/want it. If you take this out, be sure to remove it from the if-statement in UpdateROR as well
        //checkTimer += Time.deltaTime;

      if(visitors.Count > 0 && !crIsRunning)
      {
          StartCoroutine(IncreaseHappiness());
      }
	}

   IEnumerator IncreaseHappiness()
    {
        crIsRunning = true;

        do
        {
            for (int i = 0; i < visitors.Count; i++)
            {
                visitors[i].happiness = visitors[i].happiness + ror;
            }
            yield return new WaitForSecondsRealtime(increaseWaitTimer);
        }
        while (visitors.Count > 0);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        // add the appropriate type here
        if (other.GetComponent<Node_Visitor>())
        {
            visitors.Add(other.GetComponent<Node_Visitor>());
        }
        if (other.GetComponent<Node_Creature>())
        {
            creatures.Add(other.GetComponent<Node_Creature>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // add the appropriate type here
        if (other.GetComponent<Node_Visitor>())
        {
            visitors.Remove(other.GetComponent<Node_Visitor>());
        }
        if (other.GetComponent<Node_Creature>())
        {
            creatures.Remove(other.GetComponent<Node_Creature>());
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, NodeVisitorAoe);
    }


}
