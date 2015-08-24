using UnityEngine;
using System.Collections;

public class Enemy : BasicAI {


	Transform player;

	public WeaponContainer arm;

	Vector3 newDir;

	public float minDistance, maxShootDistance;

	public int detectRange;
	public float speed, rotSpeed;
	public int timeUntilCalm;
	public float actTimeUntilCalm;

	public bool detectedPlayer;

	// Use this for initialization
	new void Start()
	{
		base.Start();

		player = GameObject.FindGameObjectWithTag("Player").transform;

	}
	// Update is called once per frame
	new void Update()
	{
		base.Update();
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
		else
		{
			return;
		}

		if (!moves)
			return;

		Ray ray = new Ray(transform.position, player.position - transform.position);

		Debug.DrawRay(transform.position, player.position - transform.position, Color.red);

		RaycastHit hit;

		int layerMask = 1 << 10;

		layerMask = ~layerMask;

		if (Physics.Raycast(ray, out hit, detectRange, layerMask))
		{
			if (hit.transform.tag == "Player")
			{
				detectedPlayer = true;
				newDir = Vector3.RotateTowards(transform.forward, hit.point - transform.position, rotSpeed * Time.deltaTime, 0.0F);
				transform.rotation = Quaternion.LookRotation(newDir);
				transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
			}
		}
		else if (detectedPlayer && actTimeUntilCalm == -1)
		{
			actTimeUntilCalm = Time.time + timeUntilCalm;
		}

		//Debug.Log(Time.time);

		if (actTimeUntilCalm <= Time.time && actTimeUntilCalm != -1)
		{
			actTimeUntilCalm = -1;
			detectedPlayer = false;
		}


		if (detectedPlayer)
		{
			if (Vector3.Distance(transform.position, player.transform.position) >= minDistance)
			{
				controller.SimpleMove(transform.forward * speed);
			}

			//Debug.Log(Vector3.Distance(transform.position, player.transform.position));
			//Debug.Log(Physics.Raycast(ray, out hit, maxShootDistance, layerMask));
			Physics.Raycast(ray, out hit, maxShootDistance, layerMask);
            if (Vector3.Distance(transform.position, player.transform.position) <= maxShootDistance && hit.collider.tag == "Player")
			{
				transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 5, 0);
				arm.Shoot(false,true);
				transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 5, 0);
			}
		}

	}
}
