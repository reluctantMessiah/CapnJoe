using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public string playerName;
    public int maxHealth;
    
    private Image healthBar;
    private int health;
	private PlayerPointManager pointManager;

	void Start() {
        health = maxHealth;
        healthBar = GameObject.Find(playerName + "HealthBar").GetComponent<Image>();
		pointManager = this.GetComponent<PlayerPointManager> ();
		//StartCoroutine (DropCoinsOnTakeDamage ());
	}

    void Update() {
        float healthPercentage = (float)(health) / (float)(maxHealth);

        if (healthPercentage > 0.75f) {
            healthBar.color = Color.green;
        }
        else if (healthPercentage <= 0.75f && healthPercentage > 0.25f) {
            healthBar.color = Color.yellow;
        }
        else {
            healthBar.color = Color.red;
        }

        healthBar.fillAmount = healthPercentage;
    }

    public void SetHealth(int health_in) {
        health += health_in;
        Debug.Log(health);
    }

    public void ResetHealth(int health_in) {
        health = health_in;
    }

    public int GetHealth() {
        return health; 
    }

    public string GetPlayerName() {
        return playerName;
    }

	IEnumerator DropCoinsOnTakeDamage() {
		int prevHealth = health;
		while (true) {
			if (health < prevHealth) {
				bool willDie = (health == 0);
				if (pointManager != null && !willDie) {
					pointManager.DropGold (willDie);
				}
			}
			prevHealth = health;
			yield return null;
		}
	}

}
