using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapePreview : MonoBehaviour
{
    public Material newMaterial; // Define your material here

    public bool considerColor = false;
    public bool isPreview = false;
    public Material white;
    public bool isPracticeTask = false;
    public ExperimentLog log;
    public GameObject managerObj;
    public SceneDirector sceneDirector;
    public invisInstructions instructions;


    // Start is called before the first frame update
    void Start()
    {
        if (log == null) managerObj = GameObject.FindWithTag("Manager");
        instructions = GameObject.FindWithTag("SceneInstructions").GetComponent<invisInstructions>();

        if (managerObj.GetComponent<ExperimentLog>() != null) log = managerObj.GetComponent<ExperimentLog>();
        sceneDirector = managerObj.GetComponent<SceneDirector>();
        if (!considerColor && isPreview)
        {
            foreach (Transform child in transform)
            {
                MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.material = newMaterial;
                }
            }
        }
        if (!considerColor && !isPreview && !isPracticeTask)
        {
            Transform firstChild = transform.GetChild(0);
            MeshRenderer meshRenderer = firstChild.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = newMaterial;
            }
        }
        if (!considerColor)
            SetPropCheckColorToNull(this.transform);

        if (isPreview)
            DisableAllColliders(this.transform);
        if (isPreview && instructions.GermaneHighLoad)
            StartCoroutine(disappear(0f));
        // if (!isPreview)
        //    DisableAllTextMeshPro(this.transform);
    }
    void OnEnable()
    {
        // if (isPreview && instructions.GermaneHighLoad && sceneDirector.trialNumber != 8)
        //     StartCoroutine(disappear(0f));
    }

    // Update is called once per frame
    void Update()
    {

    }
    void DisableAllTextMeshPro(Transform parent)
    {
        foreach (Transform child in parent)
        {
            TextMeshPro tmp = child.GetComponent<TextMeshPro>();
            if (tmp != null)
            {
                tmp.enabled = false;
            }

            // Recursively disable TextMeshPro for children's children
            DisableAllTextMeshPro(child);
        }
    }
    void DisableAllColliders(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Collider collider = child.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Recursively disable colliders for children's children
            DisableAllColliders(child);
        }
    }
    void SetPropCheckColorToNull(Transform parent)
    {
        foreach (Transform child in parent)
        {
            propCheck propCheck1 = child.GetComponent<propCheck>();
            if (propCheck1 != null)
            {
                propCheck1.color = null;
                if (!isPracticeTask) child.GetComponent<MeshRenderer>().material = white;
            }

            // Recursively set color to null for children's children
            SetPropCheckColorToNull(child);
        }
    }
    IEnumerator disappear(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.transform.parent.gameObject.SetActive(false);
        Debug.Log(this.gameObject.transform.parent.gameObject);//turns off the grandparent object

    }
}