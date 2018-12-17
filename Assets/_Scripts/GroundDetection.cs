using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(AddJumpingAngle.instance.inAir)
        {
            if(collision.gameObject.tag == "BodyCollider")
            {
                GameManager.instance.landed = true;
              //  StartCoroutine(GameManager.instance.GameOver("Game Over"));
            } 
            else if(collision.gameObject.tag == "HeadCollider")
            {
                
            }
        }
    }
}
