using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawSprite : MonoBehaviour
{
    SpriteRenderer sprr;
    public Sprite open;
    public Sprite closed;
    public Sprite bball;
    public Sprite gun;
    public Sprite glove;
    public Sprite yarn;
    public Sprite lightbulbOn;
    public Sprite lightbulbOff;
    public Sprite magnet;
    public Sprite pencil;
    public Sprite stopwatch;
    public ArmControl armcon;
    public ClawAction clawac;

    // Start is called before the first frame update
    void Start()
    {
        sprr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clawac.state == 0)
        {
            if (armcon.grabbing)
            {
                sprr.sprite = closed;
            }
            else
            {
                sprr.sprite = open;
            }
        }
    }

    public void UpdateSprite()
    {
        switch (clawac.state)
        {
            case 0:
                if (armcon.grabbing)
                {
                    sprr.sprite = closed;
                }
                else
                {
                    sprr.sprite = open;
                }
                break;
            case 1:
                sprr.sprite = bball;
                break;
            case 2:
                sprr.sprite = gun;
                break;
            case 3:
                sprr.sprite = glove;
                break;
            case 4:
                sprr.sprite = yarn;
                break;
            case 5:
                if (clawac.lightOn)
                {
                    sprr.sprite = lightbulbOn;
                } else
                {
                    sprr.sprite = lightbulbOff;
                }
                break;
            case 6:
                sprr.sprite = magnet;
                break;
            case 7:
                sprr.sprite = pencil;
                break;
            case 8:
                sprr.sprite = stopwatch;
                break;
            default:
                break;
        }
    }
}
