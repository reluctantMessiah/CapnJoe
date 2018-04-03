using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroy : MonoBehaviour
{

    public GameObject ship;
    public GameObject plank;

    float timerConstant = 2f;
    float speed = 10f;
    float timer;

    bool phase1 = true;
    bool phase2 = false;
    bool finished = false;

    // Use this for initialization
    void Start()
    {
        print("SHIPDESTROY IS HEREEE!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        timer = timerConstant;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<BoatHealth>().IsDead())
        {
            Destroy(plank);
            if (phase1)
            {
                ship.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 20, Space.World);
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    phase1 = false;
                    phase2 = true;
                    timer = timerConstant;
                }
            }

            if (phase2)
            {
                ship.transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -30, transform.position.z), Time.deltaTime * speed);
                if(Mathf.Approximately(transform.position.y, -30f))
                {
                    finished = true;
                }
            }
        }
    }

    public bool FinishedDeath()
    {
        return finished;
    }
}