using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyColor : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] Color32 originalColor = new Color32 (255, 0, 0, 0);
    [SerializeField] Color32 detectedColor = new Color32 (255, 0, 0, 255);
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

  public void Hit()
  {
    sr.color = detectedColor;
    Invoke("RestoreColor", 1f);
  }

  private void RestoreColor()
  {
    sr.color = originalColor;
  }
}
