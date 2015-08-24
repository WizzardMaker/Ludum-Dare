using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {
	new void Start()
	{
		base.Start();
		/*
		amunition = 30;
		clipSize = 8;
		clipAmunition = 8;*/
	}

	new void Update()
	{

	}

	public override void Shoot(bool playerShot = true, bool ai = false, bool playSound = false)	{
		if (attackTimeLeft <= Time.time)
		{
			attackTimeLeft = -1;
		}		if (attackTimeLeft == -1 && clipAmunition > 0)
		{
			clipAmunition -= ai ? 0 : 1;

			Debug.Log(transform.parent.parent.parent.localRotation + "/" + transform.parent.parent.parent.localRotation.y);
			GameObject temp = (GameObject)Instantiate(bulletPrefab, transform.position, transform.parent.parent.parent.localRotation);
			GameObject temp2 = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.parent.parent.localRotation.eulerAngles + new Vector3(0,transform.parent.parent.parent.position.y,0) * 3));
			GameObject temp3 = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.parent.parent.localRotation.eulerAngles + new Vector3(0, transform.parent.parent.parent.position.y, 0) * -3));
			temp.GetComponent<Bullet>().playerFired = playerShot;
			temp2.GetComponent<Bullet>().playerFired = playerShot;
			temp3.GetComponent<Bullet>().playerFired = playerShot;
			Instantiate(casingPrefab, transform.position, Quaternion.Euler(transform.parent.parent.localRotation.eulerAngles));
			attackTimeLeft = Time.time + attackTime;
			base.Shoot(playerShot, ai, playSound);
		}	}
}

