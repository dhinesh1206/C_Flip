using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelCompletionDetection : MonoBehaviour 
{
    //public SkinnedMeshRenderer testblend;
    float blendValue;

    private void Start()
    {
        //testblend.SetBlendShapeWeight(1, 100);
    }

    private void Update()
    {
       // testblend.SetBlendShapeWeight(1, Time.deltaTime * 1000);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (AddJumpingAngle.instance.inAir)
        {
           /* if (collision.contacts[0].otherCollider.name == "Head")
            {
                //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //collision.gameObject.GetComponent<Rigidbody>().mass = 100;
                collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Invoke("Restart", 2f);
            }*/

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    void Restart()
    {
        GameManager.instance.landed = true;
    //    StartCoroutine(GameManager.instance.GameOver("Level Cleared"));
    }


}
