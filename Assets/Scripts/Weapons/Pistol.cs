using UnityEngine;
using System.Collections;

public class Pistol : Weapon {

	// Use this for initialization
	new void Start () {
		base.Start();
		amunition = 30;
		clipSize = 8;
		clipAmunition = 8;
    }
	
	public override void Shoot(bool playerShot = true, bool ai = false)	{		base.Shoot();

		if (attackTimeLeft <= Time.time)
		{
			attackTimeLeft = -1;
		}		if (attackTimeLeft == -1 && clipAmunition > 0)
		{
			clipAmunition -= ai ? 0:1;
			GameObject temp = (GameObject)Instantiate(bulletPrefab, transform.position, transform.parent.parent.parent.localRotation);
			temp.GetComponent<Bullet>().playerFired = playerShot;
			Instantiate(casingPrefab, transform.position, transform.parent.parent.localRotation);
			attackTimeLeft = Time.time + attackTime;
		}	}
}
