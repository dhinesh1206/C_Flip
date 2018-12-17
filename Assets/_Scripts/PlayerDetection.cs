using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour 
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            Invoke("PrintTest", 3f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Head")
        {
            CancelInvoke();
        }
    }

    void PrintTest()
    {
        print("LevelComplete");
    }
}
