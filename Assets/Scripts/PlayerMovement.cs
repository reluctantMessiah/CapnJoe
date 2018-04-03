using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour {

    public GameObject healthBar;

    public LayerMask jumpLayerMask;

    public GameObject runningSmoke;
    public Vector3 jumpForce;
    public float movementSpeed; 
    public float jumpTime; 
    public float gravityScale;
    public float runScale;
    public float iSeconds;
    public float flashTime;
    public float stopMovementTime;

	// For automated movement
	public bool isAutomated = false;

    private InputDevice inputDevice;
    private Rigidbody rb;
    private MeshRenderer[] rends;
    private PlayerAttack playerAttack;
    private PlayerTeamInfo playerTeamInfo;
    private PlayerStats playerStats;
    private Vector3 input;
    private Coroutine instantiateRunningSmokeCoroutine;
    private Coroutine movementCoroutine;

    private bool jumpPressed;
    private bool isJumping;
    private bool isRunning;
    private bool isInvincible;
    private bool stopMovement;
    private bool pushed;
    private bool instantiateRunningSmoke = true;
    private bool pushBack;

	private ScoreTracker scoreTracker;

	private GameObject attacker;
	private string nameOfAttacker;
	private float timeOfAttack;

	// Use this for initialization
	void Start () {
		if (isAutomated) {
			rb = GetComponent<Rigidbody>();
			MoveForward();
			return;
		}

        inputDevice = GetComponent<PlayerInputDevice>().GetPlayerInputDevice();
        rb = GetComponent<Rigidbody>();
        rends = GetComponentsInChildren<MeshRenderer>();
        playerAttack = GetComponent<PlayerAttack>();
        playerTeamInfo = GetComponent<PlayerTeamInfo>();
        playerStats = GetComponent<PlayerStats>();
        var obj = GameObject.Find("ScoreTracker");
        if (obj != null)
            scoreTracker = GameObject.Find ("ScoreTracker").GetComponent<ScoreTracker> ();

        movementCoroutine = StartCoroutine(MovementCoroutine());
		//StartCoroutine (RecordMurderer ());
	}
	
	// Update is called once per frame
	void Update () {
        if (isAutomated) {
            return;
        }

        if (inputDevice.Action1.IsPressed) {
            jumpPressed = true;
        }
        else {
            jumpPressed = false;
        }

        if (inputDevice.LeftStickButton.IsPressed) {
            isRunning = true;
        }
        else {
            isRunning = false;
            instantiateRunningSmoke = true;
        }

        if (!stopMovement && inputDevice.Action1.WasPressed && !isJumping && IsGrounded()) {
            isJumping = true;
            StartCoroutine(JumpCoroutine());
        }
	}

	// AUTOMATED MOVEMENT 

	public void Jump() {
		jumpPressed = true;
	}

	public void MoveForward() {
		rb.velocity = transform.forward * movementSpeed;
	}

	public void StopMoveForward()
	{
		rb.velocity = Vector3.zero;	
	}

	// END AUTOMATED MOVEMENT

    void FixedUpdate() {
        if (!IsGrounded()) {
            //if in the air and running (jumping and running at same time), put some juice on that jump distance
            if (isRunning) {
                rb.velocity = new Vector3(rb.velocity.x * 1f, rb.velocity.y, rb.velocity.z * 1f);
            }
            //else lessen distance you can go while in air
            else {
                rb.velocity = new Vector3(rb.velocity.x / 1.5f, rb.velocity.y, rb.velocity.z / 1.5f);
            }

            rb.AddForce(Vector3.down * gravityScale);
        }

        if (transform.parent != null) {
            if (transform.parent.gameObject.tag == "Boat1" || transform.parent.gameObject.tag == "Boat2") {
                Vector3 boatVelocity = transform.parent.gameObject.GetComponent<Rigidbody>().velocity;
                rb.velocity += new Vector3(boatVelocity.x, 0f, boatVelocity.z);
            }
        }
    }

    Vector3 GetInput() {
        float x; 
        float z; 

        x = inputDevice.LeftStickX;
        z = inputDevice.LeftStickY;

        Vector3 temp = new Vector3(x, 0f, z);

        if (temp != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(temp);
        }
           
        //don't want diagonal movement to be faster than single axis movement, so we normalize
        return temp.normalized;
    }

    bool IsGrounded() {
        float halfHeight = (GetComponent<BoxCollider>().size.y / 2f);
        if (Physics.Raycast(transform.position, Vector3.down, halfHeight + 0.1f, jumpLayerMask) || 
            Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down, halfHeight + 0.1f, jumpLayerMask) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0f, 0.5f), Vector3.down, halfHeight + 0.1f, jumpLayerMask) || 
            Physics.Raycast(transform.position + new Vector3(0.5f, 0f, -0.5f), Vector3.down, halfHeight + 0.1f, jumpLayerMask) || 
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0f, -0.5f), Vector3.down, halfHeight + 0.1f, jumpLayerMask)) {
            return true;
        }
        else {
            return false;
        }
    }

    IEnumerator MovementCoroutine() {
        while (true) {
            if (stopMovement) {
                movementCoroutine = null;
                yield break;
            }

            Vector3 temp = GetInput();

            //move player relative to ship
            if (temp.magnitude == 0f && !pushed && !pushBack) {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            } 
            else {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }

            //instantiate running smoke when player moves while holding down run button
            if (temp.magnitude > 0f && isRunning && instantiateRunningSmoke) {
                Instantiate(runningSmoke, transform.position + transform.up * -1f, Quaternion.identity);
                if (instantiateRunningSmokeCoroutine == null) {
                    instantiateRunningSmokeCoroutine = StartCoroutine(InstantiateRunningSmoke());
                }
            }

            if (isRunning) {
                rb.velocity = new Vector3(temp.x * movementSpeed * runScale, rb.velocity.y, temp.z * movementSpeed * runScale);
            } 
            else {
                rb.velocity = new Vector3(temp.x * movementSpeed, rb.velocity.y, temp.z * movementSpeed);
            }

            yield return null; 
        }
    }

    IEnumerator JumpCoroutine() {
        rb.velocity = Vector3.zero;
        float timer = 0f; 

        while (jumpPressed && timer < jumpTime) {
            float p = timer / jumpTime; 
            Vector3 thisFrameJumpVector = Vector3.Lerp(jumpForce, Vector3.zero, p); 
            rb.AddForce(thisFrameJumpVector); 
            timer += Time.fixedDeltaTime; 
            yield return null; 
        }

        isJumping = false; 
    }

	IEnumerator RecordMurderer() {
		float creditForMurderTimeout = .016f;
		while (true) {
			if (playerStats.GetHealth () <= 0) {
				print ("********************************");
				print (attacker.name);
				if (Mathf.Abs (Time.time - timeOfAttack) <= creditForMurderTimeout) {
					print (attacker.name + " killed " + this.name);
					scoreTracker.RecordKill (attacker);

				}
			}
			yield return null;
		}
	}

	IEnumerator TakeDamage() {
        isInvincible = true;

        //drop gem on damage here!
        if (GetComponent<PlayerInventory>().HasGem()) {
            GetComponent<PlayerPointManager>().DropGold();
            GetComponent<PlayerInventory>().SetGem(false);
        }

        healthBar.SetActive(true);

        playerStats.SetHealth(-1);

        float endTime = Time.time + iSeconds;

        while (Time.time < endTime) {
            foreach(MeshRenderer rend in rends) {
                rend.enabled = false; 
            }

            yield return new WaitForSeconds(flashTime);

            foreach (MeshRenderer rend in rends) {
                rend.enabled = true;
            }

            yield return new WaitForSeconds(flashTime);
        }

        foreach (MeshRenderer rend in rends) {
            rend.enabled = true;
        }

        healthBar.SetActive(false);

        isInvincible = false;
    }

    IEnumerator StopMovement(Vector3 pushDirection) {
        stopMovement = true;
        rb.velocity = Vector3.zero;
        playerAttack.enabled = false;

        pushDirection = new Vector3(pushDirection.x, 0.5f, pushDirection.z);

        rb.velocity = Vector3.zero;
        rb.AddForce(50f * pushDirection.normalized, ForceMode.Impulse);

        yield return new WaitForSeconds(stopMovementTime);

        stopMovement = false;
        playerAttack.enabled = true;

        if (movementCoroutine == null) {
            movementCoroutine = StartCoroutine(MovementCoroutine());
        }
    }

    IEnumerator PushBackTime() {
        pushBack = true;
        yield return new WaitForSeconds(1f);
        pushBack = false;
    }

    void OnTriggerEnter(Collider other) {
        if (!isInvincible) {

			bool hitByWeapon = false;

            //sword damage
            if (other.gameObject.tag == "Sword") {
	
				hitByWeapon = true;
                if (other.transform.parent.parent.gameObject.GetComponent<PlayerTeamInfo>().GetTeam() != playerTeamInfo.GetTeam()) {
                    if (GetComponent<PlayerSteeringMode>().GetSteeringMode()) {
                        if (!pushBack) {
                            StartCoroutine(PushBackTime());
                            GiveUpShipOnHit(other.transform.parent.gameObject.GetComponent<Sword>().GetPlayerForward());
                        }
                    }
                    else {
                        if (!pushBack) {
                            StartCoroutine(PushBackTime());
                            StartCoroutine(StopMovement(other.transform.parent.gameObject.GetComponent<Sword>().GetPlayerForward()));
                        }
                    }
                }
            }

			if (hitByWeapon) {
				// Get the weapon's Joe
				attacker = other.transform.parent.parent.gameObject;
				nameOfAttacker = attacker.name;
				timeOfAttack = Time.time;

				PlayerDamageHandler damageHandler = this.GetComponent<PlayerDamageHandler> ();
				damageHandler.attacker = attacker;
				damageHandler.timeOfAttack = timeOfAttack;
			}
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Sword") {
            if (!isInvincible) {
                if (other.transform.parent.parent.gameObject.GetComponent<PlayerTeamInfo>().GetTeam() != playerTeamInfo.GetTeam()) {
                    StartCoroutine(TakeDamage());
                }
            }
        }
    }

    public void StopMovementManual(bool value) {
        stopMovement = value;

        if (!value) {
            if (movementCoroutine == null) {
                movementCoroutine = StartCoroutine(MovementCoroutine());
            }
        }
    }

    public void GiveUpShipOnHit(Vector3 pushDirection) {
        pushDirection = new Vector3(pushDirection.x, 0.5f, pushDirection.z);

        rb.velocity = Vector3.zero;
        rb.AddForce(50f * pushDirection.normalized, ForceMode.Impulse);

        GetComponent<PlayerSteeringMode>().GetMyBoat().GetComponent<BoatSteering>().SetSteeringMode(false);
        GetComponent<PlayerSteeringMode>().SetPlayerSteeringMode(true);
    }

    void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.tag == "Player") {
            pushed = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.collider.gameObject.tag == "Player") {
            pushed = false;
        }
    }

    IEnumerator InstantiateRunningSmoke() {
        yield return new WaitForSeconds(0.1f);
        instantiateRunningSmoke = false;
        instantiateRunningSmokeCoroutine = null;
    }

}
