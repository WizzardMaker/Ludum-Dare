using UnityEngine;using System.Collections.Generic;public class Player : MonoBehaviour {	public float infectChance = 20;	public int lives = 2;	public bool invincible = false;	public float timeInvincible, timeInvincibleLeft;	public bool aiControled = false, dead = false;	public List<GameObject> fellowZombies = new List<GameObject>();	public Transform player;	public WeaponContainer leftHand, rightHand;	CharacterController controller;	public UIHandler handler;	public float speed,rotSpeed;	public float sprintTime,sprintTimeLeft, sprintSpeedFactor,sprintRefill;	public float biteRange, biteTime, biteTimeLeft;	public Material hitMaterial;	Vector3 newDir;	Vector3 lastPosition;	Vector3 velocity;	// Use this for initialization	void Start () {		handler = GameObject.FindGameObjectWithTag("UI").GetComponent<UIHandler>();        controller = GetComponent<CharacterController>();		sprintTimeLeft = sprintTime;		biteTimeLeft = -1;		if (aiControled)
		{
			Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
			tag = "Untagged";
			player = GameObject.FindGameObjectWithTag("Player").transform;
			player.GetComponent<Player>().AddZombie(gameObject);
		}    }

	public bool IsUnlocked(int id, bool left)
	{
		if (left)
		{
			return leftHand.weaponActive[id];
		}
		return rightHand.weaponActive[id];
	}

	public void UnlockWeapon(int id, bool left = true)
	{
		if (left)
		{
			leftHand.SetActive(id);
		}
		else
		{
			rightHand.SetActive(id);
		}
		
		foreach(GameObject go in fellowZombies)
		{
			go.GetComponent<Player>().UnlockWeapon(id,left);
		}
    }	public void AddZombie(GameObject toAdd)
	{
		fellowZombies.Add(toAdd);

		foreach (GameObject go in fellowZombies)
		{
			for (int i = 0; i < leftHand.weaponActive.Length; i++)
			{
				if(leftHand.weaponActive[i])
					go.GetComponent<Player>().UnlockWeapon(i, true);
			}
			for (int i = 0; i < rightHand.weaponActive.Length; i++)
			{
				if (rightHand.weaponActive[i])
					go.GetComponent<Player>().UnlockWeapon(i, false);
			}
		}
	}	void Update(){				if (GetComponentInChildren<Animator>().GetBool("died"))
		{
			if (aiControled)
				player.GetComponent<Player>().fellowZombies.Remove(gameObject);
			Destroy(gameObject);
			return;
		}		if (dead)
			return;		if (timeInvincibleLeft <= Time.time && timeInvincibleLeft != -1){
			timeInvincibleLeft = -1;
			invincible = false;
			GetComponentInChildren<Animator>().SetBool("invincible", false);
		}		if (aiControled)
		{

			if (Input.GetButtonDown("Fire3"))
			{
				Debug.Log("Interact");
				//int layerMask = 1 << 11;
				//layerMask = ~layerMask;

				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				int layerMask = 1 << 10;
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
				{
					Debug.Log(hit.collider.name);
					if (hit.collider.tag == "Human" || hit.collider.tag == "Enemy")
					{
						if (Vector3.Distance(hit.collider.transform.position, transform.position) <= 8)
						{
							
							hit.collider.gameObject.GetComponent<BasicAI>().Interact();
						}
					}
				}
			}

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
			}		}	}	// Update is called once per frame	void FixedUpdate () {

		if (dead)
			return;

		player = GameObject.FindGameObjectWithTag("Player").transform;		if (aiControled)
		{
			
			Ray ray = new Ray(transform.position, player.position - transform.position);

			//Debug.Log(player.position - transform.position);
			Debug.DrawRay(transform.position, player.position - transform.position, Color.red);
			int layerMask;
			RaycastHit hit;
			//Debug.Log(player.position);
			if (player.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
			{
				if (Vector3.Distance(lastPosition, transform.position) != 0)
				{
					//newDir = Vector3.RotateTowards(transform.position, (player.position * 1.1f) - transform.position, rotSpeed * Time.deltaTime, 0.0F);
					transform.rotation = Quaternion.LookRotation(player.position - transform.position);
					transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
				}
				//Debug.Log(Vector3.Distance(lastPosition, transform.position));
				if (Vector3.Distance(lastPosition, transform.position) == 0)
				{
					ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					layerMask = 1 << 8;
					//Debug.Log("Rotate");
					if (Physics.Raycast(ray, out hit, 100, layerMask))
					{
						newDir = Vector3.RotateTowards(transform.forward, hit.point - transform.position, rotSpeed * Time.deltaTime, 0.0F);
						transform.rotation = Quaternion.LookRotation(newDir);
						transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
					}
				}
				lastPosition = transform.position;
				if (Vector3.Distance(transform.position, player.transform.position) >= 3 && Vector3.Distance(transform.position, player.transform.position) <= 8)
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
				

				
			}
		}
		else
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			int layerMask = 1 << 8;
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
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
			}		}	}	public void MakeInvincible()
	{
		invincible = true;
		timeInvincibleLeft = Time.time + timeInvincible;
		GetComponentInChildren<Animator>().SetBool("invincible", true);
	}	public void TogglePlayer()
	{
		tag = "Untagged";
		fellowZombies[fellowZombies.Count - 1].tag = "Player";
		fellowZombies[fellowZombies.Count - 1].GetComponent<Player>().aiControled = false;
		fellowZombies[fellowZombies.Count - 1].GetComponent<Player>().MakeInvincible();
        fellowZombies.RemoveAt(fellowZombies.Count -1 );
	}	public void Hit()
	{
		if (invincible)
			return;

		lives--;
		if (lives == 0)
		{
			if (fellowZombies.Count == 0)
			{
				Application.LoadLevel(Application.loadedLevel + 1);
				return;
			}

			if (!aiControled && !dead)
				TogglePlayer();

			dead = true;
			GetComponentInChildren<Animator>().SetBool("death", true);
		}
	}
}