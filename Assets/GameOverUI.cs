using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {

	public void ExitButton()
	{
		Application.Quit();
	}
	public void RestartButton()
	{
		Application.LoadLevel(Application.loadedLevel - 1);
	}
}
