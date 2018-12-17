using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeCameraAngle : MonoBehaviour
{

    //public float orthoCamSize;
    //public float camMovementTime;
    public CamPositions newCampositions;
    public bool changePosition, changeRotation, orthoSize;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "JumpingCube")
        {
            AddJumpingAngle.instance.gameObject.GetComponent<FollowCamera>().enabled = false;
            //StartCoroutine(AddJumpingAngle.instance.ChangeCameraAngle(LevelSetup.instance.endCameraAngle, LevelSetup.instance.endCameraPosition, LevelSetup.instance.thirdCamMovementTime, LevelSetup.instance.thirdCamDelay,false,true,false));
            if (changePosition && !changeRotation)
            {
                StartCoroutine(AddJumpingAngle.instance.ChangeCameraAngle(GameManager.instance.mainCam.transform.eulerAngles, newCampositions.camPosition, newCampositions.duration, newCampositions.delay, newCampositions.orthoSize, true, false));
            }
            else if (!changePosition && changeRotation)
            {
                StartCoroutine(AddJumpingAngle.instance.ChangeCameraAngle(newCampositions.camAngle, GameManager.instance.mainCam.transform.position, newCampositions.duration, newCampositions.delay, newCampositions.orthoSize, true, false));
            }
            else if (changePosition && changeRotation)
            {
                StartCoroutine(AddJumpingAngle.instance.ChangeCameraAngle(newCampositions.camAngle, newCampositions.camPosition, newCampositions.duration, newCampositions.delay, newCampositions.orthoSize, true, false));
            }

            if (!GameManager.instance.zoomedOut)
            {
                GameManager.instance.camMovement.Kill();
                GameManager.instance.camMovement.Append(GameManager.instance.mainCam.DOOrthoSize(newCampositions.orthoSize, newCampositions.duration));
            }
        }
    }
}