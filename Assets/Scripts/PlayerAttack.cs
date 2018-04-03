using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerAttack : MonoBehaviour {

    public AudioClip swordSwingSound;

    bool GUN_DISABLED = true;

    public GameObject sword; 
	public GameObject gun;
    public float attackCooldown;

	// For automated stuff
	public bool isAutomated = false;

    private InputDevice inputDevice;
    private bool canAttack = true;

	void Start () {
		if (isAutomated)
			return;
        inputDevice = GetComponent<PlayerInputDevice>().GetPlayerInputDevice();
	}
	
	// Update is called once per frame
	void Update () {
		if (!canAttack)
			return;

		if (isAutomated)
			return;
		
		if (inputDevice.LeftTrigger.WasPressed && !GUN_DISABLED) {
			StartCoroutine (AttackCooldown ());
			GameObject spawnedGun = SpawnWeapon (gun, false, false);

			WeaponFire weaponFire = spawnedGun.GetComponent<WeaponFire> ();

			weaponFire.playerInv = this.GetComponent<PlayerInventory> ();
			weaponFire.Fire (GetComponent<PlayerTeamInfo>().GetTeam());

		} 
        else if (inputDevice.Action3.WasPressed) {
            AudioSource.PlayClipAtPoint(swordSwingSound, Camera.main.gameObject.transform.position);
            StartCoroutine(AttackCooldown());
			SpawnWeapon(sword, true, true);
		}
	}

	// Used to automate swing attack for tutorial demonstration
	public void Swing() {
        StartCoroutine(AttackCooldown ());
		GameObject spawnedGun = SpawnWeapon(gun, false, false);

		WeaponFire weaponFire = spawnedGun.GetComponent<WeaponFire>();

		weaponFire.playerInv = this.GetComponent<PlayerInventory> ();
		weaponFire.Fire (GetComponent<PlayerTeamInfo>().GetTeam());
	}

    IEnumerator AttackCooldown() {
        canAttack = false; 
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

	GameObject SpawnWeapon(GameObject weapon, bool inRightHand, bool rotateUp) {
		int posOrNeg = 1;
		if (!inRightHand)
			posOrNeg = -1;
		Vector3 pos = transform.position + (posOrNeg * transform.right / 1.5f) + (0.25f * transform.up) + (0.5f * transform.forward);
		Quaternion rot;
		if (rotateUp) 
			rot = Quaternion.Euler(transform.rotation.eulerAngles.x - 90f, transform.rotation.eulerAngles.y, transform.eulerAngles.z);
		else
			rot = transform.rotation;
		var spawn = Instantiate(weapon, pos, rot); 
		spawn.transform.parent = transform;

		return spawn;
	}

}
