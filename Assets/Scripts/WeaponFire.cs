using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

	public int numProjectilesInSpreadShot = 8;
	public bool usesSpreadShot = false;
	public GameObject projectilePrefab;
	public Transform projectileOrigin;
	public float projectileSpeed = 5f;

	public PlayerInventory playerInv;

	public void Fire(string playerTeam) {

		//if (!playerInv.HasBullet ())
		//	return;

		if (usesSpreadShot) {

			float degreesPerProjectile = 90f / numProjectilesInSpreadShot;

			for (int i = 0; i < numProjectilesInSpreadShot; ++i) {
				GameObject projectile = GameObject.Instantiate (projectilePrefab, projectileOrigin.position, transform.rotation);
				projectile.transform.Rotate (new Vector3 (0f, -45f + degreesPerProjectile * i, 0f));
				projectile.GetComponent<Rigidbody> ().velocity = projectile.transform.forward * projectileSpeed;

                projectile.GetComponent<BuckShot>().SetPlayerTeam(playerTeam);
			}
		} else {
			GameObject projectile = GameObject.Instantiate (projectilePrefab, projectileOrigin.position, Quaternion.identity);
			projectile.GetComponent<Rigidbody> ().velocity = transform.forward * projectileSpeed;
		}

		//playerInv.UseBullet ();
	}
}
