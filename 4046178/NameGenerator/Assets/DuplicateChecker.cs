
using System.Collections.Generic;
using UnityEngine;

public class DuplicateChecker : MonoBehaviour
{
    private HashSet<string> generatedNames = new HashSet<string>();

    
    public bool IsDuplicate(string name)
    {
        return generatedNames.Contains(name);
    }

   
    public void AddName(string name)
    {
        generatedNames.Add(name);
    }
}

