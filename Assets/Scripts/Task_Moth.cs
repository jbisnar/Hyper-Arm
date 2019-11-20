using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Moth : MonoBehaviour
{
    public Vector3 lightpos;
    float movespeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        lightpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(Mathf.Atan2(lightpos.y-transform.position.y, lightpos.x-transform.position.x) * Mathf.Rad2Deg - 90, Vector3.forward), 1080 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, lightpos, movespeed * Time.deltaTime);
    }
}
