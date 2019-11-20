using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public GameObject pickup;
    float nextChallengeDelay = .5f;
    float trashedTime = 0;
    float spawnedTime = 0;
    float velThrowV = 35f;
    float velThrowH = 25f;
    bool pickupOut = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > trashedTime + nextChallengeDelay && !pickupOut && HyperUI.countingTasks)
        {
            spawnedTime = Time.time;
            pickupOut = true;
            //spawn next challenge and toss it out the bin
            GameObject spawnedPick = GameObject.Instantiate(pickup);
            spawnedPick.GetComponent<Pickup>().type = Random.Range(1,8);
            spawnedPick.transform.position = transform.position;
            spawnedPick.transform.parent = null;
            spawnedPick.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-velThrowH,velThrowH), velThrowV);
        }
        if (Input.GetKeyDown("r"))
        {
            pickupOut = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("TriggerEnter called");
        if (col.gameObject.layer == LayerMask.NameToLayer("Pickup") && Time.time > spawnedTime + nextChallengeDelay && pickupOut)
        {
            GameObject.Destroy(col.gameObject);
            trashedTime = Time.time;
            Challenges.endChallenge();
            Challenges.progressLeft = -1;
            pickupOut = false;
            /*
            if (Time.time > trashedTime + nextChallengeDelay)
            {
                trashedTime = Time.time;
                Challenges.endChallenge();
                Challenges.progressLeft = -1;
                pickupOut = false;
            }
            */
        }
    }
}
