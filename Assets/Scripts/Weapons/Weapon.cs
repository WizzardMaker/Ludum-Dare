using UnityEngine;using System.Collections;[RequireComponent (typeof(Animation))]public class Weapon : MonoBehaviour {

	public float attackTimeLeft, attackTime;	public GameObject bulletPrefab,casingPrefab;	protected Animation anim;	public float clipSize = -1, clipAmunition = -1, amunition = -1;	public string weaponName;	public int side = 1;	// Use this for initialization	public void Start () {		anim = GetComponent<Animation>();	}	public void Update()
	{
		if (Input.GetButtonUp("Fire" + side))
		{
			attackTimeLeft = -1;
        }
	}	public void Reload()
	{
		if (amunition == -1 || clipAmunition == clipSize)
			return;

		if (amunition < clipSize)
		{
			clipAmunition = amunition;
		}
		else
		{
			clipAmunition = clipSize;
        }

		amunition -= clipSize;

		amunition = Mathf.Clamp(amunition, 0, Mathf.Infinity);
	}	public virtual void Shoot(bool playerShot = true, bool ai = false)	{		anim.Play();	}}