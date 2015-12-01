using UnityEngine;
using System.Collections;

public class DestroyExplosion : MonoBehaviour {

	float Delay = 1f;

	void Awake()
	{
		Destroy (gameObject, Delay);
	}
}
