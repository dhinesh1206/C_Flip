using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour 
{
    public Vector3 initialCameraPosition,initialCameraAngle;
    public static LevelSetup instance;
    //[HideInInspector]
    public float upWardForce;
    //[HideInInspector]
    public float maxDistance;
    //[HideInInspector]
    public float startingDistance;
    public GameObject playerInLevel;
    public int startingCameraPosition;
    public int savedCameraPosition;
    public List<LevelCameraSetup> cameraPosition;
    public List<CameraMovement> cameraSetup;
    public GameObject[] indicatingCircle;

    [HideInInspector]
    public float offsetfromcam;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.instance.playerInLevel = playerInLevel;
        GameManager.instance.levelCameraSetups = cameraPosition;
        GameManager.instance.cameraSetup = cameraSetup;
        Camera.main.transform.position = initialCameraPosition;
        Camera.main.transform.rotation = Quaternion.Euler(initialCameraAngle);
        StartCoroutine(GameManager.instance.ChangeCameraAngle(startingCameraPosition));
        // PlayerPrefs.SetInt("Level" + GameManager.instance.levelNumber, 1);
        GameManager.instance.createdLevels.Add((GameManager.instance.levelNumber));       

       // PlayerPrefsX
    }
}
