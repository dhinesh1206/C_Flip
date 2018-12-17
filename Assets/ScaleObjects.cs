using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleObjects : MonoBehaviour {

    public Vector3 scaleTo;
    public float duration;
    public bool scaleDownOnPlay,destroy,disableOtherObjects;
    [ConditionalHide("disableOtherObjects", true)]
    public GameObject objectToDisable;

    private void Start()
    {
        if(scaleDownOnPlay)
        {
            StartScaling();
        }
    }

    public void StartScaling()
    {
        transform.DOScale(scaleTo, duration).SetEase(Ease.Linear).OnComplete(() => {
            if (disableOtherObjects)
            {
                objectToDisable.gameObject.SetActive(false);
            }
            if (destroy)
            {
                Destroy(transform.gameObject);
            }
           
        });
    }
}
