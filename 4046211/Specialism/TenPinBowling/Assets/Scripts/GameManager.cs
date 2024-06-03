using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

// TRUE MEANS HIT
// FALSE MEANS MISS 

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject framePrefab;
    [SerializeField] private List<Transform> frameObjects;
    
    private TextMeshProUGUI nameText;
    private int currentFrame;
    private int currentPlayerFramePosition = 900;

    public List<string> playerGame;

    private int totalScore = 0;

    private void Start()
    {
        var dir = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, $"Game"));
        var fileInfo = dir.GetFiles("*.txt");

        for (int i = 1; i <= fileInfo.Length; i++)
        {
            currentFrame = 0;
            totalScore = 0;

            spawnFrame();
            populateFrames(fileInfo, i-1);
            scoreFrames();
        }
    }

    private void spawnFrame()
    {
        var currentPlayer = Instantiate(framePrefab, new Vector3(1100, currentPlayerFramePosition, 0), Quaternion.identity, canvas.transform);

        frameObjects = new List<Transform>();
        foreach (Transform frame in currentPlayer.transform.GetChild(0)) frameObjects.Add(frame);

        currentPlayerFramePosition -= 200;

        nameText = currentPlayer.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void populateFrames(FileInfo[] fileInfo, int currentPlayer)
    {
        string[] allFrames = File.ReadAllLines(fileInfo[currentPlayer].ToString());
        playerGame = new List<string>(allFrames);

        string namePath = Path.GetFileName(fileInfo[currentPlayer].ToString());
        string name = namePath.Remove(namePath.Length - 4);
        nameText.text = name;
    }

    private void scoreFrames() 
    {
        for (int i = 0; i < playerGame.Count; i += 2) 
        {
            int score = 0;

            score = scoreFrame(playerGame[i], i, score, "first");
            showFrame(currentFrame, score, "first");

            if (score != 10) 
            {
                int firstScore = score;
                score = scoreFrame(playerGame[i + 1], i + 1, score, "second");
                showFrame(currentFrame, score, firstScore);
            }

            else i--;

            showFrame(currentFrame, score, "total");
            currentFrame++;
        }

        if (totalScore > 300) totalScore = 300;
    }

    private int scoreFrame(string frame, int idx, int prevScore, string type)
    {
        int score = 0;
        foreach (char b in frame) if (b == '1') score++;

        if (score == 10 && type != "bonus")
        {
            if (type == "first") handleBonus(idx, 0, 2);
            if (type == "second") handleBonus(idx, 0, 1);
        } 

        if (score == 10 && prevScore == 10) totalScore += score;   
        else totalScore += score - prevScore;

        return score;
    }
  
    private void handleBonus(int idx, int score, int times)
    {
        for (int i = 0; i < times; i++)
        {
            try
            {
                idx++;
                score = scoreFrame(playerGame[idx], idx, score, "bonus");
            }
            catch (System.Exception e)
            {
                //Debug.Log($"{idx} : {e.Message}");
            }
        }
    }

    // Shows total and first throw
    private void showFrame(int idx, int score, string type)
    {
        if (type == "total")
        {
            var total = frameObjects[idx].Find("TotalScore").GetComponent<TextMeshProUGUI>();
            total.text = totalScore.ToString();
        }

        else if (type == "first")
        {
            if (score == 10)
            {
                var strike = frameObjects[idx].Find("InnerFrame").Find("Strike");
                strike.gameObject.SetActive(true);
            }
            else 
            {
                var first = frameObjects[idx].Find("Score").GetComponent<TextMeshProUGUI>();
                first.text = score.ToString();
            }
        }
    }

    // shows second throw
    private void showFrame(int idx, int score, int firstScore)
    {
        var innerFrame = frameObjects[idx].Find("InnerFrame");
        if (firstScore == score)
        {
            var miss = innerFrame.Find("Miss");
            miss.gameObject.SetActive(true);
        }
        else if (score == 10)
        {
            var spare = innerFrame.Find("Spare");
            spare.gameObject.SetActive(true);
        }
        else 
        {
            var second = innerFrame.Find("Score").GetComponent<TextMeshProUGUI>();
            second.text = (score - firstScore).ToString();
        }
    }
}