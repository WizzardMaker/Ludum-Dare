﻿using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	virtual public void OnTriggerEnter(Collider caller)
	{
		if(caller.tag == "Player" || caller.tag == "House")
			Destroy(gameObject);
	}

}
