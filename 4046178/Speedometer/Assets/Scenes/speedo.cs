using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Image imageNeedle;
    [SerializeField] private Slider sliderSpeed;
    [SerializeField] private Rigidbody2D rigidbody;

    private const float MPS_TO_MPH = 2.23694f; 

    private float currentSpeed = 0;
    private float targetSpeed = 0;
    private float needleSpeed = 100.0f;
    private const float MIN_SPEED = 0.0f;
    private const float MAX_SPEED = 180.0f;
    private const float NEEDLE_MIN_ANGLE = -118.0f;
    private const float NEEDLE_MAX_ANGLE = 180.0f;
    private const float ANGLE_RANGE = NEEDLE_MAX_ANGLE - NEEDLE_MIN_ANGLE;

    void Update()
    {
        if (targetSpeed != currentSpeed)
        {
            UpdateSpeed();
        }

       
        float speedMetersPerSecond = rigidbody.velocity.magnitude;
        float speedMilesPerHour = speedMetersPerSecond * MPS_TO_MPH;

      
        Debug.Log("Current Speed: " + speedMilesPerHour.ToString("F2") + " mph");

       
        SetNeedleRotation();

    }

    public void SetSpeedFromSlider()
    {
        targetSpeed = sliderSpeed.value;
    }

    public void SetSpeed(float speed)
    {
        targetSpeed = speed;
    }

    void UpdateSpeed()
    {

        float velocityMagnitudeMph = rigidbody.velocity.magnitude * MPS_TO_MPH;

        
        currentSpeed = Mathf.Clamp(velocityMagnitudeMph, MIN_SPEED, MAX_SPEED);


        targetSpeed = sliderSpeed.value;

       
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, Time.deltaTime * needleSpeed);

        SetNeedleRotation();
    }


    void SetNeedleRotation()
    {
        float normalizedSpeed = currentSpeed / MAX_SPEED;
        float angle = Mathf.Lerp(NEEDLE_MIN_ANGLE, NEEDLE_MAX_ANGLE, normalizedSpeed);
        imageNeedle.transform.localEulerAngles = new Vector3(0, 0, -angle);
    }
}

