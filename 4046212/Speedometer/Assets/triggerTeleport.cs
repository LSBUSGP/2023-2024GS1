using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class triggerTeleport : MonoBehaviour
{
    public GameObject Trigger1;
  void OnTriggerEnter2D(Collider2D other)
  {

    Debug.Log("Hit");
    transform.position = Trigger1.transform.position;
  
  }

}
