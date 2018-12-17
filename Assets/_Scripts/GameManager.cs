using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;
using System;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    //public GameObject retryPage;
    //public Text gameOverStatusText;
    [HideInInspector]
    public bool gameOver,landed,gameStarted,readytoJump;
    public float inAnimationSpeed,inCompressionRate, outAnimationSpeed,outCompressionRate;
    [HideInInspector]
    public GameObject playerInLevel,landingTarget;
    //public Animator playerAnimator;
    //public SkinnedMeshRenderer cubeMesh;
    //public float blendfrontValue,blendbackValue;

    [Header("UI Part")]
    public GameObject gameOverScreen;
    public GameObject commpletedScreen;
    public GameObject mainMenuScreen;
    public GameObject inGameScreen;
    public Text startCountText;
    public GameObject zoomButton;
    public Image fadeImage;
    public Image fillImage;
    public Image greenImage;
    public GameObject textPrefabUi;

    [Space(10)]
    [Header("Level Manager")]
    public float jumptocentervalue;
    public GameObject[] levelPrefabs;
    public int levelNumber;
    public GameObject currentLevel;
    public Sequence camMovement;

    [HideInInspector]
    public Camera mainCam;public bool zoomedOut;

    //public List<CamPositions> cameraPositions;
    //[HideInInspector]
    public List<CameraMovement> cameraSetup;
    public List<LevelCameraSetup> levelCameraSetups;public int currentCamPosition;public int currentCam;
    public GameObject textPrefab;

    GameObject cameraRotationTarget;
    bool camRotation;


    //[HideInInspector]
    public List<int> createdLevels;
    public int currentJumpingPad;

    public enum AdditionalObjectsType
    {
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }

    public List<ObjectShapeTypes> objectsPoints;
    float score;

    #region privateVariables
        Rigidbody playerrb;
    #endregion

    private void Start()
    {
        mainCam = Camera.main;
        //ObjectPointIncrement(AdditionalObjectsType.Large, new Vector3(0, 0, 0), Color.black);
    }

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }
	public IEnumerator FadeCamera(float fadeDelay,float fadeto, float fadeDuration)
	{
		fadeImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(fadeDelay);
		fadeImage.DOFade(fadeto, fadeDuration).SetEase(Ease.Linear);
	}

    public void ObjectPointIncrement(AdditionalObjectsType type,Vector3 worldPosition,Color textColor)
    {
        foreach (ObjectShapeTypes item in objectsPoints)
        {
            if(item.objectsType == type)
            {
                score += item.objectValue;
                //GameObject textElement = Instantiate(textPrefab);
                //textElement.transform.position = worldPosition;
                ////textElement.transform.LookAt(Camera.main.transform);
                ////textElement.transform.rotation = Quaternion.Euler(0, 180-textElement.transform.eulerAngles.y, 0);
                //textElement.transform.GetChild(0).GetComponent<TextMesh>().text = item.objectValue.ToString();
                //textElement.transform.GetChild(0).GetComponent<TextMesh>().color = textColor;
                //MoveScore(textElement);

                GameObject textElementUi = Instantiate(textPrefabUi, inGameScreen.transform);
                textElementUi.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
                textElementUi.GetComponent<Text>().text = item.objectValue.ToString();
                textElementUi.GetComponent<Text>().color = textColor;
                MoveScore(textElementUi);
            }
        }
    }

    public void MoveScore(GameObject scoreText)
    {
        scoreText.transform.DOMoveY(scoreText.transform.position.y + 200f, 2.5f);
        Destroy(scoreText, 2f);
        scoreText.GetComponent<Text>().DOFade(0, 2.5f);
        
    }


    public IEnumerator ChangeCameraAngle(int cameraPosition)
    {
        readytoJump = false; 
        FollowCamera.instance.StopFollowing();
        LevelCameraSetup levelProperty = levelCameraSetups[cameraPosition];
        if (levelProperty.cameraMovement )//--&& !levelProperty.rotateAround && !levelProperty.cameraFade
        {
            foreach (FadePoints item in levelProperty.cameramovementCoordinates.fadePoints)
            {
                StartCoroutine(FadeCamera(item.fadeDelay, item.fadeto, item.fadeDuration));
            }
            if (levelProperty.cameramovementCoordinates.addDelay)
            {
                yield return new WaitForSeconds(levelProperty.cameramovementCoordinates.delay);
                mainCam.transform.DOMove(levelProperty.cameramovementCoordinates.cameraPosition, levelProperty.cameramovementCoordinates.cameraMovementduration).SetEase(levelProperty.cameramovementCoordinates.easeType).OnComplete(() =>
                    {
                        if (levelProperty.cameramovementCoordinates.connectWIthNextAnimation)
                        {
                            StartCoroutine(ChangeCameraAngle(cameraPosition + 1));
                        }
                        if (levelProperty.cameramovementCoordinates.readyTojump)
                        {
                            fadeImage.gameObject.SetActive(false);
                            readytoJump = true; 
                            if (levelProperty.cameramovementCoordinates.followCamera)
                            {
                                FollowCamera.instance.StartFollowing();
                            }
                            landingTarget = levelProperty.cameramovementCoordinates.landingTarget;
                            landingTarget.SetActive(true);
                            if (playerInLevel.transform.eulerAngles.z > 100)
                            {
                                playerInLevel.transform.rotation = Quaternion.Euler(playerInLevel.transform.eulerAngles - new Vector3(0, 0, 180));
                            }
                            playerInLevel.transform.DOLookAt(new Vector3(landingTarget.transform.position.x, playerInLevel.transform.position.y, landingTarget.transform.position.z), 0.5f);
                            AddJumpingAngle.instance.target = levelProperty.cameramovementCoordinates.landingTarget.transform;
                            AddJumpingAngle.instance.upWardForce = levelProperty.cameramovementCoordinates.upForce;
                           // print(levelProperty.cameramovementCoordinates.distanceTocenter);
                            LevelSetup.instance.maxDistance = levelProperty.cameramovementCoordinates.maxDistance;
                        //LevelSetup.instance.startingDistance
                            LevelSetup.instance.startingDistance = levelProperty.cameramovementCoordinates.distanceToTargetsStartingPoint;
                            AddJumpingAngle.instance.ConfigureForceAndAngle();
                        }
                    });
                mainCam.transform.DORotate(levelProperty.cameramovementCoordinates.cameraAngle, levelProperty.cameramovementCoordinates.cameraRotationduration).SetEase(levelProperty.cameramovementCoordinates.easeType);
            }
            else
            {
                yield return new WaitForSeconds(0);
                mainCam.transform.DOMove(levelProperty.cameramovementCoordinates.cameraPosition, levelProperty.cameramovementCoordinates.cameraMovementduration).SetEase(levelProperty.cameramovementCoordinates.easeType).OnComplete(() =>
                {
                    if (levelProperty.cameramovementCoordinates.connectWIthNextAnimation)
                    {
                        StartCoroutine(ChangeCameraAngle(cameraPosition + 1));
                    }
                    if (levelProperty.cameramovementCoordinates.readyTojump)
                    {
                        fadeImage.gameObject.SetActive(false);
                        readytoJump = true;
                        if (levelProperty.cameramovementCoordinates.followCamera)
                        {
                            FollowCamera.instance.StartFollowing();
                        }
                        landingTarget = levelProperty.cameramovementCoordinates.landingTarget;
                        landingTarget.SetActive(true);
                        if (playerInLevel.transform.eulerAngles.z > 100)
                        {
                            playerInLevel.transform.rotation = Quaternion.Euler(playerInLevel.transform.eulerAngles - new Vector3(0, 0, 180));
                        }
                        playerInLevel.transform.DOLookAt(new Vector3(landingTarget.transform.position.x, playerInLevel.transform.position.y, landingTarget.transform.position.z), 0.5f);
                        AddJumpingAngle.instance.target = levelProperty.cameramovementCoordinates.landingTarget.transform;
                        AddJumpingAngle.instance.upWardForce = levelProperty.cameramovementCoordinates.upForce;
                       // print(levelProperty.cameramovementCoordinates.distanceTocenter);
                        LevelSetup.instance.maxDistance = levelProperty.cameramovementCoordinates.maxDistance;
                        LevelSetup.instance.startingDistance = levelProperty.cameramovementCoordinates.distanceToTargetsStartingPoint;
                        AddJumpingAngle.instance.ConfigureForceAndAngle();
                    }
                });
                mainCam.transform.DORotate(levelProperty.cameramovementCoordinates.cameraAngle, levelProperty.cameramovementCoordinates.cameraRotationduration).SetEase(levelProperty.cameramovementCoordinates.easeType);
            }
        }
        /*else if (!levelProperty.cameraMovement && levelProperty.rotateAround && !levelProperty.cameraFade)
        {
            CamRotateAround camrotation = levelProperty.cameraRotationCoordinates;
            mainCam.GetComponent<RotateAround>().StartRotating(camrotation.targetPoint, camrotation.direction, camrotation.speed, camrotation.delay, camrotation.duration, cameraPosition);
            if (camrotation.readyTojump)
            {
                fadeImage.gameObject.SetActive(false);
                landingTarget = camrotation.landingTarget;
                if (landingTarget)
                    landingTarget.SetActive(true);
                if (playerInLevel.transform.eulerAngles.z > 100)
                {
                    playerInLevel.transform.rotation = Quaternion.Euler(playerInLevel.transform.eulerAngles - new Vector3(0, 0, 180));
                }
                playerInLevel.transform.DOLookAt(new Vector3(landingTarget.transform.position.x, playerInLevel.transform.position.y, landingTarget.transform.position.z), 0.5f);
                AddJumpingAngle.instance.target = camrotation.landingTarget.transform;
                AddJumpingAngle.instance.upWardForce = camrotation.upForce;
                AddJumpingAngle.instance.maxDistance = camrotation.maxDistance;
                AddJumpingAngle.instance.ConfigureForceAndAngle();
            }
        }
        else if (!levelProperty.cameraMovement && !levelProperty.rotateAround && levelProperty.cameraFade)
        {
            FadeCamera fadeCamera = levelProperty.cameraFadeCoordinates;
            // fadeImage.DOFade(0, 0.1f);
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                mainCam.transform.position = fadeCamera.position;
                mainCam.transform.rotation = Quaternion.Euler(fadeCamera.rotation);
                fadeImage.DOFade(0.2f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        fadeImage.gameObject.SetActive(false);
                        if (fadeCamera.connectWithNextAnimation)
                        {
                            StartCoroutine(ChangeCameraAngle(cameraPosition + 1));
                        }
                        if (fadeCamera.readyTojump)
                        {
                            fadeImage.gameObject.SetActive(false);
                            readytoJump = true;
                            if (fadeCamera.followCamera)
                            {
                                FollowCamera.instance.StartFollowing();
                            }
                            landingTarget = fadeCamera.landingTarget;
                            landingTarget.SetActive(true);
                            if (playerInLevel.transform.eulerAngles.z > 100)
                            {
                                playerInLevel.transform.rotation = Quaternion.Euler(playerInLevel.transform.eulerAngles - new Vector3(0, 0, 180));
                            }
                            playerInLevel.transform.DOLookAt(new Vector3(landingTarget.transform.position.x, playerInLevel.transform.position.y, landingTarget.transform.position.z), 0.5f);
                            AddJumpingAngle.instance.target = fadeCamera.landingTarget.transform;
                            AddJumpingAngle.instance.upWardForce = fadeCamera.upForce;
                            LevelSetup.instance.maxDistance = fadeCamera.maxDistance;
                            AddJumpingAngle.instance.ConfigureForceAndAngle();
                      }
                });
            });
        }*/
    }

    public void CreateLevel()
    {
        DOTween.KillAll();
        StopAllCoroutines();
        if(currentLevel)
        {
            Destroy(currentLevel);
        }
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        commpletedScreen.SetActive(false);
        currentLevel = Instantiate(levelPrefabs[levelNumber]);
        //playerInLevel = currentLevel.GetComponent<LevelSetup>().playerInLevel;
        //levelCameraSetups = currentLevel.GetComponent<LevelSetup>().cameraPosition;
        // PlayerPrefs.DeleteAll();
        ////float newLevelPlayerPrefs = PlayerPrefs.GetInt("Level" + levelNumber, 0);
        //if (createdLevels.Contains(levelNumber))
        //{
        //    // Camera.main.transform.position = cameraPosition[SavedCameraPosition].cameramovementCoordinates.cameraPosition;
        //    // Camera.main.transform.rotation = Quaternion.Euler(cameraPosition[SavedCameraPosition].cameramovementCoordinates.cameraAngle);
        //    StartCoroutine(ChangeCameraAngle(currentLevel.GetComponent<LevelSetup>().SavedCameraPosition));
        //}
        //else
        //{
        //    Camera.main.transform.position =currentLevel.GetComponent<LevelSetup>().initialCameraPosition;
        //    Camera.main.transform.rotation = Quaternion.Euler(currentLevel.GetComponent<LevelSetup>().initialCameraAngle);
        //    StartCoroutine(ChangeCameraAngle(currentLevel.GetComponent<LevelSetup>().startingCameraPosition));
        //    createdLevels.Add(levelNumber);
        //}
        for (int i = 0; i < LevelSetup.instance.indicatingCircle.Length; i++)
        {
            string lastPosition = PlayerPrefs.GetString("LastPadPosition"+levelNumber.ToString() +i.ToString(), null);
           // print(lastPosition);
            if (lastPosition != null && lastPosition != "")
            {
                string[] positions = lastPosition.Split(' ');
                Vector3 position = new Vector3();
                position.x = float.Parse(positions[0]);
                position.y = float.Parse(positions[1]);
                position.z = float.Parse(positions[2]);
                LevelSetup.instance.indicatingCircle[i].transform.localPosition = position;
                if (i == 0)
                {
                    LevelSetup.instance.indicatingCircle[i].SetActive(true);
                }
            }
        }
        zoomButton.SetActive(false);
    }

    public void NextLevelCreate()
    {
        DOTween.KillAll();
        if (levelNumber+1 >= levelPrefabs.Length)
        {
            landed = false;
            levelNumber = 0;
            CreateLevel();
        }
        else
        {
            landed = false;
            levelNumber += 1;
            CreateLevel();
        }
    }

    //public void SwitchCam1()
    //{
    //    camMovement.Kill();
    //    zoomedOut = true;
    //    camMovement.Append(Camera.main.GetComponent<Camera>().DOOrthoSize(LevelSetup.instance.zoomoutAngle, LevelSetup.instance.zoomDuration))
    //               .Append(Camera.main.transform.DOMove(new Vector3(LevelSetup.instance.offsetfromcam,0,0)+Camera.main.transform.position,LevelSetup.instance.zoomDuration));
    //}

    //public void SwitchCam2()
    //{
    //    camMovement.Kill();
    //    zoomedOut = false;
    //}

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOverScreen()
    {
        //print("hello");
        if(!gameOverScreen.activeSelf)
            gameOverScreen.SetActive(true);
    }

    public void CompletedScreen()
    {
        commpletedScreen.SetActive(true);
    }

    public void RetryLevel()
    {
        zoomButton.SetActive(false);
        landed = false;
        gameOverScreen.SetActive(false);
        commpletedScreen.SetActive(false);
        CreateLevel();
    }

    public void GameStart()
    {
        mainMenuScreen.SetActive(false);
        gameStarted = true;
        CreateLevel();
    }

    #region ScoreManagement
    public void AddScoreFromFallingObjects()
    {

    }
    #endregion

}


[System.Serializable]
public class CamPositions
{
    //[Header("Camera Position Title")]
    //[Tooltip("Indicator To Differentiate camera Position/Angle")]
    //public GameManager.CameraPosition camPositionTitle;

    [Space(2)]
    [Header("CameraPosition")]
    [Tooltip("New camera position to which the camera should move")]
    public Vector3 camPosition;

    [Space(1)]
    [Header("CameraAngle")]
    [Tooltip("New camera Angle to which the camera should Rotate")]
    public Vector3 camAngle;

    [Space(2)]
    [Header("Time delay")]
    [Tooltip("Timedelay before the camera moves or rotates")]
    public float delay = 0;

    [Space(1)]
    [Header("Time duration")]
    [Tooltip("time taken for the camera to reach the endposition/angle")]
    public float duration = 0;

    [Space(1)]
    [Header("CameraOrthoSize")]
    [Tooltip("cameras new Ortho size to increase the viewing angle")]
    public float orthoSize = 10;

}

[System.Serializable]
public class LevelCameraSetup
{
    public bool cameraMovement;
    [ConditionalHide("cameraMovement",true)]
    public CamMovement cameramovementCoordinates;
    //public bool rotateAround;
    //[ConditionalHide("rotateAround", true)]
    //public CamRotateAround cameraRotationCoordinates;
    //public bool cameraFade;
	//[ConditionalHide("cameraFade", true)]
    //public FadeCamera cameraFadeCoordinates;
}

[System.Serializable]
public class CameraMovement
{
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    public bool addDelay;
    [ConditionalHide("addDelay", true)]
    public float delay;
    public float cameraMovementduration;
    public float cameraRotationduration;
    public bool connectWIthNextAnimation;
    public bool readyTojump;
    [ConditionalHide("readyTojump", true)]
    public ReadyTojump jumpingVelocitySetup;
}

[System.Serializable]
public class ReadyTojump
{
    //[ConditionalHide("readyTojump", true)]
    public bool followCamera;
    //[ConditionalHide("readyTojump", true)]
    public float angle;
    //[ConditionalHide("readyTojump", true)]
    public GameObject landingTargetGameobject;
    //[ConditionalHide("readyTojump", true)]
    public GameObject landingtoTargetStartingGameobject;
    //[ConditionalHide("readyTojump", true)]
    public GameObject landingtoMaxDistanceGameobject;
    public Ease easeType;

    [Space(30)]
    [Header("Velocity Calculation Points")]
    public Vector3 velocityForMaxDistance = Vector3.zero;
    public Vector3 velocityForCenter = Vector3.zero;
    public Vector3 velocityForStartingPoint = Vector3.zero;
    public float maxHeightVelocity;
}

[System.Serializable]
public class CamMovement 
{
    public Vector3 cameraPosition;
    public Vector3 cameraAngle;
    public bool addDelay;
	[ConditionalHide("addDelay", true)]
    public float delay;
    public float cameraMovementduration;
    public float cameraRotationduration;
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
    [ConditionalHide("readyTojump", true)]
    public float distanceTocenter;
    [ConditionalHide("readyTojump", true)]
    public float distanceToTargetsStartingPoint;
    //public Transform cameraZoomOutPosition;
    public Ease easeType;
	public bool fade;
	[ConditionalHide("fade")]
	public List<FadePoints> fadePoints;

} 

[System.Serializable]
public class CamRotateAround
{
    public Transform targetPoint;
    public float speed;
    public float duration;
    public Vector3 direction;
    public bool addDelay;
	[ConditionalHide("addDelay", true)]
    public float delay;
    public bool connectWithNextAnimation;
    public bool readyTojump;
    [ConditionalHide("readyTojump",true)]
    public bool followCamera;
    [ConditionalHide("readyTojump", true)]
    public float upForce; 
    [ConditionalHide("readyTojump", true)]
    public float maxDistance;
    [ConditionalHide("readyTojump", true)]
    public GameObject landingTarget;
}

[System.Serializable]
public class FadeCamera
{
    public Vector3 position;
    public Vector3 rotation;
    public bool addDelay;
	[ConditionalHide("addDelay", true)]
    public float delay;

    public bool connectWithNextAnimation;
    public bool readyTojump;
    [ConditionalHide("readyTojump", true)]
    public bool followCamera;
    [ConditionalHide("readyTojump", true)]
    public float upForce;
    [ConditionalHide("readyTojump", true)]
    public float maxDistance;
    [ConditionalHide("readyTojump", true)]
    public GameObject landingTarget;
}

[System.Serializable]
public class FadePoints
{
	public float fadeDelay, fadeDuration, fadeto;
}

[System.Serializable]
public class ObjectShapeTypes
{
    public GameManager.AdditionalObjectsType objectsType;
    public float objectValue;
}