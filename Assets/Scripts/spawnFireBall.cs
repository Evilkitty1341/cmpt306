using UnityEngine;
using System.Collections;

public class spawnFireBall : MonoBehaviour {
    //the audiosource attached to the game object
    //AudioSource audio;
    
    //prefab to spawn
    public GameObject bulletPrefab;

    //the bullet that has been spawned
    public GameObject spawnedBullet;

    public float fireRate = 1.0F;
    private float nextFire = 0.0F;
    StatCollectionClass playerStat;

	//to check that the intro scene is done
	StoryLineComponents SLC;

    // Use this for initialization
     void Start()
     {
         //audio = GetComponent<AudioSource>();
		SLC = GameObject.Find("Main Camera").GetComponent<StoryLineComponents>();
     }

    // Update is called once per frame
    void Update()
    {
        //fire1 is set as left mouse button

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && SLC.playerEnabled == true)
        {
            nextFire = Time.time + fireRate;
            //print ("fire");
           // audio.Play();
            //spawn a bullet as an object
            spawnedBullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            //add force to bullet
            if (playerStat.playerDirection == 1)
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500));
            }
            else if (playerStat.playerDirection == 2)
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
            }
            else if (playerStat.playerDirection == 3)
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -500));
            }
            else if (playerStat.playerDirection == 4)
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, -0));
            }
            /*
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500));
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -500));
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                spawnedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, -0));
            }*/
        }
    }
}
