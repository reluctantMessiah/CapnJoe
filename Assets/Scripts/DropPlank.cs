using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlank : MonoBehaviour
{

    public GameObject plank;
    public GameObject xbox360_x;

    private bool plankDropped;

    // Use this for initialization
    void Start()
    {
        plank.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //set icons
            if (!plankDropped) {
                xbox360_x.SetActive(true);
            }
            else {
                xbox360_x.SetActive(false);
            }

            other.gameObject.GetComponent<PlayerDropPlank>().SetPlank(gameObject);
            other.gameObject.GetComponent<PlayerDropPlank>().SetOnTrigger(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            //set icons
            xbox360_x.SetActive(false);

            other.gameObject.GetComponent<PlayerDropPlank>().SetPlank(null);
            other.gameObject.GetComponent<PlayerDropPlank>().SetOnTrigger(false);
        }
    }

    public void Drop() {
        plank.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        plank.GetComponent<Rigidbody>().useGravity = true;
        plankDropped = true;
    }


    //float timer = 2f;

    //// Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        GetComponent<Rigidbody>().useGravity = true;
    //    }
    //    if (Mathf.Approximately(transform.position.y, 1f))
    //    {
    //        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //        GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}
}
