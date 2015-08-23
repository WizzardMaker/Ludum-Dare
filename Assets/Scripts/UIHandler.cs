using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour {

	public Image staminaPanel,xpPanel;

	public Text lives, cash, levelText;
	public int livesAmount;
	public float cashAmount;

	[Range(0,1)]
	public float staminaAmount;

	public float xpAmount;


	public int level;
	public float nextLevel = 15;
	public float xp;

	// Use this for initialization
	void Start () {
	
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

		lives.text = "Lives: " + livesAmount;
		cash.text = "Cash: " + cashAmount + "$";
		levelText.text = "Level: " + level;

		staminaPanel.fillAmount = staminaAmount;

		xpPanel.fillAmount = xpAmount;
    }
}
