using UnityEngine;
using System.Collections;

public class Cash : PickUp {

	private float internalCash;
	public float[] range = new float[2] {1,10};

	// Use this for initialization
	void Start () {
		internalCash = Random.Range(range[0], range[1]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTriggerEnter(Collider caller)
	{
		if(caller.tag == "Player")
			caller.gameObject.GetComponent<Player>().handler.AddCash(Mathf.Round(internalCash));

		base.OnTriggerEnter(caller);
	}
}
