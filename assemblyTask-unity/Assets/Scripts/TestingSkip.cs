using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSkip : MonoBehaviour
{
    public GameObject tempObjT;
    public GameObject tempObjM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            tempObjT.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
        tempObjM.SetActive(!tempObjM.activeSelf);
        }
       
    }
}
