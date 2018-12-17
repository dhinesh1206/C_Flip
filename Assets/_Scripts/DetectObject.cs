using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour 
{
    public Vector3 multiplyingFactor;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "JumpingCube")
        {
            print(collision.relativeVelocity);
            DetectForce.instance.DetectContactObject(collision.gameObject,new Vector3(collision.relativeVelocity.x*multiplyingFactor.x,-collision.relativeVelocity.y*multiplyingFactor.y,0));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "JumpingCube")
        {
            DetectForce.instance.otherObject = null;
        }
    }
}
