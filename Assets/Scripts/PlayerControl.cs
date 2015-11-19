﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public float speed = 8f; // Character's movement speed
	private Animator anim;
	Vector3 startPosition; 
    StatCollectionClass playerStat;
   // public GameObject projectile;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		startPosition = new Vector3(120, 0, -1);
        playerStat = gameObject.GetComponent<StatCollectionClass>();
        playerStat.health = 100;
        playerStat.mana = 100;
        playerStat.intellect = 1;
    }

	// Update is called once per frame
	void Update () {
		// Controls for character movement
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 0); // Up
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 1); // Right
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			transform.Translate (Vector3.down * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 2); // Down
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 3); // Left
			anim.SetBool ("Moving", true);
		} else {
			anim.SetBool ("Moving", false);
		}
		// Controls for attacking
		if (Input.GetKey (KeyCode.Space)) {
			anim.SetBool ("Attacking", true);
		} else {
			anim.SetBool ("Attacking", false);
		}

	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        // If player collides with an enemy they take damage
        if (collision.gameObject.tag == "Enemy")
        {
            StatCollectionClass enemyStat = collision.gameObject.GetComponent<StatCollectionClass>();
            playerStat.health = playerStat.health - enemyStat.intellect;
        }

        if (playerStat.health <= 0)
        {
			transform.position = startPosition;
			playerStat.health = playerStat.initialHealth;
			playerStat.mana = playerStat.initialMana;
            //Reset ();
            //Instantiate(deadsound);
            //Destroy(gameObject);
            //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 - 25, 100, 50), " You Dead!!!!! ");

            //Application.LoadLevel(Application.loadedLevel);
        }
    }

//    // Restart level
//    void Reset()
//    {
//        Application.LoadLevel(Application.loadedLevel);
//    }
}
