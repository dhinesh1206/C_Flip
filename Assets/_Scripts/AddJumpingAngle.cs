using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class AddJumpingAngle : MonoBehaviour
{
    public Animation clipanimation;
    public static AddJumpingAngle instance;
    Rigidbody rb;
    Vector3 jumpingAngle;
    public Transform target;
    public float maxDistance,distanceToCenter,distanceTostartingPoint;
    public bool inAir;
    Animator playerAnimator;
    public float upWardForce;
    bool inSteppingStone;
    bool hit;
    Vector3 fullJumpingAngle;
    float tempdistance;
    bool intarget;
    public Transform currentJumpingPad;

    private void Awake()
    {
        instance = this;
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
        // LevelSetup.instance.offsetfromcam = transform.position.x - GameManager.instance.mainCam.transform.position.x;
    }

    void Start()
    {
        //upWardForce = LevelSetup.instance.upWardForce;
       //rb = gameObject.GetComponent<Rigidbody>();
        //jumpingAngle = Vector3.up * upWardForce;
       // foreach (CamPositions item in LevelSetup.instance.cameraPositions)
       // {
        //    if (item.camPositionTitle == GameManager.CameraPosition.PortraitPosition)
        //    {
        //        StartCoroutine(ChangeCameraAngle(item.camAngle, item.camPosition, item.duration, item.delay, item.orthoSize, true, true, true));
         //   }
        //}

       // rb.constraints = RigidbodyConstraints.FreezePositionX;
       // rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX ;
    }

    public void ConfigureForceAndAngle()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //upWardForce = LevelSetup.instance.upWardForce;
        maxDistance = LevelSetup.instance.maxDistance;
        distanceTostartingPoint = LevelSetup.instance.startingDistance;
        //float circleSize = (maxDistance / 2) - LevelSetup.instance.startingDistance;
        //print(circleSize);
        //print(maxDistance);
       // float percentageOfGreen = circleSize / (maxDistance/2);
        //float circleWidth =900 * (percentageOfGreen*2);
        //print(circleWidth);
        jumpingAngle = Vector3.zero;
        //jumpingAngle = Vector3.zero;
        fullJumpingAngle = jumpingAngle;
        // tempdistance = Vector3.Distance((fullJumpingAngle + (transform.forward * maxDistance)), ( jumpingAngle + (transform.forward * (maxDistance * 0.01f))));
        //tempdistance = (Vector3.Distance((fullJumpingAngle + (transform.forward * maxDistance)), (jumpingAngle * (maxDistance*0.01f))));
        // print(tempdistance);
        //GameManager.instance.greenImage.rectTransform.sizeDelta = new Vector2(circleWidth*2, circleWidth*2);
        tempdistance = 0;
       // initialPlayerPosition = transform.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (inAir)
        {
           // print(rb.velocity.magnitude);
            if (rb.velocity.magnitude < 0.1f)
            {
                GameManager.instance.GameOverScreen();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PickUpObjects")
        {
            GameManager.instance.ObjectPointIncrement(collision.gameObject.GetComponent<ScoreManagement>().objectScoreType, collision.gameObject.transform.position, collision.gameObject.GetComponent<ScoreManagement>().textColour);
            Time.timeScale = 0.5f;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (inAir)
        {
           // print(rb.angularVelocity.magnitude);
            float angle = transform.eulerAngles.z;
            if (((collision.gameObject.tag == "FinalPoint") && !GameManager.instance.landed && !hit))
            {
                GameManager.instance.landed = true;
                playerAnimator.SetFloat("Velocity", GameManager.instance.outAnimationSpeed);
                rb.angularVelocity = Vector3.zero;
                rb.velocity = rb.velocity / 2;
                if (angle > 330 || angle < 30 || (angle > 150 && angle < 210))
                {
                    transform.GetComponent<Rigidbody>().angularVelocity = transform.GetComponent<Rigidbody>().angularVelocity / 2;
                    transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    rb.centerOfMass = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                }
                StartCoroutine(CheckLevelCompletion(collision.gameObject));
            }
            else if (collision.gameObject.tag == "SteppingStone")
            {
                inSteppingStone = true;
                if (angle > 330 || angle < 30 || (angle > 150 && angle < 210))
                {
                    transform.GetComponent<Rigidbody>().angularVelocity = transform.GetComponent<Rigidbody>().angularVelocity / 2;
                    transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    rb.centerOfMass = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                    inAir = false;
                }
                else
                {
                    if (collision.gameObject.tag != "SteppingStone")
                    {
                        GameManager.instance.GameOverScreen();
                    }
                }
            }
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "FinalPoint"))
        {
           
            GameManager.instance.landed = false;
            //inAir = true;
        }
        if(other.gameObject.tag == "JumpingPad")
        {
            if (intarget)
            {
                intarget = false;
                //inAir = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inAir)
        {
            float angle = transform.eulerAngles.x;
            if ((other.gameObject.tag == "FinalPoint")&& !GameManager.instance.landed && !hit )
            {
                intarget = true;
                inAir = false;
                GameManager.instance.landed = true;
                playerAnimator.SetFloat("Velocity", GameManager.instance.outAnimationSpeed);
                rb.angularVelocity = Vector3.zero;
                rb.velocity = rb.velocity / 2;
               // print(angle);
                if (angle > 330 || angle < 30 || (angle > 150 && angle < 210))
                {
                    transform.GetComponent<Rigidbody>().angularVelocity = transform.GetComponent<Rigidbody>().angularVelocity /3;
                    transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    StartCoroutine(CheckLevelCompletion(other.gameObject, other.gameObject.GetComponent<ContinueCameraRotation>().nextCameraIndex));
                }
                else
                {
                    StartCoroutine(CheckLevelCompletion(other.gameObject, other.gameObject.GetComponent<ContinueCameraRotation>().nextCameraIndex));
                }
            }
        }
    }

    public IEnumerator CheckLevelCompletion(GameObject otherCollider,int nextCamIndex)
    {
        StopCoroutine(CheckLevelCompletion(otherCollider,0));
        GameManager.instance.readytoJump = false;
        yield return new WaitForSeconds(2f);
        float angle = transform.eulerAngles.x;
      // print(angle);
        if (angle > 330 || angle < 30 || (angle > 160 && angle < 200))
        {
            GameManager.instance.landed = false;
            GameManager.instance.readytoJump = true;
            inAir = false;
            //MoveTheJumpingPad(otherCollider,nextCamIndex);
            transform.DOJump(new Vector3(otherCollider.transform.position.x, transform.position.y, otherCollider.transform.position.z), GameManager.instance.jumptocentervalue, 1, 0.5f).OnComplete(() =>{
                MoveTheJumpingPad(otherCollider, nextCamIndex);
            });
        }
        else
        {
            GameManager.instance.GameOverScreen();
        }
    }

    public void MoveTheJumpingPad(GameObject otherCollider,int nextCamIndex)
    {
        if (otherCollider.GetComponent<AttachedTrampolin>())
        {
            GameObject otherObject = otherCollider.GetComponent<AttachedTrampolin>().affachedTrampolin;
            GameManager.instance.currentJumpingPad = otherCollider.GetComponent<AttachedTrampolin>().jumpingpadNumber;
            currentJumpingPad = otherObject.transform;
            otherObject.SetActive(true);
            //Destroy(otherCollider);
            otherObject.transform.DOMove(new Vector3(0, 3f, 0) + otherObject.transform.position, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                LevelSetup.instance.indicatingCircle[GameManager.instance.currentJumpingPad].SetActive(true);
                StartCoroutine(GameManager.instance.ChangeCameraAngle(nextCamIndex));
            });
        } 
        else
        {
            StartCoroutine(GameManager.instance.ChangeCameraAngle(nextCamIndex));
        }
    }

    void Update()
    {
        if(!inAir)
        {
            if(rb)
            if (rb.velocity.magnitude < 0.1f && intarget)
            {
                float angle = transform.eulerAngles.x;
                 //   print(angle);
                if(angle > 320 || angle < 40 || (angle > 140 && angle < 220))
                {
                   
                } 
                else 
                {
                    GameManager.instance.GameOverScreen();
                }
            }
        }

        if (GameManager.instance.gameStarted && !GameManager.instance.gameOver && !GameManager.instance.landed && GameManager.instance.readytoJump && !EventSystem.current.IsPointerOverGameObject())
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (!inAir)
                {
                    GameManager.instance.playerInLevel.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                }
                else
                    playerAnimator.ForceStateNormalizedTime(1);
            }
            if (Input.GetMouseButton(0))
            {
                if (!inAir)
                {
                   // print(tempdistance);
                    if (tempdistance < maxDistance)
                    {
                        tempdistance += maxDistance * 0.01f;
                        print(tempdistance);
                        if(tempdistance>distanceTostartingPoint)
                        {
                            // print(tempdistance);
                            //Shader shader = gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.shader;


                            gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.EnableKeyword("_EMISSION");
                            Invoke("DisableEmission", 0.5f);
                            //if(gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().GetSharedMaterials.)
                        }
                        //GameManager.instance.fillImage.fillAmount = tempdistance/maxDistance;
                        if (currentJumpingPad)
                        {
                            currentJumpingPad.transform.position -= new Vector3(0, 0.02f, 0);
                        }
                        jumpingAngle += transform.forward * (maxDistance*0.01f);
                    }
                    playerAnimator.SetFloat("Velocity", GameManager.instance.inAnimationSpeed / 2);
                }
                else
                {
                    playerAnimator.SetFloat("Velocity", GameManager.instance.inAnimationSpeed);
                    gameObject.transform.Rotate(Vector3.right * Time.deltaTime * 200);
                }
            }
            else if (Input.GetMouseButtonUp(0) && !GameManager.instance.landed && !GameManager.instance.gameOver)
            {
                playerAnimator.ForceStateNormalizedTime(1);
                playerAnimator.SetFloat("Velocity", GameManager.instance.outAnimationSpeed);
                if (!inAir)
                {
                    inAir = true;
                    GameManager.instance.playerInLevel.GetComponent<Rigidbody>().freezeRotation = false;
                    //if (!inSteppingStone)
                    //{
                    if (Mathf.Abs(maxDistance - tempdistance) <= maxDistance * 0.08f)
                    {
                    //    jumpingAngle = transform.forward * (maxDistance - (maxDistance * Random.Range(0.01f, 0.03f)));
                    }
                   // print(upWardForce);
                    jumpingAngle += new Vector3(0,upWardForce,0);
                    rb.velocity = jumpingAngle;                       
                    FollowCamera.instance.offset = transform.position - GameManager.instance.mainCam.transform.position;
                    FollowCamera.instance.StartFollowing();
                    // currentJumpingPad.GetComponent<Rigidbody>().isKinematic = false;
                    string position = currentJumpingPad.transform.localPosition.x.ToString() + " " + currentJumpingPad.transform.localPosition.y.ToString() + " " + currentJumpingPad.transform.localPosition.z.ToString();
                    PlayerPrefs.SetString("LastPadPosition" + GameManager.instance.levelNumber + GameManager.instance.currentJumpingPad.ToString(), position);
                    //print(PlayerPrefs.GetString("LastPadPosition" + GameManager.instance.levelNumber + GameManager.instance.currentJumpingPad.ToString()));
                    //}
                    //else
                    //{
                    //    rb.velocity = jumpingAngle / 1.5f;
                    //}
                }
            }
        }
    }

    public IEnumerator ChangeCameraAngle(Vector3 angle, Vector3 position, float time, float waitingTime, float orthoSizes, bool changePosition = false, bool changeRotation = false, bool readyTojump = false)
    {
        yield return new WaitForSeconds(waitingTime);
        GameManager.instance.mainCam.DOOrthoSize(orthoSizes, time);
        if (changePosition)
        {
            GameManager.instance.mainCam.transform.DOMove(position, time, false).SetEase(Ease.Linear);
        }
        if (changeRotation)
        {
            GameManager.instance.mainCam.transform.DORotate(angle, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                GameManager.instance.zoomButton.SetActive(true);
                GameManager.instance.readytoJump = readyTojump;
            });
        }
    }

    void DisableEmission()
    {
        gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.DisableKeyword("_EMISSION");
    }
}