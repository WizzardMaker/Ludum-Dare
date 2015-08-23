using UnityEngine;
using System.Collections;

public class BulletCasing : MonoBehaviour {
	Vector3 startPos;

	// Use this for initialization
	void Awake()
	{
		startPos = transform.position;
		GetComponent<Rigidbody>().velocity = transform.right * -1;
	}

	void Update()
	{
		if (Vector3.Distance(transform.position, startPos) >= 25)
			Destroy(gameObject);

	}
}
