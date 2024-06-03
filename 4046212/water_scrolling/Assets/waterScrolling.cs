using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class waterScrolling : MonoBehaviour
{
    public Material water;
    public float speed = -0.5f;
    public Vector2 Flow_Direction = new Vector2 (0, 1);
    public Vector2 uvCoordinates;


    void Start()
    {
        uvCoordinates = water.GetTextureOffset("_MainTex");
    }

    
    void Update()
    {
        uvCoordinates += Flow_Direction * speed * Time.deltaTime;
        water.SetTextureOffset("_MainTex", uvCoordinates);
    }
}
