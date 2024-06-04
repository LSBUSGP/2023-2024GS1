using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicShuffler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<string> tracks = new List<string> { "Track 1", "Track 2", "Track 3", "Track 4", "Track 5", "Track 6", "Track 7", "Track 8", "Track 9", "Track 10" };

    void Start()
    {
        try
        {
            List<string> shuffledTracks = ShuffleTracks(tracks);
            if (shuffledTracks != null)
            {
                foreach (var track in shuffledTracks)
                {
                    Debug.Log($"Shuffled track: {track}");
                }
            }
            else
            {
                Debug.LogError("Failed to shuffle tracks. The list may be null or empty.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred during shuffling: {e.Message}");
        }
    }

    public List<string> ShuffleTracks(List<string> originalTracks)
    {
        // Error handling for null or empty list
        if (originalTracks == null || originalTracks.Count == 0)
        {
            Debug.LogError("List of tracks is null or empty.");
            return null;
        }

        try
        {
            // Create a copy of the original list to preserve it
            List<string> tracksToShuffle = new List<string>(originalTracks);

            int n = tracksToShuffle.Count;
            System.Random random = new System.Random();
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = tracksToShuffle[k];
                tracksToShuffle[k] = tracksToShuffle[n];
                tracksToShuffle[n] = value;
            }

            return tracksToShuffle;
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred during shuffling: {e.Message}");
            return null;
        }
    }
}
