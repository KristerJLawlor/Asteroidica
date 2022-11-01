 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class RotateOverTime : MonoBehaviour {
   public Vector3 rotationDirection;
   private float smooth;
 
   // Use this for initialization
   void Start () {
   
   }
 
   // Update is called once per frame
   void Update () {
     transform.Rotate(rotationDirection * Time.deltaTime);
   }
}
 