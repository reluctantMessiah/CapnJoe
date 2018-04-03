using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonShootTrigger : MonoBehaviour {

    public GameObject slider;
    public float sliderTime;

    public GameObject xbox360_x;
    public GameObject CannonBall;
    public GameObject CannonShaft;

    public GameObject cannonSmoke;

    public float x;
    public float y;
    public float z;

    private Coroutine cannonShoot;

    private GameObject Player;
    bool shoot = false;
    bool createCannonBall = true;


	// Use this for initialization
	void Start () 
    {
        slider.GetComponent<Slider>().minValue = 0f;
        slider.GetComponent<Slider>().maxValue = sliderTime;
        slider.SetActive(false);
	}

    private void FixedUpdate ()
    {
        if (shoot)
        {
            Rigidbody clone;
            clone = Instantiate(CannonBall.GetComponent<Rigidbody>(), new Vector3(CannonShaft.transform.position.x + x, CannonShaft.transform.position.y + y, CannonShaft.transform.position.z + z), CannonShaft.transform.rotation) as Rigidbody;
            //if (!GetComponent<AudioSource>().isPlaying)
            //{
            //    GetComponent<AudioSource>().Play();
            //}
            this.GetComponent<AudioSource>().Play();

            if (this.tag == "Cannon45")
            {
                clone.velocity = new Vector3(1, 0, 1) * 30f;
            }
            else if(this.tag == "Cannon0")
            {
                clone.velocity = Vector3.right * 30f;
            }
            else if(this.tag == "Cannon-45")
            {
                clone.velocity = new Vector3(1, 0, -1) * 30f;
            }
            else if(this.tag == "Cannon45-2")
            {
                clone.velocity = new Vector3(-1, 0, 1) * 30f;
            }
            else if(this.tag == "Cannon0-2")
            {
                clone.velocity = Vector3.left * 30f;
            }
            else if (this.tag == "Cannon-45-2")
            {
                clone.velocity = new Vector3(-1, 0, -1) * 30f;
            }

            shoot = false;           
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            //set icons
            if (other.gameObject.GetComponent<PlayerInventory>().hasCannonBall) 
            {
                xbox360_x.SetActive(true);
            }
            else 
            {
                xbox360_x.SetActive(false);
            }

            other.gameObject.GetComponent<PlayerCannon>().SetCannon(this);
            other.gameObject.GetComponent<PlayerCannon>().SetOnTrigger(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") 
        {
            //set icons
            xbox360_x.SetActive(false);

            other.gameObject.GetComponent<PlayerCannon>().SetOnTrigger(false);
            other.gameObject.GetComponent<PlayerCannon>().SetCannon(null);
        }
    }

    public void ShootingPosition() {
        //locks the player's position and lets player aim the cannon
    }

    public void Shoot(GameObject player_in) {
        if (Player == null)
        {
            Player = player_in;
        }
        
        if (cannonShoot == null)
        {
            Player.GetComponent<PlayerInventory>().SetCannonBall(false);
            Destroy(Player.GetComponent<CarryCannonBall>().GetCannonBall().gameObject);
            cannonShoot = StartCoroutine(CannonShoot(player_in));
        }
    }

    IEnumerator CannonShoot(GameObject player_in)
    {
        slider.SetActive(true);
        slider.GetComponent<Slider>().value = 0f;

        float timer = 0f; 

        while (timer < sliderTime)
        {
            if (Player != null && !Player.GetComponent<PlayerInputDevice>().GetPlayerInputDevice().Action2.IsPressed)
            {               
                slider.GetComponent<Slider>().value = 0f;
                slider.SetActive(false);
                cannonShoot = null;
                Player = null;
                yield break;
            }

            if (Player != null && !Player.GetComponent<PlayerCannon>().GetOnTrigger())
            {
                slider.GetComponent<Slider>().value = 0f;
                slider.SetActive(false);
                cannonShoot = null;
                Player = null;
                yield break;
            }

            slider.GetComponent<Slider>().value = timer;
            timer += Time.deltaTime;
            yield return null;
        }


        shoot = true;
        slider.SetActive(false);
        cannonShoot = null;
        Player = null;

        Instantiate(cannonSmoke, transform.position + transform.right * 4f + transform.up * 1.5f, Quaternion.identity);
    }

}
