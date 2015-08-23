using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

	public UIHandler handler;

	public GameObject moneyPrefab;
	public int rangeMoneySpawns;

	public bool moves = true;

	public float xp;

	bool dead;

	public bool wasBitten;
	public Material biteMaterial;

	Material standardMaterial;

	
	protected CharacterController controller;


	public float biteTime, actBiteTime = -1;

	public int lives;


	// Use this for initialization
	protected void Start () {
		controller = GetComponent<CharacterController>();
		handler = GameObject.FindGameObjectWithTag("UI").GetComponent<UIHandler>();
    }
	
	// Update is called once per frame
	protected void Update () {

		if (dead)
		{
			
        }

		if (wasBitten && actBiteTime == -1 && !dead)
		{
			lives--;
			if(lives <= 0)
			{
				Die();
			}

			standardMaterial = GetComponentInChildren<MeshRenderer>().material;
			GetComponentInChildren<MeshRenderer>().material = biteMaterial;

			actBiteTime = Time.time + biteTime;
		}

		if(actBiteTime <= Time.time && wasBitten)
		{
			GetComponentInChildren<MeshRenderer>().material = standardMaterial;

			actBiteTime = -1;
			wasBitten = false;
        }
	}

	void Hit(bool bite = false)
	{

	}

	void Die(bool bite = true)
	{
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
