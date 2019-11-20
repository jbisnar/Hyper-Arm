using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_MetalBin : MonoBehaviour
{
    public int type;
    /*
     * 0: nut
     * 1: bolt
     */
    public Sprite nuts;
    public Sprite bolts;
    GameObject lastTouched;

    // Start is called before the first frame update
    void Start()
    {
        if (type == 0)
        {
            GetComponent<SpriteRenderer>().sprite = nuts;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = bolts;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("TriggerEnter called");
        if (col.gameObject.layer == LayerMask.NameToLayer("Task"))
        {
            var metal = col.gameObject;
            if (metal.GetComponent<Task_Metal>())
            {
                if (metal.GetComponent<Task_Metal>().type == type && metal.gameObject != lastTouched)
                {
                    Challenges.progressLeft--;
                    Debug.Log("ProgressLeft: "+Challenges.progressLeft);
                    GameObject.Destroy(metal);
                }
            }
            lastTouched = col.gameObject;
        }
    }
}
