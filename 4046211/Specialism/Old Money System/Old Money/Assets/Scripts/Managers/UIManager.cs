using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText; // Text for the amount of money the player has

    [SerializeField] private GameObject notifierSpawnpoint; // Spawnpoint for money change notification text
    [SerializeField] private TextMeshProUGUI moneyChangeNotifier; // Money change notification text prefab

    public static UIManager Instance; // Singleton

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void UpdateCoinUI(int pounds, int shillings, float pence)
    {
        coinText.text = $"{(pounds == 0 ? "" : $"£{pounds}/")}{(shillings == 0 ? pounds != 0 && pence != 0 ? "-/" : "" : $"{shillings}/")}" +
            $"{(pence == 0 && (pounds != 0 || shillings != 0) ? "-" : $"{pence}")}" +
            $"{(pounds == 0 && shillings == 0 ? "d" : "")}"; // Change the UI to update to the players new amount 
    }

    public void MoneyChangeNotification(string text, Color color)
    {
        TextMeshProUGUI notification = Instantiate(moneyChangeNotifier, notifierSpawnpoint.transform.position, notifierSpawnpoint.transform.rotation, notifierSpawnpoint.transform); // Spawns the notification text
        notification.color = color; // Make the text the color passed in
        notification.text = text; // Set the text content
    }
}
