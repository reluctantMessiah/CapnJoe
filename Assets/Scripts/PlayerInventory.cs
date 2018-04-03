using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public bool hasCannonBall = false;
	public bool hasGem = false;

    private InputDevice inputDevice;
    private bool OnTriggerCannonball;

	private PlayerPointManager playerPointManager;

    private GameObject inventoryIcons;

    void Start() {
        inputDevice = GetComponent<PlayerInputDevice>().GetPlayerInputDevice();
		playerPointManager = GetComponent<PlayerPointManager> ();
        inventoryIcons = GameObject.Find(GetComponent<PlayerStats>().GetPlayerName() + "Inventory");
    }

    void Update() {
        if (OnTriggerCannonball) {
            if (inputDevice.Action3.WasPressed) {
                if (GetComponent<CarryCannonBall>().GetInstantiateCannonBall() == false && hasCannonBall == false) {
                    GetComponent<CarryCannonBall>().SetInstantiateCannonBall(true);
                }
                
                SetCannonBall(true);
            }
        }

        if (hasGem) {
            inventoryIcons.transform.Find("Gem").gameObject.SetActive(true);
        }
        else {
            inventoryIcons.transform.Find("Gem").gameObject.SetActive(false);
        }

        if (hasCannonBall) {
            inventoryIcons.transform.Find("Cannonball").gameObject.SetActive(true);
        } 
        else {
            inventoryIcons.transform.Find("Cannonball").gameObject.SetActive(false);
        }
    }

	public bool HasGem() {
		return hasGem;
	}

    public void SetGem(bool value) {
        hasGem = value;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Gem")) {
            if (!hasGem) {
                print(other.transform.parent.name);
                //Destroy(other.transform.parent.gameObject);

                other.gameObject.GetComponent<Destroyer>().DestroyParent();

                hasGem = true;
            }
        }
        else if (other.CompareTag("CannonballPickup")) {
            if (!hasCannonBall) {
                print(other.transform.parent.name);
                //Destroy(other.transform.parent.gameObject);

                other.gameObject.GetComponent<Destroyer>().DestroyParent();

                hasCannonBall = true;
                GetComponent<CarryCannonBall>().SetInstantiateCannonBall(true);
            }
        }
	}

    public void SetCannonBall(bool a) {
        hasCannonBall = a;
    }

    public bool HasCannonBall() {
        return hasCannonBall;
    }

    public void SetOnTriggerCannonball(bool value) {
        OnTriggerCannonball = value;
    }

}
