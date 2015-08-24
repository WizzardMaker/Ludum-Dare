using UnityEngine;
using System.Collections;

public class Uzi : Weapon {

	// Use this for initialization
	new void Start () {
		base.Start();
		amunition = 15;
		clipSize = 15;
		clipAmunition = 15;
    }

	// Update is called once per frame
	public override void Shoot(bool playerShot = true, bool ai = false) {
		if (attackTimeLeft <= Time.time)
		{
			attackTimeLeft = -1;
		}		if (attackTimeLeft == -1 && clipAmunition > 0)
		{
			clipAmunition -= ai ? 0 : 1; ;
			GameObject temp = (GameObject)Instantiate(bulletPrefab, transform.position, transform.parent.parent.parent.localRotation);
			temp.GetComponent<Bullet>().playerFired = playerShot;
			Instantiate(casingPrefab, transform.position, transform.parent.parent.localRotation);
			attackTimeLeft = Time.time + attackTime;
		}
	}
}
