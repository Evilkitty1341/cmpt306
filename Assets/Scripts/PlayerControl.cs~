using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public float speed = 8f; // Character's movement speed
	private Animator anim;
	Vector3 startPosition; 
	Vector3 townPosition;
    StatCollectionClass playerStat;
    
    public float attackRate = 1.0F;
    private float nextAttack = 1.0F;

    AudioSource attackSound;
    public GameObject attackPrefab;
    public GameObject spawnedAttack;

    public GameObject magicPrefab;
    public GameObject spawnedMagic;
    public int lives =4;

    public GameObject deadSoundObject;

	//used to check tha tthe intro scene is done
	StoryLineComponents SLC;

	//for respawn position at town
	Dialogue D;
    
	ParticleSystem flamethrower;
	GameObject flameEmmision;
    // public GameObject projectile;

    // Use this for initialization
    void Start () 
	{
		anim = GetComponent<Animator> ();
		flamethrower = gameObject.GetComponentInChildren<ParticleSystem>();
		flameEmmision = flamethrower.gameObject;
		startPosition = new Vector3(-120, 0, -1);
        playerStat = gameObject.GetComponent<StatCollectionClass>();
        playerStat.health = 100;
        playerStat.mana = 100;
        playerStat.intellect = 1;
        playerStat.playerDirection = 2;
        attackSound = GetComponent<AudioSource>();
		SLC = GameObject.Find("Main Camera").GetComponent<StoryLineComponents>();
		D = GetComponent<Dialogue>();
		townPosition = new Vector3 (137f, -46f, 10f);
    }

	// Update is called once per frame
	void Update () 
	{
		// Controls for character movement
		if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))  && SLC.playerEnabled == true && D.freezePos == false) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
            playerStat.playerDirection = 1;
            anim.SetInteger ("Direction", 0); // Up
			flameEmmision.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, 0f);
			flameEmmision.transform.rotation = Quaternion.AngleAxis(-90f, new Vector3(1, 0, 0));
			anim.SetBool ("Moving", true);
		} else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && SLC.playerEnabled == true && D.freezePos == false) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 1); // Right
            playerStat.playerDirection = 2;
			flameEmmision.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, 0f);
			flameEmmision.transform.rotation = Quaternion.AngleAxis(90f, new Vector3(0, 1, 0));
            anim.SetBool ("Moving", true);
		} else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && SLC.playerEnabled == true &&D.freezePos == false) {
			transform.Translate (Vector3.down * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 2); // Down
            playerStat.playerDirection = 3;
			flameEmmision.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
			flameEmmision.transform.rotation = Quaternion.AngleAxis(90f, new Vector3(1, 0, 0));
            anim.SetBool ("Moving", true);
		} else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && SLC.playerEnabled == true && D.freezePos == false){
			transform.Translate (Vector3.left * speed * Time.deltaTime);
            playerStat.playerDirection = 4;
            anim.SetInteger ("Direction", 3); // Left
			flameEmmision.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, 0f);
			flameEmmision.transform.rotation = Quaternion.AngleAxis(-90f, new Vector3(0, 1, 0));
			anim.SetBool ("Moving", true);
		} else {
			anim.SetBool ("Moving", false);
		}
        // Controls for attacking
		if (Input.GetKey("f") && SLC.playerEnabled == true)
        {
            anim.SetBool("Attacking", true);
            attackSound.Play();
            nextAttack = Time.time + attackRate;

			flamethrower.Emit(20);
			//flamethrower.enableEmission = true;
            //playerStat.mana = playerStat.mana - 40f;


			/*
            spawnedMagic = GameObject.Instantiate(magicPrefab, transform.position, transform.rotation) as GameObject;
            spawnedMagic01 = GameObject.Instantiate(magicPrefab01, transform.position, transform.rotation) as GameObject;
            spawnedMagic02 = GameObject.Instantiate(magicPrefab02, transform.position, transform.rotation) as GameObject;
            spawnedMagic03 = GameObject.Instantiate(magicPrefab03, transform.position, transform.rotation) as GameObject;
            spawnedMagic04 = GameObject.Instantiate(magicPrefab04, transform.position, transform.rotation) as GameObject;
            spawnedMagic05 = GameObject.Instantiate(magicPrefab05, transform.position, transform.rotation) as GameObject;
            spawnedMagic06 = GameObject.Instantiate(magicPrefab06, transform.position, transform.rotation) as GameObject;
            spawnedMagic07 = GameObject.Instantiate(magicPrefab07, transform.position, transform.rotation) as GameObject;
            spawnedMagic.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500));
            spawnedMagic01.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
            spawnedMagic02.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -500));
            spawnedMagic03.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, 0));
            spawnedMagic04.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, -500));
            spawnedMagic05.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 500));
            spawnedMagic06.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, 500));
            spawnedMagic07.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, -500));
			*/
        }


		/*********************
		 * NOTE:
		 * SHOULD TAKE THIS OUT BECAUSE NOW USE SPACE FOR QUESTS AND DIALOGUE
            if (Input.GetKey (KeyCode.Space) && Time.time >nextAttack)
		{
			anim.SetBool ("Attacking", true);
            //attackSound.Play();
//            nextAttack = Time.time + attackRate;
//            spawnedAttack = GameObject.Instantiate(attackPrefab, transform.position, transform.rotation) as GameObject;
//            if (playerStat.playerDirection == 1)
//            {
//                spawnedAttack.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500));
//            }
//            else if (playerStat.playerDirection == 2)
//            {
//                spawnedAttack.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
//            }
//            else if (playerStat.playerDirection == 3)
//            {
//                spawnedAttack.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -500));
//            }
//            else if (playerStat.playerDirection == 4)
//            {
//                spawnedAttack.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, 0));
//            }

            }else
		{*/
		//flamethrower.Pause();
        anim.SetBool ("Attacking", false);
		checkHealth();
		}
    /*
        void OnCollisionEnter2D(Collision2D collision)
    {
        // If player collides with an enemy they take damage
        if (collision.gameObject.tag == "Lava")
        {
            playerStat.health = playerStat.health - 10;
        }
        /*
            if (collision.gameObject.tag == "Enemy")
        {
            StatCollectionClass enemyStat = collision.gameObject.GetComponent<StatCollectionClass>();
            playerStat.health = playerStat.health - enemyStat.intellect;
        }
    }
     */


void checkHealth()
	{
		if (playerStat.health <= 0) {
			Instantiate (deadSoundObject);

			if (playerStat.health <= 0) {
				Instantiate (deadSoundObject);
				
				if (D.townDone) {
					transform.position = townPosition;
				} else {
					transform.position = startPosition;
				}
				playerStat.health = 100;
				playerStat.mana = 100;
				lives--;
			}
		}
	}

    void OnGUI()
    {
        if(lives > -1)
        {
            GUI.Label(new Rect(10,100, 60, 50), "lives:" + lives);
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
//    // Restart level
//    void Reset()
//    {
//        Application.LoadLevel(Application.loadedLevel);
//    }
}
