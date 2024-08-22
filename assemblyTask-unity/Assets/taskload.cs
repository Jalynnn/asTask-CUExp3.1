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
    // Start is called before the first frame update
    void Start()
    {

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
}

