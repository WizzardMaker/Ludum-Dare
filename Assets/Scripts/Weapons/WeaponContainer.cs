using UnityEngine;
using System.Collections;

public class WeaponContainer : MonoBehaviour{	public GameObject[] weapons = new GameObject[1];	public bool[] weaponActive = new bool[1];	public int activeWeapon;	public int side = 1;	public void SetActive(int id)
	{
		weaponActive[id] = true;
    }	void Awake()
	{
		ToggleWeapon(0);
	}	protected void ResetWeapon()
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			//Debug.Log(i);
			weapons[i].GetComponent<Weapon>().side = side;
            weapons[i].gameObject.SetActive(false);
		}
	}	public void ToggleWeapon(int id)
	{
		if (weapons.Length == id)
		{
			id = 0;
		}
		if (!weaponActive[id])
		{
				while (id <= weapons.Length) {
					id += 1;
					if (weapons.Length == id)
					{
						id = 0;
						break;
					}
			}
        }
		activeWeapon = id;
		ResetWeapon();
		weapons[id].gameObject.SetActive(true);
	}

	public void ToggleWeapon(string name)
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			if (weapons[i].GetComponent<Weapon>().weaponName == name)
			{
				activeWeapon = i;
				ResetWeapon();
				weapons[i].gameObject.SetActive(true);
				return;
			}

		}

		throw new Exception("Weapon with name: " + name + " not in Container");
	}	public void NextWeapon()
	{
		if (activeWeapon >= weapons.Length)
			activeWeapon = -1;
		ToggleWeapon(activeWeapon + 1);
    }	public void Reload()
	{
		if (activeWeapon != -1)
		{
			weapons[activeWeapon].GetComponent<Weapon>().Reload();
			//Debug.Log(weapons[activeWeapon].GetComponent<Weapon>().weaponName);
		}
	}	public void Shoot(bool playerShot = true, bool ai = false, bool playSound = false)
	{
		if (activeWeapon != -1)
		{
			weapons[activeWeapon].GetComponent<Weapon>().Shoot(playerShot, ai, playSound);
			//Debug.Log(weapons[activeWeapon].GetComponent<Weapon>().weaponName);
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