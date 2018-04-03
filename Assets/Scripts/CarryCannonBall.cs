using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryCannonBall : MonoBehaviour {

    public GameObject CannonBall;
    GameObject cannonBall;
    bool instantiateCannonBall_Player = true;

    // Update is called once per frame
    void Update () {

        if (instantiateCannonBall_Player)
        {
            instantiateCannonBall_Player = false;
            if (this.GetComponent<PlayerInventory>().HasCannonBall())
            {
                cannonBall = Instantiate(CannonBall, transform.Find("PositionToHoldItem").transform.position, Quaternion.identity);
                //cannonBall.GetComponent<Rigidbody>().isKinematic = true;
                //cannonBall.GetComponent<SphereCollider>().enabled = false;
                cannonBall.transform.parent = this.transform;               
            }
        }

	}

    public GameObject GetCannonBall()
    {
        return cannonBall;
    }

    public void SetInstantiateCannonBall(bool a)
    {
        instantiateCannonBall_Player = a;
    }

    public bool GetInstantiateCannonBall()
    {
        return instantiateCannonBall_Player;
    }
}
