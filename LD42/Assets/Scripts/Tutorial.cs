using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour 
{
	public GameObject MenuPanel;
	public GameObject TutorialPanel;

	public void OpenTutorial ()
	{
		MenuPanel.SetActive (false);
		TutorialPanel.SetActive (true);
	}

	public void CloseTutorial ()
	{
		MenuPanel.SetActive (true);
		TutorialPanel.SetActive (false);
	}
}
