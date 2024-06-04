using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TrainingAlgorithm
{
    public class NameGenerator : MonoBehaviour
    {
        private readonly HashSet<string> generatedNames = new();
        private Regex obscenityPattern;
        

        private readonly string[] vowels = { "a", "e", "i", "o", "u" };
        private readonly string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v", "w", "x", "y", "z" };
        private readonly string[] consonantClusters = { "bl", "br", "ch", "cl", "cr", "dr", "fl", "fr", "gr", "pl", "pr", "sl", "tr", "st", "sh" };

        public List<string> obscenities = new List<string> { "piss", "damn", "bollocks" };

        void Start()
        {

            var pattern = string.Join("|", obscenities);
            obscenityPattern = new Regex($"\\b({pattern})\\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            GenerateUniqueNames(500);
        }

        public void GenerateUniqueNames(int numberOfNames)
        {
            generatedNames.Clear();

            while (generatedNames.Count < numberOfNames)
            {
                string newName = GenerateName();

                if (!ContainsObscenity(newName) && generatedNames.Add(newName))
                    Debug.Log("Generated Name: " + newName);
            }

            Debug.Log("Generated " + generatedNames.Count + " unique names.");
        }

        private string GenerateName()
        {
            int syllableCount = UnityEngine.Random.Range(2, 4);
            string name = "";

            for (int i = 0; i < syllableCount; i++)
            {
                string consonant = UnityEngine.Random.value < 0.6 ? consonantClusters[UnityEngine.Random.Range(0, consonantClusters.Length)] : consonants[UnityEngine.Random.Range(0, consonants.Length)];
                string vowel = vowels[UnityEngine.Random.Range(0, vowels.Length)];
                string coda = i == syllableCount - 1 && UnityEngine.Random.value < 0.3 ? consonants[UnityEngine.Random.Range(0, consonants.Length)] : "";

                string syllable = consonant + vowel + coda;
                if (i == 0)
                    syllable = char.ToUpper(syllable[0]) + syllable.Substring(1);
                name += syllable;
            }

           
            bool includeNumber = UnityEngine.Random.value < 0.5;
            string number = includeNumber ? (UnityEngine.Random.value < 0.5 ? UnityEngine.Random.Range(10, 100).ToString() : UnityEngine.Random.Range(100, 1000).ToString()) : "";

            
            name += includeNumber ? number : "";

            return name;
        }

        private bool ContainsObscenity(string name) =>
            obscenityPattern.IsMatch(name);
    }
}
