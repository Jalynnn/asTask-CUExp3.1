using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCICollector : MonoBehaviour
{
    // Start is called before the first frame update
    int barCount = 0;
    public GameObject bciMenu;
    public GameObject instructions;
    public GameObject[] bars;
    Transform[] barTransforms;
    void Start()
    {
        barTransforms = new Transform[bars.Length];
        for (int i = 0; i < bars.Length; i++)
        {
            barTransforms[i] = bars[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (barCount == 12)
        //{
        //    Debug.Log("All bars are in");
        //    bciMenu.SetActive(true);
       //     instructions.SetActive(false);
       // }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Builder")
        {
            barCount++;
            Debug.Log(barCount);
            duplicateBars();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Builder")
        {
            barCount--;
        }
    }
    void duplicateBars()
    {
        int randomIndexBar = Random.Range(0, bars.Length);
        GameObject selectedBar = bars[randomIndexBar];
        int randomIndexPos = Random.Range(0, barTransforms.Length);
        Transform selectedPos = barTransforms[randomIndexPos];

        Instantiate(selectedBar, selectedPos.position, selectedPos.rotation);

    }
}
