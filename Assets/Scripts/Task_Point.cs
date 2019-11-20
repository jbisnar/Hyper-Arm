using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Point : MonoBehaviour
{
    public bool touched = false;
    public int childNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.GetComponentInParent<Task_Dots>().DotTouch(col, childNum);
    }
}
