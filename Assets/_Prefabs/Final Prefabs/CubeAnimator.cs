using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CubeAnimator : MonoBehaviour {

    public CubeJoints topJoint, bottomJoint;
    public float animTime;


    [ContextMenu("ResetPosition")]
    public void ResetAll()
    {
         TopPointZero(animTime);
        TopBendRight(animTime);
        TopBendLeft(animTime);
       //   BottomPointZero(animTime);
       // BottomPointZero(animTime);
    }



    public void TopPointZero(float totalAnimTime)
    {
        topJoint.baseJoint.DOLocalMove(Vector3.zero, totalAnimTime,false);
        topJoint.baseJoint.DOLocalRotate(new Vector3(0, 0, 270), totalAnimTime, RotateMode.Fast).OnComplete(() =>
          {
              topJoint.bodyJoint1.DOLocalMove(new Vector3(-0.040f, 0, 0), totalAnimTime, false);
              topJoint.bodyJoint1.DOLocalRotate(new Vector3(180, 0, 1.140991f), totalAnimTime, RotateMode.Fast);

         //     topJoint.bodyJoint1.DOLocalRotateQuaternion(Quaternion.Euler(180, 0, 1.140991f), totalAnimTime);

              //topJoint.bodyJoint2.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
              //topJoint.bodyJoint2.DOLocalRotate(new Vector3(0, 0, 0), totalAnimTime, RotateMode.Fast);

              //topJoint.headJoint.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
              //topJoint.headJoint.DOLocalRotate(new Vector3(0, 180, 270), totalAnimTime, RotateMode.Fast);
          });
    }


    public void TopBendRight(float totalAnimTime)
    {
        topJoint.baseJoint.DOLocalMove(new Vector3(0.01f,0,0), totalAnimTime, false);
        topJoint.baseJoint.DOLocalRotate(new Vector3(0, 0, -110), totalAnimTime, RotateMode.Fast);

        topJoint.bodyJoint1.DOLocalMove(new Vector3(-0.040f, 0, 0), totalAnimTime, false);
        topJoint.bodyJoint1.DOLocalRotate(new Vector3(-180, 0, 25), totalAnimTime, RotateMode.Fast);

        topJoint.bodyJoint2.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
        topJoint.bodyJoint2.DOLocalRotate(new Vector3(0, 0, 25), totalAnimTime, RotateMode.Fast);

        topJoint.headJoint.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
        topJoint.headJoint.DOLocalRotate(new Vector3(0, 180, -90), totalAnimTime, RotateMode.Fast);
    }


    public void TopBendLeft(float totalAnimTime)
    {
        topJoint.baseJoint.DOLocalMove(new Vector3(-0.01f, 0, 0), totalAnimTime, false);
        topJoint.baseJoint.DOLocalRotate(new Vector3(0, 0, -70 ), totalAnimTime, RotateMode.Fast);

        topJoint.bodyJoint1.DOLocalMove(new Vector3(-0.040f, 0, 0), totalAnimTime, false);
        topJoint.bodyJoint1.DOLocalRotate(new Vector3(-180, 0, -25), totalAnimTime, RotateMode.Fast);

        topJoint.bodyJoint2.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
        topJoint.bodyJoint2.DOLocalRotate(new Vector3(0, 0, -25), totalAnimTime, RotateMode.Fast);

        topJoint.headJoint.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
        topJoint.headJoint.DOLocalRotate(new Vector3(0, 180, -90), totalAnimTime, RotateMode.Fast);
    }





    public void BottomPointZero(float totalAnimTime)
    {
        topJoint.baseJoint.DOLocalMove(Vector3.zero, totalAnimTime, false);
        topJoint.baseJoint.DOLocalRotate(new Vector3(0, 0, 90), totalAnimTime, RotateMode.Fast);

        topJoint.bodyJoint1.DOLocalMove(new Vector3(-0.040f, 0, 0), totalAnimTime, false);
        topJoint.bodyJoint1.DOLocalRotate(new Vector3(-180, 0, 1.140991f), totalAnimTime, RotateMode.Fast);

        topJoint.bodyJoint2.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
        topJoint.bodyJoint2.DOLocalRotate(new Vector3(0, 0, 0), totalAnimTime, RotateMode.Fast);

        topJoint.headJoint.DOLocalMove(new Vector3(-0.049f, 0, 0), totalAnimTime, false);
        topJoint.headJoint.DOLocalRotate(new Vector3(0, 180, -90), totalAnimTime, RotateMode.Fast);
    }







    [System.Serializable]
    public class CubeJoints
    {
        public Transform baseJoint, bodyJoint1, bodyJoint2, headJoint;
    }
}
