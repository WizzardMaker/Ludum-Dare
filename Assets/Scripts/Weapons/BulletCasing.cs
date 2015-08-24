using UnityEngine;
using System.Collections;

public class BulletCasing : MonoBehaviour {
	Vector3 startPos;

	float bulletLifeLeft, bulletLife = 9;

	// Use this for initialization
	void Awake()
	{
		startPos = transform.position;
		bulletLifeLeft = Time.time + bulletLife;
        GetComponent<Rigidbody>().velocity = transform.right * -1;
	}

	void Update()
	{
		if (bulletLifeLeft <= Time.time)
		{
			bulletLifeLeft = -1;
		}		if (bulletLifeLeft == -1)
		{
			Destroy(gameObject);
		}

			if (Vector3.Distance(transform.position, startPos) >= 25)
			Destroy(gameObject);

	}
}
