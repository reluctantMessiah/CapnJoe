using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleManager : MonoBehaviour
{


    public GameObject whaleRight;
    public GameObject whaleLeft;
    public GameObject SpawnRight;
    public GameObject SpawnLeft;

    private void Start()
    {
        StartCoroutine(whaleSpawner());
    }

    IEnumerator whaleSpawner()
    {
        yield return new WaitForSeconds(30f);

        while (true)
        {
            if (Random.Range(0, 10) % 2 == 0)
            {
                float x = SpawnLeft.transform.position.x;
                float y = SpawnLeft.transform.position.y;
                float z = Random.Range(-24.5f, 30f);
                Instantiate(whaleRight, new Vector3(x, y, z), whaleRight.transform.rotation);
            }
            else
            {
                float x = SpawnRight.transform.position.x;
                float y = SpawnRight.transform.position.y;
                float z = Random.Range(-24.5f, 30f);
                Instantiate(whaleLeft, new Vector3(x, y, z), whaleLeft.transform.rotation);
            }

            yield return new WaitForSeconds(Random.Range(20, 30));
        }
    }
}
