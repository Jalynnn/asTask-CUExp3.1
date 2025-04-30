using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    public AudioClip tutorialAudioEXPA;
    public AudioClip tutorialAudioEXPB;
    public GameObject nextButton;
    bool tutoStarted = false;
    public GameObject sceneDirector;
    public VideoPlayer videoPlayer;
    public GameObject TextBox;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TextBox.SetActive(false);
            if (sceneDirector.GetComponent<SceneDirector>().experimentType == SceneDirector.ExperimentType.ExpA)
            {
                StartCoroutine(StartTutorial("A"));
            }
            if (sceneDirector.GetComponent<SceneDirector>().experimentType == SceneDirector.ExperimentType.ExpB)
            {
                StartCoroutine(StartTutorial("B"));
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            nextButton.SetActive(true);
        }
    }

    IEnumerator StartTutorial(string type)
    {
        if (!tutoStarted)
        {
            AudioSource audioSource = videoPlayer.GetComponent<AudioSource>();
            videoPlayer.Play();
            tutoStarted = true;
            yield return new WaitForSeconds((float)videoPlayer.clip.length);
            nextButton.SetActive(true);

        }

    }
}
