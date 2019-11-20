using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    public GameObject UpperArm;
    public GameObject ForeArm;
    public GameObject Claw;
    public bool grabbing = false;
    public ClawAction cl;

    public GameObject pin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpperArm.transform.rotation = Quaternion.RotateTowards(UpperArm.transform.rotation, Quaternion.AngleAxis(-(Mathf.Atan2(Input.GetAxisRaw("UpperArmV"), Input.GetAxisRaw("UpperArmH")) * Mathf.Rad2Deg) - 90, Vector3.forward), 1080*Time.deltaTime);
        ForeArm.transform.rotation = Quaternion.RotateTowards(ForeArm.transform.rotation, Quaternion.AngleAxis(-(Mathf.Atan2(Input.GetAxisRaw("ForeArmV"), Input.GetAxisRaw("ForeArmH")) * Mathf.Rad2Deg) - 90, Vector3.forward), 1080 * Time.deltaTime);

        if (Input.GetAxisRaw("Grab") > 0 && !grabbing)
        {
            grabbing = true;
            cl.ClawGrab();
        } else if (Input.GetAxisRaw("Grab") == 0 && grabbing)
        {
            grabbing = false;
        }
        if (Input.GetButtonDown("Use"))
        {
            cl.ClawUse();
        }

        if (Input.GetKey("escape"))
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
    }
}
