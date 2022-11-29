using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PirateShipScript : MonoBehaviour
{
    public Rigidbody myRig;

    //List of checkpoints for the ship's navigation
    public Vector3 goal1;
    public Vector3 goal2;
    public Vector3 goal3;
    public Vector3 goal4;
    public Vector3 goal5;
    public Vector3 goal6;
    public Vector3 goal7;
    public Vector3 goal8;

    //The player's location
    public Vector3 PlayerLocation;
    public NavMeshAgent myNav = null;

    //keep track of the next goal to travel to
    public int goal = 1;

    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();

        myNav = this.gameObject.GetComponent<NavMeshAgent>();
        goal1 = GameObject.Find("Goal1").transform.position;
        goal2 = GameObject.Find("Goal2").transform.position;
        goal3 = GameObject.Find("Goal3").transform.position;
        goal4 = GameObject.Find("Goal4").transform.position;
        goal5 = GameObject.Find("Goal5").transform.position;
        goal6 = GameObject.Find("Goal6").transform.position;
        goal7 = GameObject.Find("Goal7").transform.position;
        goal8 = GameObject.Find("Goal8").transform.position;

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
            Debug.Log("Setting goal to 5");
            goal = 5;
            myNav.destination = goal5;
            myNav.isStopped = false;
        }
        else if (myNav.remainingDistance == 0 && goal == 5)
        {
            Debug.Log("Setting goal to 6");
            goal = 6;
            myNav.destination = goal6;
            myNav.isStopped = false;
        }
        else if (myNav.remainingDistance == 0 && goal == 6)
        {
            Debug.Log("Setting goal to 7");
            goal = 7;
            myNav.destination = goal7;
            myNav.isStopped = false;
        }
        else if (myNav.remainingDistance == 0 && goal == 7)
        {
            Debug.Log("Setting goal to 8");
            goal = 8;
            myNav.destination = goal8;
            myNav.isStopped = false;
        }
        else if (myNav.remainingDistance == 0 && goal == 8)
        {
            Debug.Log("Setting goal to 1 again");
            goal = 1;
            myNav.destination = goal1;
            myNav.isStopped = false;
        }
    }
}
