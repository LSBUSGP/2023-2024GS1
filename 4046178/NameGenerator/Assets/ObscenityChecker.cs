using UnityEngine;

public class ObscenityChecker : MonoBehaviour
{
    
    private string[] bannedWords = { "cosmos", "void", "spaceship" }; 

   
    public bool ContainsObscenity(string name)
    {
        foreach (string word in bannedWords)
        {
            if (name.ToLower().Contains(word.ToLower()))
            {
                return true;
            }
        }
        return false;
    }
}

