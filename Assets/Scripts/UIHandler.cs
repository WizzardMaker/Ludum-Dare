using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour {

	public Image staminaPanel,xpPanel;

	public GameObject Shop;

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

		Armor.GetComponentInChildren<Text>().text = "Current Health: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lives;
		Stamina.GetComponentInChildren<Text>().text = "Current Sprint Time: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().sprintTime;

		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1,true))
		{
			Pistol.GetComponent<Text>().text = "Bought Left!";
		}
		else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1, false)) {
			Pistol.GetComponent<Text>().text = "Bought Both!";
		}
		else
		{
			Pistol.GetComponent<Text>().text = "Not Bought";
		}

		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, true))
		{
			Uzi.GetComponent<Text>().text = "Bought Left!";
		}
		else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, false))
		{
			Uzi.GetComponent<Text>().text = "Bought Both!";
		}
		else
		{
			Uzi.GetComponent<Text>().text = "Not Bought";
		}

		/*if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(3, true))
		{
			Pistol.GetComponentInChildren<Text>().text = "Bought Left!";
		}
		else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(3, false))
		{
			Pistol.GetComponentInChildren<Text>().text = "Bought Both!";
		}
		else
		{
			Pistol.GetComponentInChildren<Text>().text = "Not Bought";
		}*/
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
					Armor.GetComponentInChildren<Text>().text = "Current Health: " + lives;
					cashAmount -= 250;
				}
                break;
			case ("Stamina"):
				if (cashAmount >= 150)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().sprintTime += 1;
					Stamina.GetComponentInChildren<Text>().text = "Current Sprint Time: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().sprintTime;
					cashAmount -= 150;
				}
				break;
			case ("Pistol"):
				if (cashAmount >= 125)
				{
					if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1, true))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(1, true);
						Pistol.GetComponent<Text>().text = "Bought Left!";
						return;
					}
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(1, true))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(1, false);
						Pistol.GetComponent<Text>().text = "Bought Both!";
						return;
					}
					cashAmount -= 125;
				}
				break;
			case ("Uzi"):
				if (cashAmount >= 875)
				{
					if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, true))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(2, true);
						Uzi.GetComponent<Text>().text = "Bought Left!";
						return;
					}
					if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsUnlocked(2, true))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UnlockWeapon(2, false);
						Uzi.GetComponent<Text>().text = "Bought Both!";
						return;
					}
					cashAmount -= 875;
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
