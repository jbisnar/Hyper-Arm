using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Target : MonoBehaviour
{
    Vector3 nextpos;
    float movespeed;

    // Start is called before the first frame update
    void Start()
    {
        nextpos = Challenges.posAnywhere();
        movespeed = Random.Range(5f,10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextpos, movespeed*Time.deltaTime);
        if (transform.position == nextpos)
        {
            nextpos = Challenges.posAnywhere();
            movespeed = Random.Range(5f, 10f);
        }
    }

    private void OnDestroy()
    {
        Challenges.progressLeft--;
    }
}
