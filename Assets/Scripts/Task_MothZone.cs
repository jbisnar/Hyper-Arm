using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_MothZone : MonoBehaviour
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
        Debug.Log("Mothzone trigger enter from layer "+LayerMask.LayerToName(col.gameObject.layer));
        if (col.gameObject.layer == LayerMask.NameToLayer("Task"))
        {
            Debug.Log("Moth detected");
            Challenges.progressLeft -= value;
            value = 0;
            GameObject.Destroy(gameObject);
        }
    }
}
