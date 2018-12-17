using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileShoot : MonoBehaviour 
{

    [SerializeField]
    Transform target;

    [SerializeField]
    float initialAngle;

    [SerializeField]
    Vector3 maxVelocity;

    Vector3 velocity;

    [ContextMenu("Jump")]
    void Jump()
    {
        print(Vector3.Distance(target.transform.position, transform.position));
       // transform.LookAt(target);
        Rigidbody rigid = gameObject.GetComponent<Rigidbody>();

        Vector3 p = target.position;

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x,0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;
        //print(yOffset);
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        //print(initialVelocity);

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
        //print(velocity);
        // Rotate our velocity to match the direction between the two objects

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
        //print(angleBetweenObjects);
       // print(new Vector3(velocity.x, maxVelocity.y, velocity.z));
        Vector3 finalVelocity = Quaternion.AngleAxis(-angleBetweenObjects, Vector3.up) * new Vector3(velocity.x,velocity.y,velocity.z);
        print(finalVelocity);
       // rigid.velocity = finalVelocity;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(Mathf.Abs(velocity.y) < Mathf.Abs(maxVelocity.y))
            {
                velocity.y += (maxVelocity.y) * 0.01f;
            }
            if(Mathf.Abs( velocity.x) < Mathf.Abs( maxVelocity.x))
            {
                velocity.x += (maxVelocity.x) * 0.01f;
            }
            if (Mathf.Abs( velocity.z) < Mathf.Abs( maxVelocity.z))
            {
                velocity.z += (maxVelocity.z) * 0.01f;
            }
            //print(velocity);
        }
        if(Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<Rigidbody>().velocity = velocity;
        }
        if(Input.GetMouseButton(1))
        {
            SceneManager.LoadScene(1);
        }
    }
}