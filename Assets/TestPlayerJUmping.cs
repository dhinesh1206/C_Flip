using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerJUmping : MonoBehaviour 
{
    public float upForce;
    public float maxDistance;
    public Transform Target;

    Vector3 playerInitialPosition, playerInitialRotation;

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
    private void Start()
    {
        playerInitialRotation = transform.eulerAngles;
        playerInitialPosition = transform.position;
    }


    [ContextMenu("Jump")]
    void TestJump()
    {
        transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
        transform.GetComponent<Rigidbody>().velocity = (transform.forward * maxDistance) + new Vector3(0, upForce, 0);
    }

    [ContextMenu("Restart")]
    void ResetPosition()
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = playerInitialPosition;
        transform.rotation = Quaternion.Euler(playerInitialRotation);
    }
}
