using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	Vector3 startPos;

	public bool playerFired = true;

	// Use this for initialization
	void Awake () {
		startPos = transform.position;
		GetComponent<Rigidbody>().velocity = transform.forward * 30;
	}

	void Update()
	{
		if (Vector3.Distance(transform.position, startPos) >= 25)
			Destroy(gameObject);

	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log(playerFired);
		//Debug.Log(col.name);
		if(col.tag == "Human" || col.tag == (playerFired ? "Enemy" : "Player"))
		{
			if (col.tag == "Player" && !playerFired)
            {
				col.GetComponent<Player>().Hit();

			}else if(col.tag == "Human" || col.tag == "Enemy")
			{
				col.GetComponent<BasicAI>().Hit(false);
			}
		}

		if(col.tag == "Human" || col.tag == "House" || col.tag == "Zombie" || col.tag == (playerFired ? "Enemy" : "Player") )
			Destroy(gameObject);
	}
}
