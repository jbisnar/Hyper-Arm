using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task_PunchingBag : MonoBehaviour
{
    int maxhealth = 500;
    int currhealth = 500;
    float hitTime = 0;
    float invTime = .5f;
    float grav = 50f;
    public Vector2 temp;
    LineRenderer lr;
    DistanceJoint2D dj;
    public float chainlength = 5;
    public RectTransform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currhealth = maxhealth;
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        dj.distance = chainlength;
    }

    // Update is called once per frame
    void Update()
    {
        temp = transform.GetComponent<Rigidbody2D>().velocity;
        temp.y -= grav * Time.deltaTime;
        transform.GetComponent<Rigidbody2D>().velocity = temp;

        lr.SetPosition(0, transform.GetChild(0).position);
        lr.SetPosition(1, transform.position);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("TriggerEnter called");
        if (col.gameObject.layer == LayerMask.NameToLayer("Arm"))
        {
            if (col.gameObject.GetComponent<ClawAction>() != null && Time.time > hitTime + invTime)
            {
                hitTime = Time.time;
                Vector3 punchVel = col.gameObject.GetComponent<ClawAction>().FrameVelocity;
                currhealth -= (int) punchVel.magnitude;
                healthBar.sizeDelta = new Vector2( (currhealth/(float)maxhealth)*100 , healthBar.sizeDelta.y);
                Debug.Log("Hit for "+punchVel.magnitude+" damage");
                GetComponent<Rigidbody2D>().velocity = punchVel;
                if (currhealth <= 0)
                {
                    Challenges.progressLeft--;
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}
