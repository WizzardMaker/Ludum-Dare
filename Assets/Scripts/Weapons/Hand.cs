using UnityEngine;
using System.Collections;

public class Hand : Weapon {

	public float biteTimeLeft, biteTime, biteRange;

	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Shoot()
	{
		base.Shoot();
        if (biteTimeLeft <= Time.time)
		{
			biteTimeLeft = -1;
		}

		int layerMask = 1 << 10;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, layerMask))
		{
			if (hit.collider.tag == "Human" || hit.collider.tag == "Enemy")
			{
				if (Vector3.Distance(hit.collider.transform.position, transform.position) <= biteRange && biteTimeLeft == -1)
				{
					biteTimeLeft = Time.time + biteTime;
					Debug.Log(hit.collider.name);
					hit.collider.gameObject.GetComponent<BasicAI>().wasBitten = true;
				}
			}
		}
	}
}
