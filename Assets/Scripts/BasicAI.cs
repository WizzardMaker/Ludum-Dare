using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

	public UIHandler handler;
	public GameObject shop;

	public bool shopMaster = false;
	public bool invincible = false;

	public GameObject zombiePrefab;

	public GameObject moneyPrefab;
	public int rangeMoneySpawns;

	public bool moves = true;

	public float xp;

	bool dead;

	public bool wasHit;
	public Material biteMaterial;

	Material standardMaterial;

	
	protected CharacterController controller;


	public float hitTime, actHitTime = -1;

	public int lives;


	// Use this for initialization
	protected void Start () {
		controller = GetComponent<CharacterController>();
		handler = GameObject.FindGameObjectWithTag("UI").GetComponent<UIHandler>();
		Debug.Log(handler.name);
		
		shop = GameObject.FindGameObjectWithTag("Shop");
		if(shopMaster)
			shop.SetActive(false);
    }
	
	// Update is called once per frame
	protected void Update () {

		if (GetComponentInChildren<Animator>().GetBool("died"))
		{
			Destroy(gameObject);
        }



		if(actHitTime <= Time.time && actHitTime != -1)
		{
			GetComponentInChildren<MeshRenderer>().material = standardMaterial;

			actHitTime = -1;
			wasHit = false;
        }
	}

	public void Hit(bool bite = false, float zombieChance = 20)
	{
		if (invincible)
			return;

		if (actHitTime == -1 && !dead)
		{
			lives--;
			if (lives <= 0)
			{
				Die(bite, zombieChance);
			}

			standardMaterial = GetComponentInChildren<MeshRenderer>().material;
			GetComponentInChildren<MeshRenderer>().material = biteMaterial;

			actHitTime = Time.time + hitTime;
		}
	}

	void Die(bool bite = false, float zombieChance = 20)
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		Debug.Log(name);
		Debug.Log(handler.name);
		handler.AddXp(xp);
		
		moves = false;
		dead = true;

		if (bite)
		{
			if (Random.Range(0, 100) <= zombieChance)
			{
				GameObject temp = (GameObject)Instantiate(zombiePrefab, transform.position, Quaternion.identity);
				temp.GetComponent<Player>().aiControled = true;
			}
		}

		for (int i = 0; i < Random.Range(1, rangeMoneySpawns); i++)
		{
			Instantiate(moneyPrefab, transform.position + new Vector3(1 * Random.Range(-2,2),0, 1 * Random.Range(-2, 2)), Quaternion.identity);
		}
		GetComponentInChildren<Animator>().SetBool("death",true);
	}

	public void Interact()
	{
		Debug.Log("Interacted");

		if (!shopMaster)
			return;
		Time.timeScale = 0;
		shop.gameObject.SetActive(true);
		handler.StartShop();
	}


}
