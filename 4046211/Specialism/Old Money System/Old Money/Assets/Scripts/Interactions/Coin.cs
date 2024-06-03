using TMPro;
using UnityEngine;

public class Coin : Interactable
{
    [SerializeField] private float valueInPence; // Value of the coin.
    [SerializeField] private GameObject tooltip; // Tooltip object
    [SerializeField] private AudioClip coinPickupAudio; // Coin pickup audio

    private int x;

    public override void OnFocus() // When looking at the coin
    {
        // TO DO IF WANTED - Make it show the amound you will be getting in the tooltip!!!!
        tooltip.GetComponentInChildren<TextMeshProUGUI>().text = gameObject.name; // Set the tooltip text to the name of the coin
        tooltip.SetActive(true); // Show the tooltip
    }

    public override void OnLoseFocus() // When looked away from coin
    {
        tooltip.SetActive(false); // Hide the tooltip
    }

    public override void OnInteract() // When interacted with coin
    {
        UIManager.Instance.MoneyChangeNotification($"+1 {gameObject.name}", Color.green); // Shows the text

        GameManager.Instance.pence += valueInPence; // Add the value to the players money
        AudioManager.Instance.PlayAudio(coinPickupAudio); // Plays audio 
        tooltip.SetActive(false); // Hide the tooltip
        Destroy(gameObject); // Destroy the coin
    }

    public override void OnAltInteract()
    {
        x = x == 212 ? 35 : 212;
        transform.localRotation = Quaternion.Euler(x, transform.rotation.y, transform.rotation.z);
    }
}