using UnityEngine;
using System.Collections;

public class Hand : Weapon {

	public float attackRange;


	// Use this for initialization
	new void Start () {
		base.Start();
	}

	public override void Shoot(bool playerShot = true, bool ai = false, bool playSound = false)
	{
		base.Shoot();

		if (attackTimeLeft <= Time.time)
		{
			attackTimeLeft = -1;
		}

		int layerMask = 1 << 10;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, layerMask))
		{
			if (hit.collider.tag == "Human" || hit.collider.tag == "Enemy")
			{
				if (Vector3.Distance(hit.collider.transform.position, transform.position) <= attackRange && attackTimeLeft == -1)
				{
					attackTimeLeft = Time.time + attackTime;
					Debug.Log(hit.collider.name);
					hit.collider.gameObject.GetComponent<BasicAI>().Hit(true);
				}
			}
		}
	}
}
