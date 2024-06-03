using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Transform radarlineTransform;
    private float rotationSpeed;
    private float radarDistance;
    private void Awake()
    {
        radarlineTransform = transform.Find("radarline");
        rotationSpeed = 150f;
        radarDistance = 9.9f;
    }

    private void Update ()
    
    {  
        radarlineTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, GetVectorFromAngle(radarlineTransform.eulerAngles.z), radarDistance);
        if(raycastHit2D.collider != null)
        { 
            raycastHit2D.collider.transform.GetComponent<enemyColor>().Hit();
            Debug.Log("Hit");
        }
    }
     public static Vector3 GetVectorFromAngle(float angle)
    
    {
        //a conversion that I found on the internet as I was very stuck on how to get the raycast to work against the radarline
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}