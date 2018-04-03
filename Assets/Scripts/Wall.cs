using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {


    public GameObject whaleRight;
    public GameObject SpawnRight;
    public GameObject SpawnLeft;
    public GameObject whaleLeft;

    private GameObject whale;

    bool collided = false;
    bool playSound = false;

    private void Start()
    {
        float x = SpawnRight.transform.position.x;
        float y = SpawnRight.transform.position.y;
        float z = Random.Range(-24.5f, 30f);
        Instantiate(whaleLeft, new Vector3(x, y, z), whaleLeft.transform.rotation).GetComponent<Rigidbody>().velocity = Vector3.left * 10f;
        playSound = true;
    }

    private void Update()
    {
        if (collided)
        {
            float z = Random.Range(-24.5f, 30f);
            int i = Random.Range(0, 10);

            if (i%2 == 0)
            {
                float x = SpawnRight.transform.position.x;
                float y = SpawnRight.transform.position.y;
                Instantiate(whaleLeft, new Vector3(x, y, z), whaleLeft.transform.rotation).GetComponent<Rigidbody>().velocity = Vector3.left * 10f;                              
            }
            else
            {
                float x = SpawnLeft.transform.position.x;
                float y = SpawnLeft.transform.position.y;
                Instantiate(whaleRight, new Vector3(x, y, z), whaleRight.transform.rotation).GetComponent<Rigidbody>().velocity = Vector3.right * 10f;
            }
            playSound = true;
            collided = false;
        }

        if (playSound)
        {
            GameObject whale = GameObject.FindGameObjectWithTag("WhaleRight");
            if(whale != null)
            {
                if (whale.transform.position.x <= 50.8f)
                {
                    whale.GetComponent<AudioSource>().Play();
                    playSound = false;
                }
            }
            else
            {
                whale = GameObject.FindGameObjectWithTag("WhaleLeft");

                if (whale.transform.position.x >= -50.8f)
                {
                    whale.GetComponent<AudioSource>().Play();
                    playSound = false;
                }
            }
        }
    }

    public void SetCollided(bool a)
    {
        collided = a;
        playSound = a;
    }
}
