using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Shoot()	{		base.Shoot();	}
}
