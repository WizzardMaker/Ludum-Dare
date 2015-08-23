using UnityEngine;using System.Collections.Generic;public class Player : MonoBehaviour {	public bool aiControled = false;	public Transform player;	public WeaponContainer leftHand, rightHand;	CharacterController controller;	public UIHandler handler;	public float speed,rotSpeed;	public float sprintTime,sprintTimeLeft, sprintSpeedFactor,sprintRefill;	public float biteRange, biteTime, biteTimeLeft;	public Material hitMaterial;	Vector3 newDir;	Vector3 lastPosition;	Vector3 velocity;	// Use this for initialization	void Start () {		controller = GetComponent<CharacterController>();		sprintTimeLeft = sprintTime;		biteTimeLeft = -1;		if (aiControled)
		{
			Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
			tag = "Untagged";
			for(int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length - 1; i++)
			{

			}
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}    }		void Update(){		if (aiControled)
		{
			if (Input.GetButtonDown("Reload"))
			{
				leftHand.Reload();
			}
			if (Input.GetButtonDown("ReloadRight"))
			{
				rightHand.Reload();
			}
			if (Input.GetButtonDown("WeaponLeft"))
			{
				Debug.Log("Change Left!");
				leftHand.NextWeapon();
			}
			if (Input.GetButtonDown("WeaponRight"))
			{
				Debug.Log("Change Right!");
				rightHand.NextWeapon();
			}

			if (Input.GetButton("Fire2"))
			{
				rightHand.Shoot();
			}
			if (Input.GetButton("Fire1"))
			{
				leftHand.Shoot();
			}
		}
		else
		{
			velocity = transform.forward * Input.GetAxisRaw("Vertical");

			handler.staminaAmount = sprintTimeLeft / sprintTime;

			//Debug.Log(Input.GetAxisRaw("SwitchWeapon"));

			handler.lAmunition = leftHand.weapons[leftHand.activeWeapon].GetComponent<Weapon>().amunition;
			handler.lClipAmunition = leftHand.weapons[leftHand.activeWeapon].GetComponent<Weapon>().clipAmunition;

			handler.rAmunition = rightHand.weapons[rightHand.activeWeapon].GetComponent<Weapon>().amunition;
			handler.rClipAmunition = rightHand.weapons[rightHand.activeWeapon].GetComponent<Weapon>().clipAmunition;

			if (Input.GetButtonDown("Reload"))
			{
				leftHand.Reload();
			}
			if (Input.GetButtonDown("ReloadRight"))
			{
				rightHand.Reload();
			}

			if (Input.GetButtonDown("WeaponLeft"))
			{
				Debug.Log("Change Left!");
				leftHand.NextWeapon();
				handler.ChangeWeapon(leftHand.activeWeapon, true);
			}
			if (Input.GetButtonDown("WeaponRight"))
			{
				Debug.Log("Change Right!");
				rightHand.NextWeapon();
				handler.ChangeWeapon(rightHand.activeWeapon, false);
			}

			if (Input.GetButton("Fire2"))
			{
				rightHand.Shoot();
			}
			if (Input.GetButton("Fire1"))
			{
				leftHand.Shoot();
			}		}	}	// Update is called once per frame	void FixedUpdate () {				if (aiControled)
		{
			
			Ray ray = new Ray(transform.position, player.position - transform.position);

			Debug.DrawRay(transform.position, player.position - transform.position, Color.red);

			RaycastHit hit;

			int layerMask = 1 << 8;

			layerMask = ~layerMask;
			//Debug.Log(player.position);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				Debug.Log(hit.transform.tag);
				if (hit.transform.tag == "Player") {
					if ((Vector3.Distance(lastPosition, transform.position) / Time.deltaTime) != 0)
					{
						newDir = Vector3.RotateTowards(transform.forward, hit.point - transform.position, rotSpeed * Time.deltaTime, 0.0F);
						transform.rotation = Quaternion.LookRotation(newDir);
						transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
					}
				}
				if (Vector3.Distance(transform.position, player.transform.position) >= 3)
				{
					if (Input.GetButton("Sprint") && sprintTimeLeft > 0)
					{

						controller.SimpleMove((transform.forward * speed) * sprintSpeedFactor);

					}
					else
					{
						controller.SimpleMove(transform.forward * speed);
						
                    }
				}
				//Debug.Log(Vector3.Distance(lastPosition, transform.position) / Time.deltaTime);
				//if ((Vector3.Distance(lastPosition, transform.position) / Time.deltaTime) == 0)
				//{
					
					ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					layerMask = 1 << 8;
					Debug.Log("Rotate");
					if (Physics.Raycast(ray, out hit, 100, layerMask))
					{
						newDir = Vector3.RotateTowards(transform.forward, hit.point - transform.position, rotSpeed * Time.deltaTime, 0.0F);
						transform.rotation = Quaternion.LookRotation(newDir);
						transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
					}
				//}

				lastPosition = transform.position;
			}
		}
		else
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			int layerMask = 1 << 8;
			if (Physics.Raycast(ray, out hit, 100, layerMask))
			{
				newDir = Vector3.RotateTowards(transform.forward, hit.point - transform.position, rotSpeed * Time.deltaTime, 0.0F);
				transform.rotation = Quaternion.LookRotation(newDir);
				transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
			}

			if (Input.GetButton("Sprint") && sprintTimeLeft > 0)
			{
				controller.SimpleMove(velocity * (speed * sprintSpeedFactor));
				sprintTimeLeft--;
			}
			else
			{
				controller.SimpleMove(velocity * (speed));
			}

			if (!Input.GetButton("Sprint"))
			{
				sprintTimeLeft += sprintTimeLeft < sprintTime ? sprintRefill : 0;
			}		}	}	public void Hit()
	{

	}
}