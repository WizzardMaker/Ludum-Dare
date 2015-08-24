using UnityEngine;using System.Collections;public class House : MonoBehaviour {

	//GameObject player;

	public bool outerWall;	public MeshRenderer roofRenderer, doorRenderer;	public Transform transformToMove;	// Use this for initialization	void Start () {		//player = GameObject.FindGameObjectWithTag("Player");	}		// Update is called once per frame	void Update () {	}	void OnTriggerEnter(Collider other)	{				if(other.tag == "Player")		{			Debug.Log("Player Entered");			transformToMove.position += transform.up * -1.25f;

			if (!outerWall)
			{
				doorRenderer.enabled = false;
				roofRenderer.gameObject.SetActive(false);
			}		}	}	void OnTriggerExit(Collider other)	{				if (other.tag == "Player")		{			Debug.Log("Player left");						transformToMove.position += transform.up * 1.25f;

			if (!outerWall)
			{
				doorRenderer.enabled = true;
				roofRenderer.gameObject.SetActive( true);
			}
		}	}}/*int x = Screen.width / 2;int y = Screen.height / 2;GetComponent<MeshRenderer>().material.color = new Color(GetComponent<MeshRenderer>().material.color.r, GetComponent<MeshRenderer>().material.color.g, GetComponent<MeshRenderer>().material.color.b, 1);Ray rayM = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));RaycastHit hitM;if (GetComponent<Collider>().Raycast(rayM, out hitM, 100) && player.GetComponentInChildren<Collider>().Raycast(rayM, out hitM, 100)){	Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0)) - transform.position, Color.red);	if (hitM.transform.CompareTag("Player"))	{		GetComponent<MeshRenderer>().material.color = new Color(GetComponent<MeshRenderer>().material.color.r, GetComponent<MeshRenderer>().material.color.g, GetComponent<MeshRenderer>().material.color.b, 0);	}} */