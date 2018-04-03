using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{

    bool up = true;
    bool down = false;
    float upPosition;
    float startPosition;
    float downPosition;

    // Update is called once per frame 
    void Start()
    {
        startPosition = transform.position.y;
        upPosition = startPosition + Random.Range(0.5f, 1.5f);
    }

    void Update()

    {

        if (up)

        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, upPosition, transform.position.z), Time.deltaTime);



            if (Mathf.Approximately(transform.position.y, upPosition))

            {

                up = false;
                downPosition = startPosition - Random.Range(0.5f, 1.5f);
                down = true;

            }

        }

        if (down)

        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, downPosition, transform.position.z), Time.deltaTime);



            if (Mathf.Approximately(transform.position.y, downPosition))

            {

                down = false;
                upPosition = startPosition + Random.Range(0.5f, 1.5f);
                up = true;

            }

        }

    }
}