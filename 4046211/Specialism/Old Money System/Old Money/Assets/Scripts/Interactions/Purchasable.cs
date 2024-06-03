using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Purchasable : Interactable
{
    [SerializeField] private int poundsValue; // Amount of pounds to show on tooltip
    [SerializeField] private int shillingsValue; // Amount of shillings to show on tooltip
    [SerializeField] private float penceValue; // Amount of pence to show on tooltip
    private float totalValueInPence; // Total value of the item in pence used for calculations

    [SerializeField] private GameObject tooltip; // Tooltip object

    [SerializeField] private AudioClip kaching;
    [SerializeField] private AudioClip insufficientFunds;

    private void Start()
    {
        totalValueInPence = GameManager.Instance.ConvertCoinsToPence(poundsValue, shillingsValue) + penceValue;
    }

    public override void OnFocus() // When looking at the coin
    {
        tooltip.GetComponentInChildren<TextMeshProUGUI>().text = $"{gameObject.name}\n{(poundsValue == 0 ? "" : $"£{poundsValue}/")}{(shillingsValue == 0 ? poundsValue != 0 && penceValue != 0 ? "-/" : "" : $"{shillingsValue}/")}{(penceValue == 0 && (poundsValue != 0 || shillingsValue != 0) ? "-" : $"{penceValue}")}{(poundsValue == 0 && shillingsValue == 0 ? "d" : "")}"; // Set the tooltip text to the name of the item
        tooltip.SetActive(true); // Show the tooltip
    }

    public override void OnLoseFocus() // When looked away from coin
    {
        tooltip.SetActive(false); // Hide the tooltip
    }

    public override void OnInteract()
    {
        if (GameManager.Instance.totalInPence < totalValueInPence) // If the player doesn't have enough money
        {
            //TooExpensive(); // Call this function
            UIManager.Instance.MoneyChangeNotification("Insufficient Funds", Color.red);
            AudioManager.Instance.PlayAudio(insufficientFunds); // Play thump audio
        }
        else // otherwise
        {
            GameManager.Instance.purchasing = true; // Set purchasing to true

            int p = GameManager.Instance.pounds; // This is the amount of pounds the player has
            int s = GameManager.Instance.shillings; // This is the amount of shillings the player has

            float pence = GameManager.Instance.ConvertCoinsToPence(p, s) + GameManager.Instance.pence - totalValueInPence; // Calculate the amount of pence the player will have left after buying the item
            GameManager.Instance.ResetMoney(); // Reset the players money

            GameManager.Instance.pence = pence; // Give the player the pence they have left over

            GameManager.Instance.purchasing = false; // Set purchasing to false to allow the money to update

            AudioManager.Instance.PlayAudio(kaching); // Play kaching audio

            UIManager.Instance.MoneyChangeNotification($"- {(poundsValue == 0 ? "" : $"£{poundsValue}/")}{(shillingsValue == 0 ? poundsValue != 0 && penceValue != 0 ? "-/" : "" : $"{shillingsValue}/")}{(penceValue == 0 && (poundsValue != 0 || shillingsValue != 0) ? "-" : $"{penceValue}")}{(poundsValue == 0 && shillingsValue == 0 ? "d" : "")}", Color.yellow);
            tooltip.SetActive(false); // Hide the tooltip
            Destroy(gameObject); // Destroy the object being purchased

        }
    }
    public override void OnAltInteract()
    {
    }
}