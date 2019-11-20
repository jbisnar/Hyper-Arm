using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Dots : MonoBehaviour
{
    ClawAction clac;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).GetComponent<Task_Point>().touched && transform.GetChild(1).GetComponent<Task_Point>().touched && clac)
        {
            clac.drawfinished = true;
            Challenges.progressLeft = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Task2"))
        {
            ClawAction claw = col.transform.parent.GetComponent<ClawAction>();
            if (claw)
            {
                clac = claw;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Task2"))
        {
            ClawAction claw = col.transform.parent.GetComponent<ClawAction>();
            if (claw && !claw.drawfinished)
            {
                claw.drawfailed = true;
                transform.GetChild(0).GetComponent<Task_Point>().touched = false;
                transform.GetChild(1).GetComponent<Task_Point>().touched = false;
            }
        }
    }

    public void DotTouch(Collider2D col, int childNum)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Task2") && col.gameObject == gameObject)
        {
            Debug.Log("Triggered on spawn");
        } else if (col.gameObject.layer == LayerMask.NameToLayer("Task2") && col.transform.parent.GetComponent<ClawAction>().state == 7)
        {
            transform.GetChild(childNum).GetComponent<Task_Point>().touched = true;
        }
    }
}
