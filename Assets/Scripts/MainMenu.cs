using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayButton()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void ExitButton()
	{
		Application.Quit();
	}
}
