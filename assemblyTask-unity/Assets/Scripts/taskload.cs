using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class taskload : MonoBehaviour
{
     int scoreMental;
     int scorePhysical;
    public TextMeshPro value;
    public Slider tlx;
    SceneDirector sceneDir;
    GameObject sceneDirObj;
     int scoreJOL;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        sceneDirObj = this.GetComponent<FindSM>().sceneDirectorObject;
        sceneDir = sceneDirObj.GetComponent<SceneDirector>();
    }
    // Update is called once per frame



    void Update()
    {

    }
    public void SaveMentalTaskLoad()
    {
        scoreMental = int.Parse(value.text);
    }
    public void SavePhysicalTaskLoad()
    {
        scorePhysical = int.Parse(value.text);
    }
    public void SaveJOL()
    {
        scoreJOL = int.Parse(value.text);
    }
    
    public void logTLX()
    {
        sceneDirObj = this.GetComponent<FindSM>().sceneDirectorObject;
        sceneDirObj.GetComponent<ExperimentLog>().AddData("M_TLX", scoreMental.ToString());
        sceneDirObj.GetComponent<ExperimentLog>().AddData("P_TLX", scorePhysical.ToString());
        sceneDirObj.GetComponent<ExperimentLog>().AddData("JOL", scoreJOL.ToString());
        if (sceneDir.trialNumber ==1)
        {
            sceneDir.tlxDifference = 0;
            sceneDir.prevMentalTLX = scoreMental;
        }
        else
        {
            sceneDir.tlxDifference = sceneDir.prevMentalTLX - scoreMental; // positive values are decrease in load, negative are increase
            sceneDirObj.GetComponent<ExperimentLog>().AddData("Mental Diff", sceneDir.tlxDifference.ToString());
            sceneDir.prevMentalTLX = scoreMental;
        }
        // check previous mental score, if null set to this score
        // calculate difference
        // set new score
    }
}

