using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int type;
    /*
     * 0: stopwatch
     * 1: bowling ball
     * 2: gun
     * 3: boxing glove
     * 4: yarn ball
     * 5: lightbulb
     * 6: magnet
     * 7: pencil
     */
    public Sprite Placeholder;
    public Sprite Stopwatch;
    public Sprite BBall;
    public Sprite Gun;
    public Sprite Glove;
    public Sprite Magnet;
    public Sprite Lightbulb;
    public Sprite Pencil;
    public Sprite Yarn;
    bool gravOn;
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
                sprr.sprite = Stopwatch;
                break;
            case 1:
                sprr.sprite = BBall;
                break;
            case 2:
                sprr.sprite = Gun;
                break;
            case 3:
                sprr.sprite = Glove;
                break;
            case 4:
                sprr.sprite = Yarn;
                break;
            case 5:
                sprr.sprite = Lightbulb;
                break;
            case 6:
                sprr.sprite = Magnet;
                break;
            case 7:
                sprr.sprite = Pencil;
                break;
            case 8:
                sprr.sprite = Stopwatch;
                break;
            default:
                sprr.sprite = Placeholder;
                break;
        }
        bounceTime = 0;
        //Debug.Log("Pickup spawned type "+type);
    }

    /*
    public void setType(int putype)
    {
        SpriteRenderer sprr = gameObject.GetComponent<SpriteRenderer>();
        switch (putype)
        {
            case 1:
                sprr.sprite = BBall;
                break;
            case 2:
                sprr.sprite = Gun;
                break;
            default:
                sprr.sprite = Placeholder;
                break;
        }
        type = putype;
    }
    */

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
            } else if (walledL)
            {
                temp.x = velBounceH;
            } else if (walledR)
            {
                temp.x = -velBounceH;
            } else if (floored)
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

        if (Input.GetKeyDown("r"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
