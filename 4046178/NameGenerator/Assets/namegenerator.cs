using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class namegenerator : MonoBehaviour
{
    [SerializeField] private TextAsset nouns;
    [SerializeField] private TextAsset adjectives;

    [SerializeField] private GameObject nounscontainer;
    [SerializeField] private GameObject adjectivescontainer;

    [SerializeField] private Button generatebutton;

    private TextMeshProUGUI[] adjectivetext;
    private TextMeshProUGUI[] nountext;

    public ObscenityChecker obscenityChecker;
    public DuplicateChecker duplicateChecker; 


    void Start()
    {
       for (int i = 0; i < 10; i++)
        {

            generatebox(nounscontainer, i);
            generatebox(adjectivescontainer, i);
        }
        adjectivetext = adjectivescontainer.GetComponentsInChildren<TextMeshProUGUI>();
        nountext = nounscontainer.GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void generatebox(GameObject container, int value)
    {
        GameObject box = new GameObject($"box {value}");
        box.transform.parent = container.transform;
        box.transform.localPosition = new Vector3(0, -475 + (value * 100), 0);
        box.transform.localScale = new Vector3(1, 1, 1);
        box.AddComponent<TextMeshProUGUI>();
        box.GetComponent<TextMeshProUGUI>().enableWordWrapping = false;
        box.GetComponent<TextMeshProUGUI>().alignment =TextAlignmentOptions.Center;

            


    }

    public void generatename()
    {
        nounscontainer.transform.localPosition = new Vector3(nounscontainer.transform.localPosition.x, 475);
        adjectivescontainer.transform.localPosition = new Vector3(nounscontainer.transform.localPosition.x, 475);
        generatebutton.interactable = false;

        for (int i = 0; i < 10; i++)
        {
            string adjective = returnword(adjectives).ToUpper();
            string noun = returnword(nouns).ToUpper();

            
            if (duplicateChecker.IsDuplicate(adjective + " " + noun))
            {
                Debug.LogWarning("duplicate name found: " + adjective + " " + noun);
                continue; 
            }

            
            if (obscenityChecker.ContainsObscenity(adjective) || obscenityChecker.ContainsObscenity(noun))
            {
                Debug.LogWarning("obscene name found: " + adjective + " " + noun);
                continue; 
            }

            
            duplicateChecker.AddName(adjective + " " + noun);

            
            adjectivetext[i].text = adjective;
            nountext[i].text = noun;

           
            string usernameoutput = $"{adjective} {noun}";
            Debug.Log("Generated username: " + usernameoutput);
        }

        StartCoroutine(anitemateroll(0, false, adjectivescontainer));
        StartCoroutine(anitemateroll(0.5f, true, nounscontainer));

        generatebutton.interactable = true;
    }





    private string returnword(TextAsset file)
    {

        string[] lines = file.text.Split('\n');
        string line = lines[Random.Range(0, lines.Length - 1)];
        return line.Substring(0, line.Length - 1);
        

    }

    private IEnumerator anitemateroll(float delay,bool isfinal, GameObject container)
    {

        yield return new WaitForSecondsRealtime(delay);
        for (int i=0; i <= 225; i++)
        {

            container.transform.localPosition = new Vector3(container.transform.localPosition.x, container.transform.localPosition.y - 4f);
            yield return new WaitForSecondsRealtime(0.005f);
        }

        if (isfinal)
        {
            generatebutton.interactable = true;
        }

    }
}
