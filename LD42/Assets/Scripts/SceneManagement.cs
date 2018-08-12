using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagement : MonoBehaviour
{
	public void StartGame ()
	{
		SceneManager.LoadScene ("Main");
	}

	public void MainMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public static void GameOver ()
	{
		SceneManager.LoadScene ("GameOver");
	}

	public static void Victory ()
	{
		SceneManager.LoadScene ("Victory");
	}
}

