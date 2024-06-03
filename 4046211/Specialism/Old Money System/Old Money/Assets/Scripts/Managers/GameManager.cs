using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int pounds { get; private set; }
    public int shillings { get; private set; }
    public float pence;

    public float totalInPence { get; private set; }

    public bool purchasing = default;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Update()
    {
        totalInPence = ConvertCoinsToPence(pounds, shillings) + pence; // Gets the total value of the players money in pence 
        
        if (purchasing == false) // Don't allow any updates during purchasing
        {
            ConvertCoins(); // Converts coins

            if (pence < 12 && shillings < 20) UIManager.Instance.UpdateCoinUI(pounds, shillings, pence); // Updates UI for the coin 
        }
    }

    private void ConvertCoins() // Converts pence to shillings and shillings to pounds if needed.
    {
        if (pence >= 12) // If the player has 12 or more pence
        {
            pence -= 12; // Remove 12 pence
            shillings += 1; // Add a shilling
        }

        if (shillings >= 20) // If the player has 20 or more shillings
        {
            shillings -= 20; // Remove 20 shillings
            pounds += 1; // Add a pound
        }
    }

    public float ConvertCoinsToPence(int p, int s) // Converts money from pounds and shillings to pence
    {
        return (p * 240) + (s * 12); // 240p in a pounds and 12p to a shilling
    }

    public void ResetMoney() // Resets the players money to nothing
    {
        pounds = 0;
        shillings = 0;
        pence = 0;
        totalInPence = 0;
    }
}