using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBodyCollution : MonoBehaviour {

    public string bodyPartName;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FinalPoint")
        {
            print(collision.gameObject.tag+"           "+bodyPartName);
        }
        else if(collision.gameObject.tag == "Ground" )
        {
            print(collision.gameObject.tag + "           " + bodyPartName);
        }
    }
}
