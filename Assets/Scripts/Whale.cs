using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{

    float timer = 1f;
    float timerDestroy = 6f;
    float timerRotate = 1f;
    float stopRotate = 1f;
    bool whaleDeath = false;
    bool startRotate = true;
    bool sink = false;


    private void Start()
    {
        if(this.tag == "WhaleRight")
        {
            GetComponent<Rigidbody>().velocity = Vector3.right * 10f;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.left * 10f;
        }
    }

    private void Update()
    {
        if(this.tag == "WhaleRight")
        {
            if (transform.position.x <= 50.8f)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }
        else if(this.tag == "WhaleLeft")
        {
            if (transform.position.x >= -50.8f)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }

        if (whaleDeath)
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (timerRotate >= 0)
                {
                    timerRotate -= Time.deltaTime;
                }
                else {
                    if (startRotate)
                    {
                        this.transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 50, Space.World);
                    }
                    if(stopRotate >= 0)
                    {
                        stopRotate -= Time.deltaTime;
                    }
                    else
                    {
                        startRotate = false;
                        sink = true;
                    }
                }

                if (sink)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - 3f,
                        this.transform.position.z), Time.deltaTime * 10f);
                }

                if(timerDestroy >= 0)
                {
                    timerDestroy -= Time.deltaTime;
                }
                else
                {
                    Destroy(this.gameObject);
                    whaleDeath = false;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boulder")
        {
            if (this.tag == "WhaleRight")
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.right * 10f;
            }
            else if (this.tag == "WhaleLeft")
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.left * 10f;
            }
        }

        if(collision.gameObject.tag == "Boat1" || collision.gameObject.tag == "Boat2")
        {
            if (GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Stop();
            }

            if (this.tag == "WhaleRight")
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.left;
            }
            else if(this.tag == "WhaleLeft")
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.right;
            }

            this.GetComponent<BoxCollider>().enabled = false;
            whaleDeath = true;
        }

        if(collision.gameObject.tag == "RightWall" || collision.gameObject.tag == "LeftWall")
        {
            if (GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().Stop();
            }

            Destroy(this.gameObject);
        }
    }
}
