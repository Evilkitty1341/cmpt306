using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public float speed = 5f; // Character's movement speed
	private Animator anim;
    StatCollectionClass playerStat;
   // public GameObject projectile;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();

        playerStat = gameObject.GetComponent<StatCollectionClass>();
        playerStat.health = 100;
        playerStat.mana = 100;
        playerStat.intellect = 1;
    }
	
	// Update is called once per frame
	void Update () {
		// Controlls for character movement
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate (Vector3.up * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 0); // Up
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate (Vector3.down * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 1); // Down
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate (Vector3.left * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 2); // Left
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 3); // Right
			anim.SetBool ("Moving", true);
		} else {
			anim.SetBool ("Moving", false);
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

            //Reset ();
            //Instantiate(deadsound);
            Destroy(gameObject);
            //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 - 25, 100, 50), " You Dead!!!!! ");

            Application.LoadLevel(Application.loadedLevel);
        }
    }

    // Restart level
    void Reset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
