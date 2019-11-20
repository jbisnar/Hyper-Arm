using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenges : MonoBehaviour
{
    public static float ARM_RADIUS = 13.0f;
    public static int challengesDone = 0;
    public static int curChallenge = 0;
    /*
     * 0: No challenge
     * 1: Bowling Ball
     * 2: Gun
     * 3: Boxing glove
     * 4: Yarn
     * 5: Lightbulb
     * 6: Magnet
     * 7: Pencil
     */
    public static int progressLeft = -1;

    //public static GameObject pin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector2 posInCorner()
    {
        float randDist = Random.Range(ARM_RADIUS,ARM_RADIUS*Mathf.Sqrt(2));
        float maxFrom45 = 45 - (Mathf.Acos(ARM_RADIUS / randDist) * Mathf.Rad2Deg);
        float devFrom45 = Random.Range(-maxFrom45,maxFrom45);
        float spawnAngle = 45 + Random.Range((int)0, (int)4)*90 + devFrom45;
        return new Vector2(Mathf.Cos(spawnAngle * Mathf.Deg2Rad)*randDist, Mathf.Sin(spawnAngle * Mathf.Deg2Rad) * randDist);
    }

    public static Vector2 posInRange()
    {
        float randDist = Random.Range(1.0f, ARM_RADIUS);
        float spawnAngle = Random.Range(0.0f, 360f);
        return new Vector2(Mathf.Cos(spawnAngle * Mathf.Deg2Rad) * randDist, Mathf.Sin(spawnAngle * Mathf.Deg2Rad) * randDist);
    }

    public static Vector2 posAnywhere()
    {
        float randX = Random.Range(-ARM_RADIUS, ARM_RADIUS);
        float randY = Random.Range(-ARM_RADIUS, ARM_RADIUS);
        return new Vector2(randX,randY);
    }

    public static void endChallenge()
    {
        GameObject[] taskObjs = GameObject.FindGameObjectsWithTag("TaskObj");

        foreach (GameObject tobj in taskObjs) {
            GameObject.Destroy(tobj);
        }
        
        if (progressLeft == 0)
        {
            challengesDone++;
            Debug.Log(challengesDone+" challenges finished");
        }

        curChallenge = 0;
    }

    public static void startChallenge_Bowling(GameObject pin)
    {
        progressLeft = 10;
        for (int i = 0; i < progressLeft; i++)
        {
            GameObject spawnedpin = GameObject.Instantiate(pin);
            spawnedpin.transform.parent = null;
            Vector2 spawnpos = Challenges.posInCorner();
            spawnedpin.transform.position = new Vector3(spawnpos.x, spawnpos.y);
        }
        curChallenge = 1;
    }

    public static void startChallenge_Shoot(GameObject target)
    {
        progressLeft = 10;
        for (int i = 0; i < progressLeft; i++)
        {
            GameObject spawnedtarget = GameObject.Instantiate(target);
            spawnedtarget.transform.parent = null;
            Vector2 spawnpos = Challenges.posAnywhere();
            spawnedtarget.transform.position = new Vector3(spawnpos.x, spawnpos.y);
        }
        curChallenge = 2;
    }

    public static void startChallenge_Punch(GameObject pbag)
    {
        progressLeft = 1;
        for (int i = 0; i < progressLeft; i++)
        {
            GameObject spawnedbag = GameObject.Instantiate(pbag);
            
            spawnedbag.transform.parent = null;
            Vector2 spawnpos = Challenges.posInRange();
            spawnedbag.transform.position = new Vector3(spawnpos.x, spawnpos.y);
            spawnedbag.GetComponent<Task_PunchingBag>().chainlength = 14 - spawnpos.y;
            spawnedbag.transform.GetChild(0).position = new Vector3(spawnpos.x, 14);
        }
        curChallenge = 3;
    }

    public static void startChallenge_Yarn(GameObject tack)
    {
        progressLeft = 10;
        for (int i = 0; i < progressLeft; i++)
        {
            GameObject spawnedtack = GameObject.Instantiate(tack);
            spawnedtack.transform.parent = null;
            Vector2 spawnpos = Challenges.posInRange();
            spawnedtack.transform.position = new Vector3(spawnpos.x, spawnpos.y);
        }
        curChallenge = 4;
    }

    public static GameObject startChallenge_GuideMoth(GameObject zone, GameObject moth)
    {
        progressLeft = 8;
        for (int i = 0; i < progressLeft; i++)
        {
            GameObject spawnedzone = GameObject.Instantiate(zone);
            spawnedzone.transform.parent = null;
            Vector2 spawnpos = Challenges.posAnywhere();
            spawnedzone.transform.position = new Vector3(spawnpos.x, spawnpos.y);
        }
        curChallenge = 5;
        GameObject spawnedmoth = GameObject.Instantiate(moth);
        spawnedmoth.transform.parent = null;
        Vector2 mothspawn = Challenges.posAnywhere();
        spawnedmoth.transform.position = new Vector3(mothspawn.x, mothspawn.y);
        return spawnedmoth;
    }

    public static void startChallenge_Sort(GameObject metal, GameObject bin)
    {
        progressLeft = 5;
        for (int i = 0; i < progressLeft; i++)
        {
            GameObject spawnedmetal = GameObject.Instantiate(metal);
            spawnedmetal.transform.parent = null;
            Vector2 spawnpos = Challenges.posAnywhere();
            spawnedmetal.transform.position = new Vector3(spawnpos.x, spawnpos.y);
            spawnedmetal.GetComponent<Task_Metal>().type = 0;
        }
        for (int i = 0; i < progressLeft; i++)
        {
            GameObject spawnedmetal = GameObject.Instantiate(metal);
            spawnedmetal.transform.parent = null;
            Vector2 spawnpos = Challenges.posAnywhere();
            spawnedmetal.transform.position = new Vector3(spawnpos.x, spawnpos.y);
            spawnedmetal.GetComponent<Task_Metal>().type = 1;
        }
        progressLeft = 10;
        GameObject spawnedbin = GameObject.Instantiate(bin);
        spawnedbin.transform.position = new Vector3(-5, 4);
        spawnedbin.GetComponent<Task_MetalBin>().type = 0;
        spawnedbin = GameObject.Instantiate(bin);
        spawnedbin.transform.position = new Vector3(5, 4);
        spawnedbin.GetComponent<Task_MetalBin>().type = 1;
        curChallenge = 6;
    }

    public static void startChallenge_Line(GameObject dots)
    {
        progressLeft = 1;
        Vector2 dot1pos = Challenges.posInRange();
        Vector2 dot2pos = Challenges.posInRange();
        float linelen = (dot2pos - dot1pos).magnitude;
        float lineAngle = Mathf.Atan2((dot2pos.y - dot1pos.y),(dot2pos.x - dot1pos.x)) * Mathf.Rad2Deg;
        GameObject spawneddots = GameObject.Instantiate(dots);
        spawneddots.transform.position = new Vector3((dot1pos.x + dot2pos.x) / 2, (dot1pos.y+dot2pos.y)/2, 0);
        spawneddots.transform.rotation = Quaternion.Euler(new Vector3(0,0,lineAngle));
        spawneddots.transform.GetChild(0).localPosition = new Vector3(-linelen / 2, 0, 0);
        spawneddots.transform.GetChild(1).localPosition = new Vector3(linelen / 2, 0, 0);
        spawneddots.transform.GetChild(2).localScale = new Vector3(linelen,1,1);
        spawneddots.GetComponent<CapsuleCollider2D>().size = new Vector2(linelen+1,1.2f);
        curChallenge = 7;
    }
}
