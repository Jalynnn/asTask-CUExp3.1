using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class taskload : MonoBehaviour
{
    public int scoreMental;
    public int scorePhysical;
    public TextMeshPro value;
    public Slider tlx;
    SceneDirector sceneDir;
    GameObject sceneDirObj;
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
    public void logTLX()
    {
        sceneDirObj = this.GetComponent<FindSM>().sceneDirectorObject;
        sceneDirObj.GetComponent<ExperimentLog>().AddData("Mental Task Load", scoreMental.ToString());
        sceneDirObj.GetComponent<ExperimentLog>().AddData("Physical Task Load", scorePhysical.ToString());
        if (sceneDir.prevMentalTLX == null)
        {
            sceneDir.prevMentalTLX = scoreMental;
        }
        else
        {
            sceneDir.tlxDifference = sceneDir.prevMentalTLX - scoreMental; //if positive value, mental load has decreased
            sceneDir.prevMentalTLX = scoreMental;
        }
        // check previous mental score, if null set to this score
        // calculate difference
        // set new score
    }
}

