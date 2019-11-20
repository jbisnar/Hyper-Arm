using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HyperUI : MonoBehaviour
{
    public Text timertext;
    public Text taskcounttext;
    public float startTime = 3*60;
    public float timeLeft;
    public static bool countingTasks = false;
    public GameObject pickup;
    public Vector3 stopwatchSpawn = new Vector3(6,3,0);

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (countingTasks) {
            var seconds = (int)(timeLeft);
            if (seconds % 60 < 10)
            {
                timertext.text = "" + seconds / 60 + ":0" + seconds % 60;
            }
            else
            {
                timertext.text = "" + seconds / 60 + ":" + seconds % 60;
            }
            taskcounttext.text = "Done: " + Challenges.challengesDone;
            if (seconds <= 0)
            {
                countingTasks = false;
            }
            timeLeft -= Time.deltaTime;
        }

        if (Input.GetKeyDown("r"))
        {
            Debug.Log("Restarting");
            countingTasks = false;
            timertext.text = "3:00";
            taskcounttext.text = "Done: 0";
            timeLeft = startTime;
            Challenges.challengesDone = 0;
            GameObject spawnedwatch = GameObject.Instantiate(pickup);
            spawnedwatch.GetComponent<Pickup>().type = 8;
            spawnedwatch.transform.position = stopwatchSpawn;
            spawnedwatch.transform.parent = null;
        }
    }
}
