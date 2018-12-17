using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollider : MonoBehaviour {

    public string tagName;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == tagName)
        {
            Destroy(transform.gameObject.GetComponent<Rigidbody>());
        }
    }
}
