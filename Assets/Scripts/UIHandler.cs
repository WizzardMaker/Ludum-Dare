using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour {

	public Image staminaPanel,xpPanel;

	public WeaponContainer leftHand, rightHand;

	public Text lives, cash, levelText;
	public int livesAmount;
	public float cashAmount;

	public float lClipAmunition = -1, lAmunition = -1;
	public float rClipAmunition = -1, rAmunition = -1;

	public Text leftAmunition, rightAmunition;

	[Range(0,1)]
	public float staminaAmount;

	public float xpAmount;


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

	void Update()
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
		levelText.text = "Xp | Level: " + level;

		staminaPanel.fillAmount = staminaAmount;

		xpPanel.fillAmount = xpAmount;
    }
}
