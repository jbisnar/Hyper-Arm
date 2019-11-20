using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAction : MonoBehaviour
{
    GameObject onItem;
    bool full;
    public int state;
    public GameObject pu;
    public Vector2 FrameVelocity;
    public Vector2 PrevPosition;
    GameObject currpu;
    public Vector3 shadowRealmPos = new Vector3(30, 30, 0);
    float useTime = 0;
    float shotLingerTime = .1f;
    public LayerMask shotcollide;
    int yarnPoints = 1;
    LineRenderer LR;
    GameObject mymoth;
    public bool lightOn = true;
    Vector3 mothDarkDir = Vector3.zero;
    bool magnetfull;
    GameObject onMetal;
    GameObject stuckMetal;
    float penSegLen = .25f;
    public bool drawing = false;
    public bool drawfailed = false;
    public bool drawfinished = false;
    int penPoints = 1;

    public GameObject bowlingpin;
    public GameObject target;
    public GameObject punchingbag;
    public GameObject tack;
    public GameObject moth;
    public GameObject mothzone;
    public GameObject metal;
    public GameObject metalbin;
    public GameObject dots;

    // Start is called before the first frame update
    void Start()
    {
        full = false;
        state = -1;
        PrevPosition = new Vector2(transform.position.x, transform.position.y);
        LR = gameObject.GetComponent<LineRenderer>();
        LR.positionCount = 1;
        magnetfull = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currFrameVelocity = (new Vector2(transform.position.x, transform.position.y) - PrevPosition) / Time.deltaTime;
        FrameVelocity = Vector2.Lerp(FrameVelocity, currFrameVelocity, 0.15f);
        PrevPosition = transform.position;

        if (Challenges.curChallenge == 0)
        {
            yarnPoints = 1;
            penPoints = 1;
            LR.positionCount = 1;
        }

        switch (state)
        {
            case 0:
                if (currpu)
                {
                    if (yarnPoints < 11 && Challenges.curChallenge == 4)
                    {
                        LR.SetPosition(yarnPoints - 1, currpu.transform.position);
                    }
                    if (mymoth)
                    {
                        mymoth.GetComponent<Task_Moth>().lightpos = currpu.transform.position;
                    }
                }
                break;
            case 1:
                break;
            case 2:
                if (Time.time > useTime + shotLingerTime)
                {
                    LR.positionCount = 1;
                }
                break;
            case 3:
                break;
            case 4:
                if (yarnPoints < 11)
                {
                    LR.SetPosition(yarnPoints - 1, transform.GetChild(0).position);
                }
                break;
            case 5:
                if (mymoth)
                {
                    if (lightOn)
                    {
                        mymoth.GetComponent<Task_Moth>().lightpos = transform.position;
                        mothDarkDir = transform.position - mymoth.transform.position;
                    } else
                    {
                        mymoth.GetComponent<Task_Moth>().lightpos = mymoth.transform.position + mothDarkDir;
                    }
                }
                break;
            case 6:
                if (magnetfull && !stuckMetal)
                {
                    magnetfull = false;
                }
                break;
            case 7:
                if (drawing && (transform.GetChild(0).position - LR.GetPosition(penPoints-1)).magnitude > penSegLen && !drawfailed)
                {
                    penPoints++;
                    LR.positionCount = penPoints;
                    LR.SetPosition(penPoints - 1, transform.GetChild(0).position);
                }
                if (drawing && drawfailed)
                {
                    drawing = false;
                    LR.startColor = Color.red;
                    LR.endColor = Color.red;
                }
                if (drawfinished && drawing)
                {
                    drawing = false;
                    LR.startColor = Color.green;
                    LR.endColor = Color.green;
                }
                break;
            default:
                break;
        }

        if (Input.GetKeyDown("r"))
        {
            Debug.Log("Restarting");
            if (currpu)
            {
                GameObject.Destroy(currpu);
            }
            Challenges.endChallenge();
            state = 0;
            full = false;
            GetComponent<ClawSprite>().UpdateSprite();
            GetComponent<AudioSource>().Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("TriggerEnter called");
        if (col.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
            onItem = col.gameObject;
            //Debug.Log("Hovering over pickup");
        }
        if (state == 6 && col.gameObject.layer == LayerMask.NameToLayer("Task"))
        {
            onMetal = col.gameObject;
            //Debug.Log("Hovering over metal object");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
            onItem = null;
            //Debug.Log("Not on pickup");
        }
        if (state == 6 && col.gameObject.layer == LayerMask.NameToLayer("Task"))
        {
            onMetal = null;
            //Debug.Log("Leaving metal object");
        }
    }

    public void ClawGrab()
    {
        if (!full)
        {
            //Debug.Log("Grabbing Item");
            if (onItem != null && onItem.GetComponent<Pickup>() != null)
            {
                state = onItem.GetComponent<Pickup>().type;
                //Debug.Log("Grabbed object type "+state);
                //GameObject.Destroy(onItem);
                currpu = onItem;
                currpu.transform.position = shadowRealmPos;
                full = true;

                lightOn = true;
                if (Challenges.curChallenge == 0)
                {
                    //Debug.Log("Starting challenge");
                    switch (state)
                    {
                        case 0:
                            break;
                        case 1:
                            Challenges.startChallenge_Bowling(bowlingpin);
                            break;
                        case 2:
                            LR.startColor = Color.yellow;
                            LR.endColor = Color.yellow;
                            Challenges.startChallenge_Shoot(target);
                            break;
                        case 3:
                            Challenges.startChallenge_Punch(punchingbag);
                            break;
                        case 4:
                            yarnPoints = 1;
                            LR.positionCount = yarnPoints;
                            LR.startColor = Color.red;
                            LR.endColor = Color.red;
                            Challenges.startChallenge_Yarn(tack);
                            break;
                        case 5:
                            mymoth = Challenges.startChallenge_GuideMoth(mothzone, moth);
                            lightOn = true;
                            break;
                        case 6:
                            Challenges.startChallenge_Sort(metal, metalbin);
                            break;
                        case 7:
                            drawfinished = false;
                            drawfailed = false;
                            Challenges.startChallenge_Line(dots);
                            break;
                        case 8:
                            HyperUI.countingTasks = true;
                            GetComponent<AudioSource>().Play();
                            break;
                        default:
                            break;
                    }
                }
            }
        } else
        {
            currpu.transform.position = transform.position;
            currpu.gameObject.GetComponent<Rigidbody2D>().velocity = FrameVelocity;
            state = 0;
            full = false;
            if (magnetfull)
            {
                stuckMetal.transform.parent = null;
                stuckMetal.transform.rotation = Quaternion.Euler(Vector3.zero);
                stuckMetal.GetComponent<Rigidbody2D>().isKinematic = false;
                stuckMetal.GetComponent<Rigidbody2D>().velocity = FrameVelocity;
                stuckMetal = null;
                magnetfull = false;
            }
            magnetfull = false;
            drawfailed = true;
        }
        GetComponent<ClawSprite>().UpdateSprite();
    }

    public void ClawUse()
    {
        useTime = Time.time;
        switch (state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Challenges.ARM_RADIUS*2, shotcollide);
                LR.positionCount = 2;
                LR.SetPosition(0, transform.position);
                LR.SetPosition(1, hit.point);
                if (hit.transform.GetComponent<Task_Target>() != null)
                {
                    GameObject.Destroy(hit.transform.gameObject);
                }
                //Debug.Log("Shooting from "+transform.position+" to "+hit.point);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                lightOn = !lightOn;
                GetComponent<ClawSprite>().UpdateSprite();
                break;
            case 6:
                if (magnetfull)
                {
                    //Debug.Log("Dropping metal");
                    stuckMetal.transform.parent = null;
                    stuckMetal.transform.rotation = Quaternion.Euler(Vector3.zero);
                    stuckMetal.GetComponent<Rigidbody2D>().isKinematic = false;
                    stuckMetal.GetComponent<Rigidbody2D>().velocity = FrameVelocity;
                    stuckMetal = null;
                    magnetfull = false;
                } else if (onMetal)
                {
                    //Debug.Log("Grabbing metal");
                    stuckMetal = onMetal;
                    stuckMetal.transform.parent = transform;
                    stuckMetal.GetComponent<Rigidbody2D>().isKinematic = true;
                    magnetfull = true;
                }
                break;
            case 7:
                break;
            default:
                break;
        }
    }

    public void TipTouch(Collider2D col)
    {
        //Debug.Log("Tiptouch received");
        if (col.gameObject.layer == LayerMask.NameToLayer("Task"))
        {
            if (state == 4)
            {
                if (col.gameObject.GetComponent<Task_Tack>() != null)
                {
                    Task_Tack tt = col.gameObject.GetComponent<Task_Tack>();
                    if (!tt.touched)
                    {
                        tt.touched = true;
                        yarnPoints++;
                        if (yarnPoints < 10)
                        {
                            LR.positionCount = yarnPoints;
                            //GetComponent<LineRenderer>().SetPosition(yarnPoints - 1, transform.position);
                            LR.SetPosition(yarnPoints - 2, tt.transform.position);
                        } else
                        {
                            LR.positionCount = yarnPoints;
                            Debug.Log("LineRenderer position "+(yarnPoints-1));
                            LR.SetPosition(yarnPoints - 1, tt.transform.position);
                            LR.SetPosition(yarnPoints - 2, tt.transform.position);
                        }
                    }
                }
            } else if (state == 7 && !drawing && !drawfinished)
            {
                penPoints = 1;
                LR.positionCount = penPoints;
                LR.SetPosition(0, col.transform.position);
                LR.startColor = Color.black;
                LR.endColor = Color.black;
                drawing = true;
                drawfailed = false;
            }
        }
    }
}
