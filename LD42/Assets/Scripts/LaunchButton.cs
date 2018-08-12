using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchButton : MonoBehaviour 
{
	Button button;

	TurnManager turnManager;

	void Start () 
	{
		button = GetComponent<Button> ();
		button.onClick.AddListener (delegate {
			TaskWithParameters (transform.parent.gameObject);
		});

		turnManager = FindObjectOfType<TurnManager> ();
	}

	void TaskWithParameters(GameObject go)
	{
		turnManager.ShipLaunch (go);
	}
}
