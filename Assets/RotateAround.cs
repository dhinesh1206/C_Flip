using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    Transform rotationPoint;
    float speed;
    Vector3 direction;
    float duration;

    bool rotate;

    private void Update()
    {
        if(rotate)
        {
            transform.LookAt(rotationPoint.transform.position);
            transform.Translate(direction * Time.deltaTime *speed);
        }
    }

    public void StartRotating(Transform centerPoint,Vector3 dir, float time, float delay, float duraTion,int nextCamIndex)
    {
        rotationPoint = centerPoint;
        speed = time;
        direction = dir;
        duration = duraTion;
        StartCoroutine(Rotate(delay));
        //StartCoroutine(StopRotation(duraTion, nextCamIndex));
    }

    public IEnumerator Rotate(float delay)
    {
        yield return new WaitForSeconds(delay);
        rotate = true;
    }

    //public IEnumerator StopRotation(float delay,int camIndex)
    //{
    //    yield return new WaitForSeconds(delay);
    //    rotate = false;
    //    if (GameManager.instance.levelCameraSetups[camIndex].cameraRotationCoordinates.connectWithNextAnimation)
    //    {
    //        StartCoroutine(GameManager.instance.ChangeCameraAngle(camIndex + 1));
    //    }
    //    if (GameManager.instance.levelCameraSetups[camIndex].cameraRotationCoordinates.readyTojump)
    //    {
    //        GameManager.instance.readytoJump = true;
    //        if (GameManager.instance.levelCameraSetups[camIndex].cameraRotationCoordinates.followCamera)
    //        {
    //            FollowCamera.instance.StartFollowing();
    //        }
    //    }
    //}

}
