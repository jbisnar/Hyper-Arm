using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Pin : MonoBehaviour
{
    int value = 1;

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
        //Debug.Log("TriggerEnter called");
        if (col.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
            Challenges.progressLeft -= value;
            value = 0;
            GameObject.Destroy(gameObject);
        }
    }
}
