using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCondition : MonoBehaviour
{
    public string[] conditions;

    public int participantId;
    SceneDirector sceneDirector;
    public bool testing;
    // Start is called before the first frame update
    void Start()
    {
        sceneDirector = this.GetComponent<FindSM>().sceneD;
        if (testing)
        {
            participantId = 1;
        }
        else
        {
            participantId = sceneDirector.participantID;
        }

        sceneDirector.conditions = GetConditionFromCSV(participantId);
    }
    public string[] GetConditionFromCSV(int participantId)
    {

        // Load the appropriate CSV file based on the testing flag
        TextAsset csvFile = Resources.Load<TextAsset>("Schedule/Conditions");

        // Split the CSV file into lines
        string[] lines = csvFile.text.Split('\n');

        // Iterate through each line
        foreach (string line in lines)
        {
            // Split the line into conditions
            string[] conditions = line.Split('_');

            // Check if the first value matches the participantId
            if (conditions.Length > 0 && int.TryParse(conditions[0], out int id) && id == participantId && !testing)
            {
                Debug.Log(conditions);
                return conditions;

            }

        }

        // Return null if no matching row is found
        return null;
    }

}
