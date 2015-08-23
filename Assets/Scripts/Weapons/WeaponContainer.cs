using UnityEngine;
using System.Collections;

public class WeaponContainer : MonoBehaviour{	public GameObject[] weapons = new GameObject[1];	int activeWeapon;	void Awake()
	{
		toggleWeapon(0);
	}	protected void resetWeapons()
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			Debug.Log(i);
			weapons[i].gameObject.SetActive(false);
		}
	}	public void toggleWeapon(int id)
	{
		activeWeapon = id;
		resetWeapons();
		weapons[id].gameObject.SetActive(true);
	}

	public void toggleWeapon(string name)
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			if (weapons[i].GetComponent<Weapon>().weaponName == name)
			{
				activeWeapon = i;
				resetWeapons();
				weapons[i].gameObject.SetActive(true);
				return;
			}

		}

		throw new Exception("Weapon with name: " + name + " not in Container");
	}	public void nextWeapon()
	{
		if (activeWeapon >= weapons.Length -1)
			activeWeapon = -1;
		toggleWeapon(activeWeapon + 1);
    }	public void Shoot()
	{
		if (activeWeapon != -1)
		{
			weapons[activeWeapon].GetComponent<Weapon>().Shoot();
			Debug.Log(weapons[activeWeapon].GetComponent<Weapon>().weaponName);
		}
	}}

[global::System.Serializable]
public class Exception : System.Exception
{
	public Exception() { }
	public Exception(string message) : base(message) { }
	public Exception(string message, System.Exception inner) : base(message, inner) { }
	protected Exception(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context)
	{ }
}