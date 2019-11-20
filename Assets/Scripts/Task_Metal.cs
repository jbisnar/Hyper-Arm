using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Metal : MonoBehaviour
{
    public int type;
    /*
     * 0: nut
     * 1: bolt
     */
    public Sprite nut;
    public Sprite bolt;
    float grav = 30f;
    float velBounceH = 15f;
    float velBounceV = 30f;
    public float bounceTime;
    float bounceGrace = .1f;
    public LayerMask layerGround;
    public bool ceilinged;
    public bool walledL;
    public bool walledR;
    public bool floored;

    public Vector2 temp;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sprr = gameObject.GetComponent<SpriteRenderer>();
        switch (type)
        {
            case 0:
                sprr.sprite = nut;
                break;
            case 1:
                sprr.sprite = bolt;
                break;
            default:
                sprr.sprite = nut;
                break;
        }
        bounceTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        temp = transform.GetComponent<Rigidbody2D>().velocity;
        ceilinged = Physics2D.OverlapArea(new Vector2(transform.position.x - .1f, transform.position.y + .72f),
            new Vector2(transform.position.x + .1f, transform.position.y + .68f), layerGround);
        walledL = Physics2D.OverlapArea(new Vector2(transform.position.x - .72f, transform.position.y + .1f),
            new Vector2(transform.position.x - .68f, transform.position.y - .1f), layerGround);
        walledR = Physics2D.OverlapArea(new Vector2(transform.position.x + .68f, transform.position.y + .1f),
            new Vector2(transform.position.x + .72f, transform.position.y - .1f), layerGround);
        floored = Physics2D.OverlapArea(new Vector2(transform.position.x - .1f, transform.position.y - .72f),
            new Vector2(transform.position.x + .1f, transform.position.y - .68f), layerGround);

        if (Time.time > bounceTime + bounceGrace)
        {
            if (ceilinged)
            {
                temp.y = -velBounceH;
            }
            else if (walledL)
            {
                temp.x = velBounceH;
            }
            else if (walledR)
            {
                temp.x = -velBounceH;
            }
            else if (floored)
            {
                temp.y = velBounceV;
            }
        }

        if ((ceilinged || walledL || walledR || floored) && Time.time > bounceTime + bounceGrace)
        {
            bounceTime = Time.time;
        }

        temp.y -= grav * Time.deltaTime;
        transform.GetComponent<Rigidbody2D>().velocity = temp;
    }
}
