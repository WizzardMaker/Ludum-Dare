using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public float distance = 5;
	public Transform followTransform;
	public bool fixY = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(followTransform.position.x, fixY ? 0 : followTransform.position.y ,followTransform.position.z) + transform.forward * -distance;
	}
}
