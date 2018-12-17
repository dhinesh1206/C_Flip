using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraAngle : MonoBehaviour {

    public int cameraAngle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "JumpingCube")
        {
            StartCoroutine(GameManager.instance.ChangeCameraAngle(cameraAngle));
            Destroy(gameObject);
        }
    }
}
