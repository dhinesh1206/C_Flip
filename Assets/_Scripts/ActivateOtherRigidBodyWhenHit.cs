using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOtherRigidBodyWhenHit : MonoBehaviour {

    public string othercolliderTag;
    public Rigidbody[] collidersToactivate;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == othercolliderTag)
        {
            foreach(Rigidbody rb in collidersToactivate)
            {
                rb.isKinematic = false;
            }
        }
    }
}
