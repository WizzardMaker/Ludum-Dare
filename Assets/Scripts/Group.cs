using UnityEngine;
using System.Collections.Generic;

public class Group : MonoBehaviour {

	public List<GameObject> groupMembers = new List<GameObject>();
	public GameObject indicatorPrefab;
	public LevelGenerator lg;
	public GameObject indicator;

	public bool isShop = false;

	// Use this for initialization
	void Start () {
		indicator = Instantiate(indicatorPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		if (isShop)
		{
			indicator.transform.rotation = Quaternion.LookRotation((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position));
			indicator.transform.rotation = Quaternion.Euler(0, indicator.transform.rotation.eulerAngles.y, 0);
			indicator.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + indicator.transform.forward * 2;
			if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 15)
			{
				indicator.transform.GetChild(0).gameObject.SetActive(false);
			}
			else
			{
				indicator.transform.GetChild(0).gameObject.SetActive(true);
			}
			return;
		}

		List<int> toDelete = new List<int>();
		for (int i = 0; i < groupMembers.Count; i++)
		{
			if (groupMembers[i] == null)
			{
				groupMembers.RemoveAt(i);

			}
		}

		if (groupMembers.Count == 0)
		{
			Destroy(indicator);
			lg.totalGroups--;
			Destroy(gameObject);
			return;
		}

		transform.position = FindCenterPoint(groupMembers);

		indicator.transform.rotation =  Quaternion.LookRotation((transform.position -GameObject.FindGameObjectWithTag("Player").transform.position));
		indicator.transform.rotation = Quaternion.Euler(0, indicator.transform.rotation.eulerAngles.y, 0);
		indicator.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + indicator.transform.forward * 2;

	}
	Vector3 FindCenterPoint(List<GameObject> gos) {
		for(int i = 0; i < gos.Count; i++)
		{
			if (gos[i] == null)
			{
				gos.RemoveAt(i);

			}
		}
		if (gos.Count == 0)
			return Vector3.zero;

		if (gos.Count == 1)
			return gos[0].transform.position;

		
			

		Bounds bounds = new Bounds(gos[0].transform.position, Vector3.zero);
		for (int i = 1; i< gos.Count; i++)
			bounds.Encapsulate(gos[i].transform.position); 
		return bounds.center;
	}
}
