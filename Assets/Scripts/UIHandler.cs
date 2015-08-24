using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour {

	public Image staminaPanel,xpPanel;

	public GameObject Shop,PauseMenu;

	public WeaponContainer leftHand, rightHand;

	public Text lives, cash, levelText, shopCash;
	public int livesAmount;
	public float cashAmount;

	public float lClipAmunition = -1, lAmunition = -1;
	public float rClipAmunition = -1, rAmunition = -1;

	public Text leftAmunition, rightAmunition;

	[Range(0,1)]
	public float staminaAmount;

	public float xpAmount;

	public GameObject Armor, Stamina, Pistol, Uzi, Shotgun;
	public GameObject PistolAmmo, UziAmmo, ShotgunAmmo;

	public int level;
	public float nextLevel = 15;
	public float xp;

	// Use this for initialization
	void Start () {
	
	}
	
	public void ChangeWeapon(int id, bool isLeftHand)
	{
		if (isLeftHand){
			leftHand.ToggleWeapon(id);
        }else{
			rightHand.ToggleWeapon(id);
		}
	}

	public void AddCash(float amount, float multiplier = 1)
	{
		cashAmount += amount * multiplier;
	}

	public void AddXp(float xp)
	{
		this.xp += xp;
	}
	public void StartShop()
	{

		Armor.GetComponent<Text>().text = "Current Health: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lives / 2;
		Stamina.GetComponent<Text>().text = "Current Sprint Time: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().sprintTime;

		
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1, false)) {
			Pistol.GetComponent<Text>().text = "Bought Both!";
		}
		else
		{
			Pistol.GetComponent<Text>().text = "Not Bought";
		}
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1,true))
		{
			Pistol.GetComponent<Text>().text = "Bought Left!";
		}

		
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, false))
		{
			Uzi.GetComponent<Text>().text = "Bought Both!";
		}
		else
		{
			Uzi.GetComponent<Text>().text = "Not Bought";
		}
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, true))
		{
			Uzi.GetComponent<Text>().text = "Bought Left!";
		}

		UziAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[2].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[2].GetComponent<Weapon>().amunition;
		PistolAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[1].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[1].GetComponent<Weapon>().amunition;
		ShotgunAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[3].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[3].GetComponent<Weapon>().amunition;
		
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(3, false))
		{
			Shotgun.GetComponentInChildren<Text>().text = "Bought Both!";
		}
		else
		{
			Shotgun.GetComponentInChildren<Text>().text = "Not Bought";
		}
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(3, true))
		{
			Shotgun.GetComponentInChildren<Text>().text = "Bought Left!";
		}
	}

	public void PauseMenuExit()
	{
		PauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	public void Exit()
	{
		Application.Quit();
	}

	public void ShopExit()
	{
		Shop.SetActive(false);
		Time.timeScale = 1;
	}

	public void BuyShop(string upgrade)
	{
		switch (upgrade)
		{
			case ("Armor"):
				if (cashAmount >= 250)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lives += 1;
					Armor.GetComponent<Text>().text = "Current Health: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lives;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lives += 1;
					cashAmount -= 250;
				}
				break;
			case ("Stamina"):
				if (cashAmount >= 150)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().sprintTime += 10;
					Stamina.GetComponent<Text>().text = "Current Sprint Time: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().sprintTime;
					cashAmount -= 150;
				}
				break;
			case ("Pistol"):
				if (cashAmount >= 125)
				{
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1, false))
						return;
					cashAmount -= 125;
					if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1, true))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(1, true);
						int tempWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.activeWeapon;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.ToggleWeapon(1);
						Pistol.GetComponent<Text>().text = "Bought Left!";
						PistolAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[1].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[1].GetComponent<Weapon>().amunition;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.ToggleWeapon(tempWeapon);
						return;
					}
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1, true))
					{
						int tempWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.activeWeapon;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(1, false);
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.ToggleWeapon(1);
						Pistol.GetComponent<Text>().text = "Bought Both!";
						PistolAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[1].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[1].GetComponent<Weapon>().amunition;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.ToggleWeapon(tempWeapon);
						return;
					}
					
				}
				break;
			case ("Uzi"):
				if (cashAmount >= 875)
				{
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, false))
						return;
					cashAmount -= 875;
					if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, true))
					{
						int tempWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.activeWeapon;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(2, true);
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.ToggleWeapon(2);
						Uzi.GetComponent<Text>().text = "Bought Left!";
						UziAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[2].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[2].GetComponent<Weapon>().amunition;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.ToggleWeapon(tempWeapon);
						return;
					}
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, true))
					{
						int tempWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.activeWeapon;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(2, false);
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.ToggleWeapon(2);
						Uzi.GetComponent<Text>().text = "Bought Both!";
						UziAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[2].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[2].GetComponent<Weapon>().amunition;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.ToggleWeapon(tempWeapon);
						return;
					}
					
				}
				break;
			case ("Shotgun"):
				if (cashAmount >= 1250)
				{
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(3, false))
						return;
					cashAmount -= 1250;
					if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(3, true))
					{
						int tempWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.activeWeapon;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(3, true);
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.ToggleWeapon(3);
						Shotgun.GetComponent<Text>().text = "Bought Left!";
						UziAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[3].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[3].GetComponent<Weapon>().amunition;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.ToggleWeapon(tempWeapon);
						return;
					}
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(3, true))
					{
						int tempWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.activeWeapon;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(3, false);
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.ToggleWeapon(3);
						Shotgun.GetComponent<Text>().text = "Bought Both!";
						ShotgunAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[3].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[3].GetComponent<Weapon>().amunition;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.ToggleWeapon(tempWeapon);
						return;
					}

				}
				break;
			case ("PistolAmmo"):
				if (cashAmount >= 25)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[1].GetComponent<Weapon>().amunition += 8;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[1].GetComponent<Weapon>().amunition += 8;
                    PistolAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[1].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[1].GetComponent<Weapon>().amunition;
					cashAmount -= 25;
				}
				break;
			case ("UziAmmo"):
				if (cashAmount >= 75)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[2].GetComponent<Weapon>().amunition += 15;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[2].GetComponent<Weapon>().amunition += 15;
					UziAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[2].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[2].GetComponent<Weapon>().amunition;
					cashAmount -= 75;
				}
				break;
			case ("ShotgunAmmo"):
				if (cashAmount >= 70)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[3].GetComponent<Weapon>().amunition += 6;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[3].GetComponent<Weapon>().amunition += 6;
					ShotgunAmmo.GetComponent<Text>().text = "Ammo:  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().leftHand.weapons[3].GetComponent<Weapon>().amunition + "/" + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rightHand.weapons[3].GetComponent<Weapon>().amunition;
					cashAmount -= 70;
				}
				break;
		}
	}

	void OnGUI()
	{
		if (xp >= nextLevel)
		{
			level++;
			nextLevel *= level;
		}

		xpAmount = xp / nextLevel;
		if (lAmunition != -1)
		{
			leftAmunition.text = lClipAmunition + " / " + lAmunition;
		}
		else
		{
			leftAmunition.text = "";
        }
		if (rAmunition != -1)
		{
			rightAmunition.text = rClipAmunition + " / " + rAmunition;
		}
		else
		{
			rightAmunition.text = "";
		}
		lives.text = "Lives: " + livesAmount;
		cash.text = "Cash: " + cashAmount + "$";
		shopCash.text = "Cash: " + cashAmount + "$";
		levelText.text = "Xp | Level: " + level;

		staminaPanel.fillAmount = staminaAmount;

		xpPanel.fillAmount = xpAmount;
    }
}
