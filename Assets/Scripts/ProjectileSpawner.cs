using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

	public GameObject projectile;


	public void rangedAttack(float damage, Vector3 target){

		float rotZ = Mathf.Atan2 (target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
		Quaternion.AngleAxis (rotZ, Vector3.forward);

		GameObject bullet = Instantiate(projectile) as GameObject;
		bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.AngleAxis (rotZ + 90.0f, Vector3.forward);
		bullet.GetComponent<ProjectileCheck>().damage = damage;
		bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x - transform.position.x , target.y - transform.position.y) * 100);

	}

}
