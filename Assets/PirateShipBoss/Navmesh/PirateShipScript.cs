using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PirateShipScript : MonoBehaviour
{
    //List of checkpoints for the ship's navigation
    public Vector3 goal1;
    public Vector3 goal2;
    public Vector3 goal3;
    public Vector3 goal4;

    //The player's location
    public Vector3 PlayerLocation;
    public NavMeshAgent myNav = null;

    //keep track of the next goal to travel to
    public int goal = 1;

    // Start is called before the first frame update
    void Start()
    {
        myNav = this.gameObject.GetComponent<NavMeshAgent>();
        goal1 = GameObject.Find("Goal1").transform.position;
        goal2 = GameObject.Find("Goal2").transform.position;
        goal3 = GameObject.Find("Goal3").transform.position;
        goal4 = GameObject.Find("Goal4").transform.position;

        //set the initial destination to the first goal
        myNav.destination = goal1;

        //Setting isStopped to false replaces .Resume()
        myNav.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myNav.remainingDistance == 0 && goal == 1)
        {
            Debug.Log("Setting goal to 2");
            goal = 2;
            myNav.destination = goal2;
            myNav.isStopped = false;
        }
        else if (myNav.remainingDistance == 0 && goal == 2)
        {
            Debug.Log("Setting goal to 3");
            goal = 3;
            myNav.destination = goal3;
            myNav.isStopped = false;
        }
        else if (myNav.remainingDistance == 0 && goal == 3)
        {
            Debug.Log("Setting goal to 4");
            goal = 4;
            myNav.destination = goal4;
            myNav.isStopped = false;
        }
        else if (myNav.remainingDistance == 0 && goal == 4)
        {
            Debug.Log("Setting goal to 1");
            goal = 1;
            myNav.destination = goal1;
            myNav.isStopped = false;
        }
    }
}
