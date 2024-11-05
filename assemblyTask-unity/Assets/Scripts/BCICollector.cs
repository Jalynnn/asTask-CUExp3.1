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
    public Transform[] barTransforms;
    bool temp = true;
    void Start()
    {
        barTransforms = new Transform[bars.Length];
        for (int i = 0; i < bars.Length; i++)
        {
            GameObject newBar = new GameObject("BarTransform" + i);

            // Copy the position, rotation, and scale from the original bar
            newBar.transform.position = bars[i].transform.position;
            newBar.transform.rotation = bars[i].transform.rotation;
            newBar.transform.localScale = bars[i].transform.localScale;

            // Store the new Transform in the barTransforms array
            barTransforms[i] = newBar.transform;
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
            if (barCount % 12 == 0 ) duplicateBars();
        }
    }
    void duplicateBars()
    {
        foreach (Transform barTransform in barTransforms)
        {
            int randomIndexBar = Random.Range(0, bars.Length);
            GameObject selectedBar = bars[randomIndexBar];
            Instantiate(selectedBar, barTransform.position, barTransform.rotation);
        }
    }

}
