using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<Rigidbody>().velocity = transform.forward * 5;
	}

	void onTriggerEnter(Collider col)
	{
		if(col.tag == "Human")
		{
			col.GetComponent<HumanAI>().wasBitten;
		}
	}
}
