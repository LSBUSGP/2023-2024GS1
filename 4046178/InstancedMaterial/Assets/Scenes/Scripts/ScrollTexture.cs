using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScrollTexture : MonoBehaviour
{
    private Image image;
    private Material instantiatedMaterial; 

    [SerializeField] private Vector2 speed;

    void Start()
    {
        image = GetComponent<Image>();
        instantiatedMaterial = new Material(image.material); 
        image.material = instantiatedMaterial; 
    }

    void Update()
    {
        image.material.mainTextureOffset += speed * Time.deltaTime;
    }

    
    private void OnDestroy()
    {
        
        if (instantiatedMaterial != null)
        {
            Destroy(instantiatedMaterial);
        }
    }
}
