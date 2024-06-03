using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    private float enemySpeed = 5f;
    private float minTime = 1f;
    private float maxTime = 3f;

    private Vector3 direction;
    private float timer;
   
    private void Start()
    {
        direction = Random.insideUnitSphere;
        timer = Random.Range(minTime, maxTime);
    }
    private void Update()
    {
        transform.Translate(direction * enemySpeed * Time.deltaTime);
        timer -= Time.deltaTime;

        if(timer <= 0f)
        {
            direction = Random.insideUnitSphere;
            timer = Random.Range(minTime, maxTime);
        }
    }
}
