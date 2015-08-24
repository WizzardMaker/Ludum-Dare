using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void PlayButton()
	{
		Application.LoadLevel(1);
	}

	public void ExitButton()
	{
		Application.Quit();
	}

	public void NextButton()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	public void PrevButton()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	public void BackButton()
	{
		Application.LoadLevel(0);
	}
	public void TutorialButton()
	{
		Application.LoadLevel(3);
	}
}
