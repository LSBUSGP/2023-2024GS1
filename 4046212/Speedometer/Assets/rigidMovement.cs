using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class rigidMovement : MonoBehaviour
{
    public Rigidbody2D rb;
   [SerializeField] float speed = 10f;
   [SerializeField] GameObject Player;
   public double milesPerHour;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        double metersInMiles = 1609.344f;
        milesPerHour = rb.velocity.magnitude * 60 * 60 / metersInMiles;
        Debug.Log(milesPerHour);
    }
    
    void FixedUpdate()
    {
        float moveSpeed = Input.GetAxis("Vertical") * speed;
        transform.Translate(0, moveSpeed, 0);
        rb.velocity = new Vector2(0, moveSpeed);
    }
}
