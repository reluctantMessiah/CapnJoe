using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShipMove : MonoBehaviour
{

    bool hit = false;
    bool setPosition = true;
    float right;
    float left;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

        if (hit)
        {
            if (this.tag == "Boat1")
            {
                if (setPosition)
                {
                    left = this.transform.position.x - 1;
                    setPosition = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(left, transform.position.y, transform.position.z), Time.deltaTime * 100);


                if (transform.position.x <= left)
                {
                    setPosition = true;
                    hit = false;

                }
            }
            if (this.tag == "Boat2")
            {
                if (setPosition)
                {
                    right = this.transform.position.x + 1;
                    setPosition = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(right, transform.position.y, transform.position.z), Time.deltaTime * 100);

                if (transform.position.x >= right)
                {
                    setPosition = true;
                    hit = false;

                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            if (!hit)
            {
                hit = true;
            }
        }
    }
}
