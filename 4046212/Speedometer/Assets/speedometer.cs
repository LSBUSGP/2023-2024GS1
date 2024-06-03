using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class speedometer : MonoBehaviour
{
    public Rigidbody2D Player;
    public Text speedValue;
    public float carSpeed = 0.0f;
    [Header("UI")]
    private double speed = 0.0f;
   
   
    void Update()
    {
        speed = Player.velocity.magnitude * 60 * 60 / 1609.344;

        if (speedValue != null){
            speedValue.text = ((int)speed) + "mph";
        }
    }
}
