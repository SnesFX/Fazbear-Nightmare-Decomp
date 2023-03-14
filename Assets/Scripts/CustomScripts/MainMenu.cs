using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public void Night1()
    {
		SceneManager.LoadScene("Night 1");
	}

    public void Night2()
    {
		SceneManager.LoadScene("Night 2");
	}

	public void Credits()
	{
		SceneManager.LoadScene("Creds");
	}

	public void Back()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
