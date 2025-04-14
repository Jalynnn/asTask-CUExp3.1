using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using VRQuestionnaireToolkit;
using System.IO;


public class SceneDirector : MonoBehaviour
{
    public TextAsset csvFile;
    static SceneDirector instance;
    // Input Devices to check for grabbing
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    ExperimentLog expLog;
    private int sceneBars;
    static Scene tempScene;
    [HideInInspector] public string tempSceneName;
    public int trialNumber = 1;
    public int shapeNumber = 1;
    public bool firstWait = true;
    [SerializeField]
    public int[] schedule;
    public int stepCounter = 0;
    public int participantID;
    public ExperimentType experimentType;
    public string[] conditions;
    [HideInInspector] public ExperimentType initialType;
    [HideInInspector] public int prevMentalTLX;
    [HideInInspector] public int tlxDifference;
    public bool testing = false;
    public bool[] StepDisplay = new bool[5] { true, true, true, true, true };
    [HideInInspector] public int scaffoldsRemoved = 0;
    public enum ExperimentType
    {
        ExpA,
        ExpB,
        Usability,
        Germane
    }

    private void Awake()
    {

    }

    private void Start()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        expLog = instance.GetComponent<ExperimentLog>();
        participantID = expLog.participantNumber;
        initialType = experimentType;
        conditions = GetConditionFromCSV(participantID);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find your player and populate the data like e.g.
        if (testing)
        {
            GameObject.FindWithTag("XRRig").GetComponent<ContinuousMovement>().enabled = true;

            GameObject menu = GameObject.FindWithTag("menu");
            foreach (Transform child in menu.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        string condition = conditions[shapeNumber];
        if (string.Equals(condition.Trim(), "li", StringComparison.OrdinalIgnoreCase))
        {
            GameObject.FindWithTag("SceneInstructions").GetComponent<invisInstructions>().GermaneHighLoad = false;

        }

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
            if (conditions.Length > 0 && int.TryParse(conditions[0], out int id) && id == participantId)
            {
                Debug.Log(conditions);
                return conditions;
            }

        }

        // Return null if no matching row is found
        return null;
    }
    public void LoadScenesBasedOnConditions()
    {
        StepDisplay = new bool[5] { true, true, true, true, true };
        if (shapeNumber < 1 || shapeNumber > conditions.Length)
        {
            Debug.LogWarning("Invalid shapeNumber: " + shapeNumber);
            return;
        }

        string condition = conditions[shapeNumber];


        switch (shapeNumber)
        {
            case 1:
                // if (string.Equals(condition.Trim(), "li", StringComparison.OrdinalIgnoreCase))
                // {

                // }
                SceneManager.LoadScene("A");
                // else
                // {
                //     SceneManager.LoadScene("A");
                // }
                break;

            case 2:
                if (string.Equals(condition.Trim(), "li", StringComparison.OrdinalIgnoreCase))
                {
                    SceneManager.LoadScene("B_LIn_LEx");
                }
                else if (string.Equals(condition.Trim(), "hi", StringComparison.OrdinalIgnoreCase))
                {
                    SceneManager.LoadScene("B_HiIn_LEx");
                }
                else
                {
                    Debug.LogWarning("Unknown condition for shape 2: " + condition);
                }
                break;

            case 3:
                if (string.Equals(condition.Trim(), "li", StringComparison.OrdinalIgnoreCase))
                {
                    SceneManager.LoadScene("C_LIn_LEx");
                }
                else if (string.Equals(condition.Trim(), "hi", StringComparison.OrdinalIgnoreCase))
                {
                    SceneManager.LoadScene("C_HiIn_LEx");
                }
                else
                {
                    Debug.LogWarning("Unknown condition for shape 3: " + condition);
                }
                break;

            case 4:
                if (string.Equals(condition.Trim(), "li", StringComparison.OrdinalIgnoreCase))
                {
                    SceneManager.LoadScene("D_LIn_LEx");
                }
                else if (string.Equals(condition.Trim(), "hi", StringComparison.OrdinalIgnoreCase))
                {
                    SceneManager.LoadScene("D_HiIn_LEx");
                }
                else
                {
                    Debug.LogWarning("Unknown condition for shape 4: " + condition);
                }
                break;

            default:
                Debug.LogWarning("Invalid shape number: " + shapeNumber);
                break;
        }
    }

    public void resetType()
    {
        experimentType = initialType;
    }
    private void Update()
    {

        // if (Input.GetKey(KeyCode.Alpha2))
        // {
        //     if (Input.GetKeyDown(KeyCode.A))
        //     {
        //         LoadSceneByName("A_LIn_LEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.B))
        //     {
        //         LoadSceneByName("B_LIn_LEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.C))
        //     {
        //         LoadSceneByName("C_LIn_LEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.D))
        //     {
        //         LoadSceneByName("D_LIn_LEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         LoadSceneByName("E_LIn_LEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.F))
        //     {
        //         LoadSceneByName("F_LIn_LEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.G))
        //     {
        //         LoadSceneByName("G_LIn_LEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.H))
        //     {
        //         LoadSceneByName("H_LIn_LEx");
        //     }
        // }

        // if (Input.GetKey(KeyCode.Alpha3))
        // {
        //     if (Input.GetKeyDown(KeyCode.A))
        //     {
        //         LoadSceneByName("A_HiIn_Lex");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.B))
        //     {
        //         LoadSceneByName("B_HiIn_Lex");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.C))
        //     {
        //         LoadSceneByName("C_HiIn_Lex");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.D))
        //     {
        //         LoadSceneByName("D_HiIn_Lex");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         LoadSceneByName("E_HiIn_Lex");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.F))
        //     {
        //         LoadSceneByName("F_HiIn_Lex");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.G))
        //     {
        //         LoadSceneByName("G_HiIn_Lex");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.H))
        //     {
        //         LoadSceneByName("H_HiIn_Lex");
        //     }
        // }

        // if (Input.GetKey(KeyCode.Alpha6))
        // {
        //     if (Input.GetKeyDown(KeyCode.A))
        //     {
        //         LoadSceneByName("A_HiIn_HiEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.B))
        //     {
        //         LoadSceneByName("B_HiIn_HiEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.C))
        //     {
        //         LoadSceneByName("C_HiIn_HiEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.D))
        //     {
        //         LoadSceneByName("D_HiIn_HiEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         LoadSceneByName("E_HiIn_HiEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.F))
        //     {
        //         LoadSceneByName("F_HiIn_HiEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.G))
        //     {
        //         LoadSceneByName("G_HiIn_HiEx");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.H))
        //     {
        //         LoadSceneByName("H_HiIn_HiEx");
        //     }
        // }

        // if (Input.GetKey(KeyCode.Alpha4))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LoadSceneByName("A_LIn_HiEx");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                LoadSceneByName("B_LIn_HiEx");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LoadSceneByName("C_LIn_HiEx");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                LoadSceneByName("D_LIn_HiEx");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                LoadSceneByName("E_LIn_HiEx");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                LoadSceneByName("F_LIn_HiEx");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                LoadSceneByName("G_LIn_HiEx");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                LoadSceneByName("H_LIn_HiEx");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.name == "WaitingRoom")
                LoadScenesBasedOnConditions();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            testing = !testing;
        }


    }

    public int[] GetNumbersFromCSV(bool testing, int participantId)
    {
        if (!testing) { csvFile = Resources.Load<TextAsset>("Schedule/Yoke"); }
        else { csvFile = Resources.Load<TextAsset>("Schedule/YokeTest"); }

        string[] HiInes = csvFile.text.Split('\n');
        foreach (string HiIne in HiInes)
        {
            string[] values = HiIne.Split(',');
            if (values.Length > 0 && int.TryParse(values[0], out int id) && id == participantId)
            {
                int[] numbers = new int[values.Length - 1];
                for (int i = 1; i < values.Length; i++)
                {
                    if (int.TryParse(values[i], out int number))
                    {
                        numbers[i - 1] = number;
                    }
                }
                return numbers;
            }
        }
        return null; // Return null if no matching row is found
    }


    public void ClearPreviews()
    {
        GameObject[] previews = GameObject.FindGameObjectsWithTag("Preview");
        foreach (GameObject preview in previews)
        {
            Destroy(preview);
        }

    }


    public int[] DetermineTrack()
    {


        int[][] possibleTracks = new int[][]
        {
            new int[] { 0, 1, 2, 3 },
            new int[] { 1, 3, 0, 2 },
            new int[] { 3, 2, 1, 0 },
            new int[] { 2, 0, 3, 1 }
        };

        int trackIndex = DataStorage.ParticipantID % 4;

        return possibleTracks[trackIndex];
    }


    public void LogInstructionTimes()
    {
        DataStorage.LastInstructionSceneStartTime = DataStorage.MostRecentSceneStartTime;
        DataStorage.LastInstructionSceneEndTime = System.DateTime.Now.ToString();
        DataStorage.LastInstructionSceneElapsedTime = Time.timeSinceLevelLoad.ToString();

    }


    public void LogExperimentStartTime()
    {
        DataStorage.ExperimentStartTime = System.DateTime.Now;
    }

    public void LogExperimentEndTime()
    {
        this.GetComponent<ExperimentLog>().AddData("Trial", "ended");
    }


    public void ResetTrackStep()
    {
        DataStorage.CurrentTrackStep = 0;
    }
    void resetView()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevices(devices);
        if (devices.Count != 0)
        {
            devices[0].subsystem.TryRecenter();
        }

    }

    public void OpenParticipantIDScene()
    {
        SceneManager.LoadScene("ParticipantIDScene");
    }

    public void OpenExperimentVersion()
    {
        SceneManager.LoadScene("ExperimentVersion");


    }
    public void OpenTutorialBuildScene()
    {
        SceneManager.LoadScene("Tutorial (Build)");

    }
    public void OpenTutorialVideoScene()
    {
        SceneManager.LoadScene("Tutorial Video");

    }

    public void resetTime()
    {
        expLog.time_s = 0;
    }
    public bool RepeatCheck()
    {
        if (experimentType != ExperimentType.ExpA)
        {
            if (schedule[stepCounter] == 0)
            {
                Debug.Log("Does not repeat." + schedule[stepCounter]);
                return false;
            }
            else
            {
                Debug.Log("Repeats " + schedule[stepCounter] + " times.");
                schedule[stepCounter]--;
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    public void LoadNextTrialScene()
    {
        expLog.time_s = 0;
        trialNumber++;
        tempScene = SceneManager.GetActiveScene();
        tempSceneName = tempScene.name;
        if (trialNumber == 9)
        {
            shapeNumber++;
            //.Condition = tempSceneName;
            SceneManager.LoadScene("WaitingRoom");
            trialNumber = 1;

        }
        else if (trialNumber == 7)
        {
            Debug.Log("Loading Waiting Room");
            Debug.Log(tempScene.name);
            SceneManager.LoadSceneAsync("WaitingRoomTrial");
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
    public void LoadSceneByName(string scenename)
    {
        expLog.time_s = 0;
        SceneManager.LoadScene(scenename);
    }

    public void LoadTempScene()
    {
        expLog.time_s = 0;
        Debug.Log("Loading Temp Scene");
        Debug.Log(tempSceneName);
        trialNumber++;
        SceneManager.LoadScene(tempSceneName);
    }
    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Application.Quit();
    }
    public string getCurrentShape()
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        string[] splitSceneName = sceneName.Split('_');
        return splitSceneName[0];
    }
    public string getCondition()
    {
        return conditions[shapeNumber];
    }


}
