using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeCameraAngle : MonoBehaviour
{

    public bool lookAt;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "JumpingCube")
        {
            AddJumpingAngle.instance.gameObject.GetComponent<FollowCamera>().enabled = false;
            if(lookAt)
            {
                Camera.main.transform.DOLookAt(GameManager.instance.playerInLevel.transform.position, 0.1f).OnComplete(() =>
                {
                    Camera.main.gameObject.GetComponent<LookAtPlayer>().lookAt = true;
                });
            }
        }
    }
}