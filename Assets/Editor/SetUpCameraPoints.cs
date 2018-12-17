//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using DG.Tweening;

//public class SetUpCameraPoints : ScriptableWizard
//{
//    [Space(10)]
//    [Header("Camera Position Movement")]
//    GameObject levelSelected;

//    public GameObject player;
//    public bool cameraMovement;
//   // [SerializeField] bool cameraMovement;
//    //[ConditionalHide("cameraMovement", true)]
//    [SerializeField]
//    float cameraMovementduration;
//    //[ConditionalHide("cameraMovement", true)]
//    [SerializeField]
//    float cameraRotationduration;
//    //[ConditionalHide("cameraMovement", true)]
//    [SerializeField]
//    Ease easeType;

//    [Space(10)]
//    [SerializeField] bool addDelay;
//    [ConditionalHide("addDelay", true)]
//    [SerializeField] float delayTime;
//    [SerializeField] bool connectWIthNextAnimation;
//    [SerializeField] bool readyTojump;
//    [ConditionalHide("readyTojump", true)]
//    [SerializeField] ReadyTojump jumpingCoordinates;

//    //[SerializeField] Transform cameraZoomOutPosition;



//    [Space(20)]
//    [Header("Update Preference")]
//    [SerializeField] bool createNextCamposition;
//    [SerializeField] bool insertCameraPosition;
//    [ConditionalHide("insertCameraPosition", true)]
//    [SerializeField] int indexNumber;
//    [SerializeField] bool replaceCameraPosition;
//    [ConditionalHide("replaceCameraPosition", true)]
//    [SerializeField] int replacingindexnumber;

//    [HideInInspector]
//    [SerializeField] LevelCameraSetup newCameraPosition = new LevelCameraSetup();
//    //[HideInInspector]
//    [SerializeField] CameraMovement newCamMovement = new CameraMovement();

//    [MenuItem("My Tools/Update CameraPoints...")]
//    static void SelectAllOfTagWizard()
//    {
//        ScriptableWizard.DisplayWizard<SetUpCameraPoints>("CameraPositionUpdate", "Update Camera Position");
//    }

//    void OnWizardUpdate()
//    {
//        ReadyTojump readyTojump = newCamMovement.jumpingVelocitySetup;
//        //    newCameraPosition.cameramovementCoordinates.cameraPosition = Camera.main.transform.position;
//        //    newCameraPosition.cameramovementCoordinates.cameraAngle = Camera.main.transform.eulerAngles;
//        //    newCameraPosition.cameramovementCoordinates.addDelay = addDelay;
//        //    newCameraPosition.cameramovementCoordinates.connectWIthNextAnimation = connectWIthNextAnimation;
//        //    newCameraPosition.cameramovementCoordinates.delay = delayTime;
//        //    newCameraPosition.cameramovementCoordinates.readyTojump = readyTojump;
//        //newCameraPosition.cameramovementCoordinates.followCamera = jumpingCoordinates.followCamera;
//        ////newCameraPosition.cameramovementCoordinates.upForce = jumpingCoordinates,upForce;
//        ////newCameraPosition.cameramovementCoordinates.maxDistance = jumpingCoordinates.maxDistance;
//            //newCameraPosition.cameramovementCoordinates.cameraMovementduration = cameraMovementduration;
//            //newCameraPosition.cameramovementCoordinates.cameraRotationduration = cameraRotationduration;
//            //newCameraPosition.cameramovementCoordinates.landingTarget = landingTarget;
//            //newCameraPosition.cameramovementCoordinates.easeType = easeType;
//        if(cameraMovement )
//        {
//            readyTojump = jumpingCoordinates;
//            if (readyTojump.angle > 0 && readyTojump.angle < 90)
//            {
//                if (readyTojump.landingtoMaxDistanceGameobject)
//                {
//                    readyTojump.velocityForMaxDistance = FindJumpingAngle(readyTojump.landingtoMaxDistanceGameobject, readyTojump.angle, player);
//                    readyTojump.maxHeightVelocity = readyTojump.velocityForMaxDistance.y;
//                }
//                if (readyTojump.landingTargetGameobject)
//                {
//                    readyTojump.velocityForCenter = FindJumpingAngle(readyTojump.landingTargetGameobject, readyTojump.angle, player);
//                }
//                if (readyTojump.landingtoTargetStartingGameobject)
//                {
//                    readyTojump.velocityForStartingPoint = FindJumpingAngle(readyTojump.landingtoTargetStartingGameobject, readyTojump.angle, player);
//                }
//            }

//        }
//    }


//    void OnWizardCreate()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        levelSelected = GameObject.FindGameObjectWithTag("LevelSetUp");
       
//        List<LevelCameraSetup> levelCamera = levelSelected.GetComponent<LevelSetup>().cameraPosition;    
//        if (createNextCamposition && !insertCameraPosition)
//        {
//            levelCamera.Add(newCameraPosition);
//        }
//        else if (!createNextCamposition && insertCameraPosition)
//        {
//            levelCamera.Insert(indexNumber, newCameraPosition);
//        }

//        if(replaceCameraPosition)
//        {
//            levelCamera[replacingindexnumber] = newCameraPosition;
//        }
//    }

//    Vector3 FindJumpingAngle(GameObject target,float initialAngle,GameObject player)
//    {
//        //Rigidbody rigid = gameObject.GetComponent<Rigidbody>();

//        Vector3 p = target.transform.position;

//        float gravity = Physics.gravity.magnitude;
//        // Selected angle in radians
//        float angle = initialAngle * Mathf.Deg2Rad;

//        // Positions of this object and the target on the same plane
//        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
//        Vector3 planarPostion = new Vector3(player.transform.position.x, 0, player.transform.position.z);

//        // Planar distance between objects
//        float distance = Vector3.Distance(planarTarget, planarPostion);
//        // Distance along the y axis between objects
//        float yOffset = player.transform.position.y - p.y;
//        //print(yOffset);
//        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

//        //print(initialVelocity);

//        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
//        //print(velocity);
//        // Rotate our velocity to match the direction between the two objects

//        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
//        //print(angleBetweenObjects);
//        // print(new Vector3(velocity.x, maxVelocity.y, velocity.z));
//        Vector3 finalVelocity = Quaternion.AngleAxis(-angleBetweenObjects, Vector3.up) * new Vector3(velocity.x, velocity.y, velocity.z);

//        return finalVelocity;
//        //rigid.velocity = finalVelocity;
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class SetUpCameraPoints : ScriptableWizard
{
    [Space(10)]
    [Header("Camera Position Movement")]
    GameObject levelSelected;
    public bool cameraMovement;
    //[ConditionalHide("cameraMovement", true)]
    public float cameraMovementduration;
    //[ConditionalHide("cameraMovement", true)]
    public float cameraRotationduration;
    //[ConditionalHide("cameraMovement", true)]
    public Ease easeType;

    //[Space(10)]
    //[Header("Rotation Around a Object")]
    //public bool cameraRotateAround;
    //[ConditionalHide("cameraRotateAround",true)]
    //public Transform targetpoint;
    //[ConditionalHide("cameraRotateAround", true)]
    //public float speed;
    //[ConditionalHide("cameraRotateAround", true)]
    //public float duration;
    //[ConditionalHide("cameraRotateAround", true)]
    //public Vector3 direction;
    //[Space(10)]
    //[Header("Camera FadeIn/Out")]
    //public bool cameraFade;

    [Space(10)]
    [Header("Common Property")]
    public bool addDelay;
    [ConditionalHide("addDelay", true)]
    public float delayTime;
    public bool connectWIthNextAnimation;
    public bool readyTojump;
    [ConditionalHide("readyTojump", true)]
    public bool followCamera;
    [ConditionalHide("readyTojump", true)]
    public float upForce;
    [ConditionalHide("readyTojump", true)]
    public float maxDistance;
    [ConditionalHide("readyTojump", true)]
    public GameObject landingTarget;

    //public Transform cameraZoomOutPosition;



    [Space(20)]
    [Header("Update Preference")]
    public bool createNextCamposition;
    public bool insertCameraPosition;
    [ConditionalHide("insertCameraPosition", true)]
    public int indexNumber;
    public bool replaceCameraPosition;
    [ConditionalHide("replaceCameraPosition", true)]
    public int replacingindexnumber;

    [HideInInspector]
    public LevelCameraSetup newCameraPosition = new LevelCameraSetup();

    [MenuItem("My Tools/Update CameraPoints...")]
    static void SelectAllOfTagWizard()
    {
        ScriptableWizard.DisplayWizard<SetUpCameraPoints>("CameraPositionUpdate", "Update Camera Position");
    }

    void OnWizardUpdate()
    {
        // newCameraPosition.cameraFade = cameraFade;
        newCameraPosition.cameraMovement = cameraMovement;
        // newCameraPosition.rotateAround = cameraRotateAround;
        if (cameraMovement)
        {
            newCameraPosition.cameramovementCoordinates.cameraPosition = Camera.main.transform.position;
            newCameraPosition.cameramovementCoordinates.cameraAngle = Camera.main.transform.eulerAngles;
            newCameraPosition.cameramovementCoordinates.addDelay = addDelay;
            newCameraPosition.cameramovementCoordinates.connectWIthNextAnimation = connectWIthNextAnimation;
            // newCameraPosition.cameramovementCoordinates.cameraZoomOutPosition = cameraZoomOutPosition;
            newCameraPosition.cameramovementCoordinates.delay = delayTime;
            newCameraPosition.cameramovementCoordinates.readyTojump = readyTojump;
            newCameraPosition.cameramovementCoordinates.followCamera = followCamera;
            newCameraPosition.cameramovementCoordinates.upForce = upForce;
            newCameraPosition.cameramovementCoordinates.maxDistance = maxDistance;
            newCameraPosition.cameramovementCoordinates.cameraMovementduration = cameraMovementduration;
            newCameraPosition.cameramovementCoordinates.cameraRotationduration = cameraRotationduration;
            newCameraPosition.cameramovementCoordinates.landingTarget = landingTarget;
            newCameraPosition.cameramovementCoordinates.easeType = easeType;
        }
        /*else if (cameraRotateAround)
        {
            newCameraPosition.cameraRotationCoordinates.targetPoint = targetpoint;
            newCameraPosition.cameraRotationCoordinates.speed = speed;
            newCameraPosition.cameraRotationCoordinates.direction = direction;
            newCameraPosition.cameraRotationCoordinates.duration = duration;
            newCameraPosition.cameraRotationCoordinates.addDelay = addDelay;
            newCameraPosition.cameramovementCoordinates.connectWIthNextAnimation = connectWIthNextAnimation;
            //newCameraPosition.cameraRotationCoordinates.cameraZoomOutPosition = cameraZoomOutPosition;
            newCameraPosition.cameraRotationCoordinates.delay = delayTime;
            newCameraPosition.cameraRotationCoordinates.readyTojump = readyTojump;
            newCameraPosition.cameraRotationCoordinates.followCamera = followCamera;
            newCameraPosition.cameraRotationCoordinates.upForce = upForce;
            newCameraPosition.cameraRotationCoordinates.maxDistance = maxDistance;
            //newCameraPosition.cameraRotationCoordinates.cameraMovementduration = cameraMovementduration;
            //newCameraPosition.cameraRotationCoordinates.cameraRotationduration = cameraRotationduration;
            newCameraPosition.cameraRotationCoordinates.landingTarget = landingTarget;
            // newCameraPosition.cameraRotationCoordinates.easeType = easeType;
        }
        else if (cameraFade)
        {
            //addDelay = true;
            newCameraPosition.cameraFadeCoordinates.position = Camera.main.transform.position;
            newCameraPosition.cameraFadeCoordinates.rotation = Camera.main.transform.eulerAngles;
            newCameraPosition.cameraFadeCoordinates.addDelay = addDelay;
            newCameraPosition.cameraFadeCoordinates.connectWithNextAnimation = connectWIthNextAnimation;
            //newCameraPosition.cameraRotationCoordinates.cameraZoomOutPosition = cameraZoomOutPosition;
            newCameraPosition.cameraFadeCoordinates.delay = delayTime;
            newCameraPosition.cameraFadeCoordinates.readyTojump = readyTojump;
            newCameraPosition.cameraFadeCoordinates.followCamera = followCamera;
            newCameraPosition.cameraFadeCoordinates.upForce = upForce;
            newCameraPosition.cameraFadeCoordinates.maxDistance = maxDistance;
          
            newCameraPosition.cameraFadeCoordinates.landingTarget = landingTarget;
        }*/


    }


    void OnWizardCreate()
    {
        levelSelected = GameObject.FindGameObjectWithTag("LevelSetUp");
        List<LevelCameraSetup> levelCamera = levelSelected.GetComponent<LevelSetup>().cameraPosition;
        if (createNextCamposition && !insertCameraPosition)
        {
            levelCamera.Add(newCameraPosition);
        }
        else if (!createNextCamposition && insertCameraPosition)
        {
            levelCamera.Insert(indexNumber, newCameraPosition);
        }

        if (replaceCameraPosition)
        {
            levelCamera[replacingindexnumber] = newCameraPosition;
        }
    }
}
