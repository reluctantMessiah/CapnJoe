using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {

    public GameObject PointA;
    public GameObject PointB;
    public GameObject PointC;
    public GameObject PointD;


    bool AB = true;
    bool BC = false;
    bool CD = false;
    bool DA = false;

    float timer = 0f;
    float speed = 20;
    const float time = 2f;

	// Use this for initialization
	void Start () {
        timer = time;
	}
	
	// Update is called once per frame
	void Update () {
        if (AB)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointB.transform.position, Time.deltaTime * speed);
            if(Mathf.Approximately(transform.position.x, PointB.transform.position.x))
            {
                this.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 67f, Space.World);
                if(timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = time;
                    AB = false;
                    BC = true;
                }
            }
        }
        if (BC)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointC.transform.position, Time.deltaTime * speed);
            if (Mathf.Approximately(transform.position.x, PointC.transform.position.x))
            {
                this.transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * 67f, Space.World);
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = time;
                    BC = false;
                    CD = true;
                }
            }
        }
        if (CD)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointD.transform.position, Time.deltaTime * 40);
            if (Mathf.Approximately(transform.position.x, PointD.transform.position.x))
            {
                this.transform.Rotate(new Vector3(0, 1.7f, 0) * Time.deltaTime * 66f, Space.World);
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = time;
                    CD = false;
                    DA = true;
                }
            }
        }
        if (DA)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointA.transform.position, Time.deltaTime * speed);
            if (Mathf.Approximately(transform.position.x, PointA.transform.position.x))
            {
                this.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 66.9f, Space.World);
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {                  
                    timer = time;
                    DA = false;
                    AB = true;
                }
            }
        }
    }
}
