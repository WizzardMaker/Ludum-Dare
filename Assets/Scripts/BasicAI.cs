using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

	public UIHandler handler;

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

	public void Hit(bool bite = false)
	{
		if (actHitTime == -1 && !dead)
		{
			lives--;
			if (lives <= 0)
			{
				Die(bite);
			}

			standardMaterial = GetComponentInChildren<MeshRenderer>().material;
			GetComponentInChildren<MeshRenderer>().material = biteMaterial;

			actHitTime = Time.time + hitTime;
		}
	}

	void Die(bool bite = true)
	{
		Debug.Log(name);
		Debug.Log(handler.name);
		handler.AddXp(xp);
		
		moves = false;
		dead = true;
		for (int i = 0; i < Random.Range(1, rangeMoneySpawns); i++)
		{
			Instantiate(moneyPrefab, transform.position + new Vector3(1 * Random.Range(-2,2),0, 1 * Random.Range(-2, 2)), Quaternion.identity);
		}
		GetComponentInChildren<Animator>().SetBool("death",true);
	}


}
