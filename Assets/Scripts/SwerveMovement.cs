using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    public static SwerveMovement instance;

    private SwerveInputSystem _swerveInputSystem;

    [SerializeField] private float swerveSpeed = 0.5f;

    [SerializeField] private GameObject _getPoint;

    [SerializeField] private float _radius;

    public GameObject firstPaper;

    Vector3 centerPosition;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Update()
    {
        if (GameController.instance.isContinue == true)
        {
            centerPosition = _getPoint.transform.localPosition;
            float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
            firstPaper.transform.localPosition = new Vector3(firstPaper.transform.localPosition.x +  swerveAmount,1,0);

            float distance = Vector3.Distance(firstPaper.transform.localPosition, centerPosition);

            if (distance > _radius)
            {
                Vector3 fromOriginToObject = firstPaper.transform.localPosition - centerPosition;
                fromOriginToObject *= _radius / distance;
                firstPaper.transform.localPosition = centerPosition + fromOriginToObject;
                
            }
        }
       
    }
}
