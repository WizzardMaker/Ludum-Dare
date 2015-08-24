using UnityEngine;
using System.Collections;

public class HumanAI: BasicAI {

	Transform player;



	Vector3 newDir;

	public int detectRange;
	public float speed, rotSpeed;
	public int timeUntilCalm;
	public float actTimeUntilCalm;

	public bool runsAway;

	// Use this for initialization
	new void Start () {
		base.Start();
		player = GameObject.FindGameObjectWithTag("Player").transform;

	}

	// Update is called once per frame
	new void Update () {

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
			if (hit.transform.tag == "Player") { 
				runsAway = true;
				newDir = Vector3.RotateTowards(transform.forward, hit.point - transform.position, rotSpeed * Time.deltaTime, 0.0F);
				transform.rotation = Quaternion.LookRotation(newDir);
				transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
			}
		}else if(runsAway && actTimeUntilCalm == -1){
			actTimeUntilCalm = Time.time + timeUntilCalm;
		}

		//Debug.Log(Time.time);

		if(actTimeUntilCalm <= Time.time && actTimeUntilCalm != -1)
		{
			actTimeUntilCalm = -1;
			runsAway = false;
		}


		
		controller.SimpleMove(runsAway ? transform.forward * -speed : Vector3.zero);

	}
}
