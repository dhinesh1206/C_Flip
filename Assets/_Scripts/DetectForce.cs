using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectForce : MonoBehaviour 
{
    public static DetectForce instance;
    public Transform cylinderPosition;
    public GameObject otherObject;
   // Vector3 initialPosition,newPosition;
   // float difference;
  //  Vector3 impactForce;
    private void Awake()
    {
        instance = this;
    }

    public void DetectContactObject(GameObject other,Vector3 impactforce)
    {
        otherObject = other;
        print(impactforce);
        otherObject.GetComponent<Rigidbody>().velocity = impactforce;
    }
}
