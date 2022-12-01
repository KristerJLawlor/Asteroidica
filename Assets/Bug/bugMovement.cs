using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bugMovement : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent myNav = null;
    Vector3 goal1;
    Vector3 goal2;
    Vector3 goal3;
    Vector3 goal4;
    Vector3 goal5;
    public int goal = 0;
    
    void Start()
    {
        myNav=this.gameObject.GetComponent<NavMeshAgent>();
        goal1 = GameObject.Find("Point1").transform.position;
        goal2 = GameObject.Find("Point2").transform.position;
        goal3 = GameObject.Find("Point3").transform.position;
        goal4 = GameObject.Find("Point4").transform.position;
        goal5 = GameObject.Find("Point5").transform.position;
        myNav.destination = goal2;
        
    }
    public IEnumerator Stall()
    {
        yield return new WaitForSeconds(5);
        myNav.isStopped = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (myNav.remainingDistance <= 0.1f)
        {
            myNav.isStopped = true;
            goal = Random.Range(0, 5);
            if (goal == 0)
            {
                myNav.destination = goal1;
                StartCoroutine(Stall());
            }
            if (goal == 1)
            {
                myNav.destination = goal2;
                StartCoroutine(Stall());
            }
            if (goal == 2)
            {
                myNav.destination = goal3;
                StartCoroutine(Stall());
            }
            if (goal == 3)
            {
                myNav.destination = goal4;
                StartCoroutine(Stall());
            }
            if (goal == 4)
            {
                myNav.destination = goal5;
                StartCoroutine(Stall());
            }
        }
    }
}
